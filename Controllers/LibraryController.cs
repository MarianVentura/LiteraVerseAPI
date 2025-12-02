using LiteraVerseApi.DAL;
using LiteraVerseApi.DTOs;
using LiteraVerseApi.Models;
using Mapster;
using MapsterMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace LiteraVerseApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class LibraryController(
    Contexto context,
    IMapper mapper) : ControllerBase
{
    [HttpGet("{userId}/favorites")]
    public async Task<ActionResult<IEnumerable<StoryResponse>>> GetFavorites(int userId)
    {
        var favorites = await context.Favorites
            .Where(f => f.UserId == userId)
            .Include(f => f.Story)
                .ThenInclude(s => s.User)
            .Select(f => new StoryResponse
            {
                StoryId = f.Story.StoryId,
                UserId = f.Story.UserId,
                UserName = f.Story.User != null ? f.Story.User.UserName : null,
                Title = f.Story.Title,
                Synopsis = f.Story.Synopsis,
                CoverImageUrl = f.Story.CoverImageUrl,
                IsDraft = f.Story.IsDraft,
                IsPublished = f.Story.IsPublished,
                CreatedAt = f.Story.CreatedAt,
                PublishedAt = f.Story.PublishedAt,
                UpdatedAt = f.Story.UpdatedAt,
                ViewCount = f.Story.ViewCount,
                Genre = f.Story.Genre,
                Tags = f.Story.Tags
            })
            .ToListAsync();

        return Ok(favorites);
    }

    [HttpPost("{userId}/favorites/{storyId}")]
    public async Task<ActionResult> AddFavorite(int userId, int storyId)
    {
        var exists = await context.Favorites
            .AnyAsync(f => f.UserId == userId && f.StoryId == storyId);

        if (exists)
        {
            return BadRequest(new { message = "Story already in favorites" });
        }

        var favorite = new Favorites
        {
            UserId = userId,
            StoryId = storyId
        };

        context.Favorites.Add(favorite);
        await context.SaveChangesAsync();

        return Ok();
    }

    [HttpDelete("{userId}/favorites/{storyId}")]
    public async Task<ActionResult> RemoveFavorite(int userId, int storyId)
    {
        var favorite = await context.Favorites
            .FirstOrDefaultAsync(f => f.UserId == userId && f.StoryId == storyId);

        if (favorite == null)
            return NotFound();

        context.Favorites.Remove(favorite);
        await context.SaveChangesAsync();
        return NoContent();
    }

    [HttpGet("{userId}/favorites/{storyId}/check")]
    public async Task<ActionResult<bool>> IsFavorite(int userId, int storyId)
    {
        var exists = await context.Favorites
            .AnyAsync(f => f.UserId == userId && f.StoryId == storyId);

        return Ok(new { isFavorite = exists });
    }

    [HttpGet("{userId}/progress")]
    public async Task<ActionResult<IEnumerable<ReadingProgressResponse>>> GetReadingProgress(int userId)
    {
        var progress = await context.ReadingProgress
            .Where(p => p.UserId == userId)
            .Include(p => p.Story)
            .Include(p => p.Chapter)
            .OrderByDescending(p => p.LastReadAt)
            .Select(p => new ReadingProgressResponse
            {
                ProgressId = p.ProgressId,
                UserId = p.UserId,
                StoryId = p.StoryId,
                ChapterId = p.ChapterId,
                ScrollPosition = p.ScrollPosition,
                LastReadAt = p.LastReadAt,
                StoryTitle = p.Story != null ? p.Story.Title : null,
                ChapterTitle = p.Chapter != null ? p.Chapter.Title : null
            })
            .ToListAsync();

        return Ok(progress);
    }

    [HttpPost("{userId}/progress")]
    public async Task<ActionResult> SaveReadingProgress(int userId, ReadingProgressRequest request)
    {
        if (userId != request.UserId)
            return BadRequest(new { message = "UserId mismatch" });

        var existing = await context.ReadingProgress
            .FirstOrDefaultAsync(p => p.UserId == userId && p.StoryId == request.StoryId);

        if (existing != null)
        {
            existing.ChapterId = request.ChapterId;
            existing.ScrollPosition = request.ScrollPosition;
            existing.LastReadAt = DateTime.UtcNow;
            context.ReadingProgress.Update(existing);
        }
        else
        {
            var newProgress = new ReadingProgress
            {
                UserId = request.UserId,
                StoryId = request.StoryId,
                ChapterId = request.ChapterId,
                ScrollPosition = request.ScrollPosition,
                LastReadAt = DateTime.UtcNow
            };
            context.ReadingProgress.Add(newProgress);
        }

        await context.SaveChangesAsync();
        return Ok();
    }

    [HttpDelete("{userId}/progress/{storyId}")]
    public async Task<ActionResult> DeleteReadingProgress(int userId, int storyId)
    {
        var progress = await context.ReadingProgress
            .FirstOrDefaultAsync(p => p.UserId == userId && p.StoryId == storyId);

        if (progress == null)
            return NotFound();

        context.ReadingProgress.Remove(progress);
        await context.SaveChangesAsync();
        return NoContent();
    }

    [HttpGet("{userId}/completed")]
    public async Task<ActionResult<IEnumerable<StoryResponse>>> GetCompletedStories(int userId)
    {
        var completed = await context.CompletedStories
            .Where(c => c.UserId == userId)
            .Include(c => c.Story)
                .ThenInclude(s => s.User)
            .OrderByDescending(c => c.CompletedAt)
            .Select(c => new StoryResponse
            {
                StoryId = c.Story.StoryId,
                UserId = c.Story.UserId,
                UserName = c.Story.User != null ? c.Story.User.UserName : null,
                Title = c.Story.Title,
                Synopsis = c.Story.Synopsis,
                CoverImageUrl = c.Story.CoverImageUrl,
                IsDraft = c.Story.IsDraft,
                IsPublished = c.Story.IsPublished,
                CreatedAt = c.Story.CreatedAt,
                PublishedAt = c.Story.PublishedAt,
                UpdatedAt = c.Story.UpdatedAt,
                ViewCount = c.Story.ViewCount,
                Genre = c.Story.Genre,
                Tags = c.Story.Tags
            })
            .ToListAsync();

        return Ok(completed);
    }

    [HttpPost("{userId}/completed/{storyId}")]
    public async Task<ActionResult> MarkAsCompleted(int userId, int storyId)
    {
        var exists = await context.CompletedStories
            .AnyAsync(c => c.UserId == userId && c.StoryId == storyId);

        if (exists)
            return BadRequest(new { message = "Story already marked as completed" });

        var completed = new CompletedStories
        {
            UserId = userId,
            StoryId = storyId,
            CompletedAt = DateTime.UtcNow
        };

        context.CompletedStories.Add(completed);
        await context.SaveChangesAsync();
        return Ok();
    }

    [HttpDelete("{userId}/completed/{storyId}")]
    public async Task<ActionResult> UnmarkAsCompleted(int userId, int storyId)
    {
        var completed = await context.CompletedStories
            .FirstOrDefaultAsync(c => c.UserId == userId && c.StoryId == storyId);

        if (completed == null)
            return NotFound();

        context.CompletedStories.Remove(completed);
        await context.SaveChangesAsync();
        return NoContent();
    }
}