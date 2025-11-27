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
        return await context.Stories
            .Where(s => s.IsPublished)
            .ProjectToType<StoryResponse>()
            .ToListAsync();
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<StoryResponse>> GetStory(int id)
    {
        var story = await context.Stories.FindAsync(id);

        if (story == null)
        {
            return NotFound();
        }

        return mapper.Map<StoryResponse>(story);
    }

    [HttpGet("user/{userId}")]
    public async Task<ActionResult<IEnumerable<StoryResponse>>> GetStoriesByUser(int userId)
    {
        return await context.Stories
            .Where(s => s.UserId == userId)
            .ProjectToType<StoryResponse>()
            .ToListAsync();
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
}