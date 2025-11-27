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
            .Select(f => f.Story)
            .ProjectToType<StoryResponse>()
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
            return BadRequest("Story already in favorites");
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
        var affected = await context.Favorites
            .Where(f => f.UserId == userId && f.StoryId == storyId)
            .ExecuteDeleteAsync();

        if (affected == 0)
            return NotFound();

        return NoContent();
    }

    [HttpGet("{userId}/progress")]
    public async Task<ActionResult<IEnumerable<ReadingProgressResponse>>> GetProgress(int userId)
    {
        return await context.ReadingProgress
            .Where(p => p.UserId == userId)
            .ProjectToType<ReadingProgressResponse>()
            .ToListAsync();
    }

    [HttpGet("{userId}/progress/{storyId}")]
    public async Task<ActionResult<ReadingProgressResponse>> GetProgressByStory(int userId, int storyId)
    {
        var progress = await context.ReadingProgress
            .FirstOrDefaultAsync(p => p.UserId == userId && p.StoryId == storyId);

        if (progress == null)
        {
            return NotFound();
        }

        return mapper.Map<ReadingProgressResponse>(progress);
    }

    [HttpPost("{userId}/progress")]
    public async Task<ActionResult> SaveProgress(int userId, ReadingProgressRequest request)
    {
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
            var progress = mapper.Map<ReadingProgress>(request);
            context.ReadingProgress.Add(progress);
        }

        await context.SaveChangesAsync();
        return Ok();
    }

    [HttpGet("{userId}/completed")]
    public async Task<ActionResult<IEnumerable<StoryResponse>>> GetCompleted(int userId)
    {
        var completed = await context.CompletedStories
            .Where(c => c.UserId == userId)
            .Include(c => c.Story)
            .Select(c => c.Story)
            .ProjectToType<StoryResponse>()
            .ToListAsync();

        return Ok(completed);
    }

    [HttpPost("{userId}/completed/{storyId}")]
    public async Task<ActionResult> MarkAsCompleted(int userId, int storyId)
    {
        var exists = await context.CompletedStories
            .AnyAsync(c => c.UserId == userId && c.StoryId == storyId);

        if (exists)
        {
            return BadRequest("Story already marked as completed");
        }

        var completed = new CompletedStories
        {
            UserId = userId,
            StoryId = storyId
        };

        context.CompletedStories.Add(completed);
        await context.SaveChangesAsync();

        return Ok();
    }
}