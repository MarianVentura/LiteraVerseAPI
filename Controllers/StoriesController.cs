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
public class StoriesController(
    Contexto context,
    IMapper mapper) : ControllerBase
{
    [HttpGet]
    public async Task<ActionResult<IEnumerable<StoryResponse>>> GetStories()
    {
        var stories = await context.Stories
            .Include(s => s.User)
            .Where(s => s.IsPublished)
            .Select(s => new StoryResponse
            {
                StoryId = s.StoryId,
                UserId = s.UserId,
                UserName = s.User != null ? s.User.UserName : null,
                Title = s.Title,
                Synopsis = s.Synopsis,
                CoverImageUrl = s.CoverImageUrl,
                IsDraft = s.IsDraft,
                IsPublished = s.IsPublished,
                CreatedAt = s.CreatedAt,
                PublishedAt = s.PublishedAt,
                UpdatedAt = s.UpdatedAt,
                ViewCount = s.ViewCount,
                Genre = s.Genre,
                Tags = s.Tags
            })
            .ToListAsync();

        return Ok(stories);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<StoryResponse>> GetStory(int id)
    {
        var story = await context.Stories
            .Include(s => s.User)
            .FirstOrDefaultAsync(s => s.StoryId == id);

        if (story == null)
        {
            return NotFound();
        }

        var response = new StoryResponse
        {
            StoryId = story.StoryId,
            UserId = story.UserId,
            UserName = story.User?.UserName,
            Title = story.Title,
            Synopsis = story.Synopsis,
            CoverImageUrl = story.CoverImageUrl,
            IsDraft = story.IsDraft,
            IsPublished = story.IsPublished,
            CreatedAt = story.CreatedAt,
            PublishedAt = story.PublishedAt,
            UpdatedAt = story.UpdatedAt,
            ViewCount = story.ViewCount,
            Genre = story.Genre,
            Tags = story.Tags
        };

        return Ok(response);
    }

    [HttpGet("{id}/reader")]
    public async Task<ActionResult<StoryReaderResponse>> GetStoryForReading(int id)
    {
        var story = await context.Stories
            .Include(s => s.User)
            .Include(s => s.Chapters.Where(c => c.IsPublished))
            .FirstOrDefaultAsync(s => s.StoryId == id);

        if (story == null)
            return NotFound();

        var likeCount = await context.Likes
            .CountAsync(l => l.StoryId == id);

        var response = new StoryReaderResponse
        {
            StoryId = story.StoryId,
            UserId = story.UserId,
            Title = story.Title,
            Synopsis = story.Synopsis,
            CoverImageUrl = story.CoverImageUrl,
            Author = story.User?.UserName ?? "Unknown",
            AuthorId = story.UserId,
            Genre = story.Genre,
            Tags = story.Tags,
            ViewCount = story.ViewCount,
            LikeCount = likeCount,
            Chapters = story.Chapters
                .Where(c => c.IsPublished)
                .OrderBy(c => c.ChapterNumber)
                .Select(c => new ChapterResponse
                {
                    ChapterId = c.ChapterId,
                    StoryId = c.StoryId,
                    Title = c.Title,
                    Content = c.Content,
                    ChapterNumber = c.ChapterNumber,
                    IsDraft = c.IsDraft,
                    IsPublished = c.IsPublished,
                    CreatedAt = c.CreatedAt,
                    PublishedAt = c.PublishedAt,
                    UpdatedAt = c.UpdatedAt
                })
                .ToList()
        };

        return Ok(response);
    }

    [HttpGet("user/{userId}")]
    public async Task<ActionResult<IEnumerable<StoryResponse>>> GetStoriesByUser(int userId)
    {
        var stories = await context.Stories
            .Include(s => s.User)
            .Where(s => s.UserId == userId)
            .Select(s => new StoryResponse
            {
                StoryId = s.StoryId,
                UserId = s.UserId,
                UserName = s.User != null ? s.User.UserName : null,
                Title = s.Title,
                Synopsis = s.Synopsis,
                CoverImageUrl = s.CoverImageUrl,
                IsDraft = s.IsDraft,
                IsPublished = s.IsPublished,
                CreatedAt = s.CreatedAt,
                PublishedAt = s.PublishedAt,
                UpdatedAt = s.UpdatedAt,
                ViewCount = s.ViewCount,
                Genre = s.Genre,
                Tags = s.Tags
            })
            .ToListAsync();

        return Ok(stories);
    }

    [HttpPost]
    public async Task<ActionResult<StoryResponse>> PostStory(StoryRequest storyRequest)
    {
        var entity = mapper.Map<Stories>(storyRequest);
        context.Stories.Add(entity);
        await context.SaveChangesAsync();

        var response = mapper.Map<StoryResponse>(entity);
        return CreatedAtAction("GetStory", new { id = entity.StoryId }, response);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> PutStory(int id, StoryRequest storyRequest)
    {
        var affected = await context.Stories
            .Where(s => s.StoryId == id)
            .ExecuteUpdateAsync(s => s
                .SetProperty(x => x.Title, storyRequest.Title)
                .SetProperty(x => x.Synopsis, storyRequest.Synopsis)
                .SetProperty(x => x.CoverImageUrl, storyRequest.CoverImageUrl)
                .SetProperty(x => x.Genre, storyRequest.Genre)
                .SetProperty(x => x.Tags, storyRequest.Tags)
                .SetProperty(x => x.IsDraft, storyRequest.IsDraft)
                .SetProperty(x => x.UpdatedAt, DateTime.UtcNow)
            );

        if (affected == 0)
            return NotFound();

        return Ok();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteStory(int id)
    {
        var affected = await context.Stories
            .Where(s => s.StoryId == id)
            .ExecuteDeleteAsync();

        if (affected == 0)
            return NotFound();

        return NoContent();
    }

    [HttpPost("{id}/publish")]
    public async Task<IActionResult> PublishStory(int id)
    {
        var affected = await context.Stories
            .Where(s => s.StoryId == id)
            .ExecuteUpdateAsync(s => s
                .SetProperty(x => x.IsPublished, true)
                .SetProperty(x => x.IsDraft, false)
                .SetProperty(x => x.PublishedAt, DateTime.UtcNow)
            );

        if (affected == 0)
            return NotFound();

        return Ok();
    }

    [HttpPost("{id}/unpublish")]
    public async Task<IActionResult> UnpublishStory(int id)
    {
        var affected = await context.Stories
            .Where(s => s.StoryId == id)
            .ExecuteUpdateAsync(s => s
                .SetProperty(x => x.IsPublished, false)
                .SetProperty(x => x.IsDraft, true)
            );

        if (affected == 0)
            return NotFound();

        return Ok();
    }

    [HttpPost("{id}/cover")]
    public async Task<IActionResult> UpdateCover(int id, [FromBody] string coverImageUrl)
    {
        var affected = await context.Stories
            .Where(s => s.StoryId == id)
            .ExecuteUpdateAsync(s => s
                .SetProperty(x => x.CoverImageUrl, coverImageUrl)
            );

        if (affected == 0)
            return NotFound();

        return Ok();
    }

    [HttpPost("{id}/view")]
    public async Task<IActionResult> IncrementViewCount(int id)
    {
        var affected = await context.Stories
            .Where(s => s.StoryId == id)
            .ExecuteUpdateAsync(s => s
                .SetProperty(x => x.ViewCount, x => x.ViewCount + 1)
            );

        if (affected == 0)
            return NotFound();

        return Ok();
    }

    [HttpPost("{id}/like")]
    public async Task<ActionResult> LikeStory(int id, [FromBody] int userId)
    {
        var exists = await context.Likes
            .AnyAsync(l => l.UserId == userId && l.StoryId == id);

        if (exists)
            return BadRequest(new { message = "Story already liked" });

        var like = new Likes { UserId = userId, StoryId = id };
        context.Likes.Add(like);
        await context.SaveChangesAsync();
        return Ok();
    }

    [HttpDelete("{id}/unlike")]
    public async Task<ActionResult> UnlikeStory(int id, [FromQuery] int userId)
    {
        var like = await context.Likes
            .FirstOrDefaultAsync(l => l.UserId == userId && l.StoryId == id);

        if (like == null)
            return NotFound();

        context.Likes.Remove(like);
        await context.SaveChangesAsync();
        return NoContent();
    }

    [HttpGet("{id}/likes/count")]
    public async Task<ActionResult<int>> GetLikeCount(int id)
    {
        var count = await context.Likes.CountAsync(l => l.StoryId == id);
        return Ok(new { likeCount = count });
    }
}