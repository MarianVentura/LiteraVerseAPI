using LiteraVerseApi.DAL;
using LiteraVerseApi.DTOs;
using LiteraVerseApi.Models;
using Mapster;
using MapsterMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace LiteraVerseApi.Controllers;

[Route("api/Stories/{storyId}/[controller]")]
[ApiController]
public class ChaptersController(
    Contexto context,
    IMapper mapper) : ControllerBase
{
    [HttpGet]
    public async Task<ActionResult<IEnumerable<ChapterResponse>>> GetChapters(int storyId)
    {
        return await context.Chapters
            .Where(c => c.StoryId == storyId)
            .OrderBy(c => c.ChapterNumber)
            .ProjectToType<ChapterResponse>()
            .ToListAsync();
    }

    [HttpGet("{chapterId}")]
    public async Task<ActionResult<ChapterResponse>> GetChapter(int storyId, int chapterId)
    {
        var chapter = await context.Chapters
            .FirstOrDefaultAsync(c => c.ChapterId == chapterId && c.StoryId == storyId);

        if (chapter == null)
        {
            return NotFound();
        }

        return mapper.Map<ChapterResponse>(chapter);
    }

    [HttpPost]
    public async Task<ActionResult<ChapterResponse>> PostChapter(int storyId, ChapterRequest chapterRequest)
    {
        var entity = mapper.Map<Chapters>(chapterRequest);
        entity.StoryId = storyId;

        context.Chapters.Add(entity);
        await context.SaveChangesAsync();

        var response = mapper.Map<ChapterResponse>(entity);
        return CreatedAtAction("GetChapter", new { storyId, chapterId = entity.ChapterId }, response);
    }

    [HttpPut("{chapterId}")]
    public async Task<IActionResult> PutChapter(int storyId, int chapterId, ChapterRequest chapterRequest)
    {
        var affected = await context.Chapters
            .Where(c => c.ChapterId == chapterId && c.StoryId == storyId)
            .ExecuteUpdateAsync(s => s
                .SetProperty(c => c.Title, chapterRequest.Title)
                .SetProperty(c => c.Content, chapterRequest.Content)
                .SetProperty(c => c.ChapterNumber, chapterRequest.ChapterNumber)
                .SetProperty(c => c.IsDraft, chapterRequest.IsDraft)
                .SetProperty(c => c.UpdatedAt, DateTime.UtcNow)
            );

        if (affected == 0)
            return NotFound();

        return Ok();
    }

    [HttpDelete("{chapterId}")]
    public async Task<IActionResult> DeleteChapter(int storyId, int chapterId)
    {
        var affected = await context.Chapters
            .Where(c => c.ChapterId == chapterId && c.StoryId == storyId)
            .ExecuteDeleteAsync();

        if (affected == 0)
            return NotFound();

        return NoContent();
    }

    [HttpPost("{chapterId}/publish")]
    public async Task<IActionResult> PublishChapter(int storyId, int chapterId)
    {
        var affected = await context.Chapters
            .Where(c => c.ChapterId == chapterId && c.StoryId == storyId)
            .ExecuteUpdateAsync(s => s
                .SetProperty(c => c.IsPublished, true)
                .SetProperty(c => c.IsDraft, false)
                .SetProperty(c => c.PublishedAt, DateTime.UtcNow)
            );

        if (affected == 0)
            return NotFound();

        return Ok();
    }

    [HttpPost("{chapterId}/unpublish")]
    public async Task<IActionResult> UnpublishChapter(int storyId, int chapterId)
    {
        var affected = await context.Chapters
            .Where(c => c.ChapterId == chapterId && c.StoryId == storyId)
            .ExecuteUpdateAsync(s => s
                .SetProperty(c => c.IsPublished, false)
                .SetProperty(c => c.IsDraft, true)
            );

        if (affected == 0)
            return NotFound();

        return Ok();
    }
}
