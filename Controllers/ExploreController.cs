using LiteraVerseApi.DAL;
using LiteraVerseApi.DTOs;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace LiteraVerseApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ExploreController(Contexto context) : ControllerBase
{
    [HttpGet("featured")]
    public async Task<ActionResult<IEnumerable<StoryResponse>>> GetFeatured()
    {
        var res = await context.Stories
            .Include(s => s.User)
            .Where(s => s.IsPublished && !s.IsDraft)
            .OrderByDescending(s => s.ViewCount)
            .Take(10)
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

        return Ok(res);
    }

    [HttpGet("popular")]
    public async Task<ActionResult<IEnumerable<StoryResponse>>> GetPopular()
    {
        var res = await context.Stories
            .Include(s => s.User)
            .Where(s => s.IsPublished && !s.IsDraft)
            .OrderByDescending(s => s.ViewCount)
            .Take(20)
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

        return Ok(res);
    }

    [HttpGet("new")]
    public async Task<ActionResult<IEnumerable<StoryResponse>>> GetNew()
    {
        var res = await context.Stories
            .Include(s => s.User)
            .Where(s => s.IsPublished && !s.IsDraft)
            .OrderByDescending(s => s.PublishedAt)
            .Take(20)
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

        return Ok(res);
    }

    [HttpGet("genre/{genre}")]
    public async Task<ActionResult<IEnumerable<StoryResponse>>> GetByGenre(string genre)
    {
        var res = await context.Stories
            .Include(s => s.User)
            .Where(s => s.IsPublished && !s.IsDraft && s.Genre == genre)
            .OrderByDescending(s => s.ViewCount)
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

        return Ok(res);
    }
}
