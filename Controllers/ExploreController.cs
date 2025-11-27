using LiteraVerseApi.DAL;
using LiteraVerseApi.DTOs;
using Mapster;
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
        return await context.Stories
            .Where(s => s.IsPublished && !s.IsDraft)
            .OrderByDescending(s => s.ViewCount)
            .Take(10)
            .ProjectToType<StoryResponse>()
            .ToListAsync();
    }

    [HttpGet("popular")]
    public async Task<ActionResult<IEnumerable<StoryResponse>>> GetPopular()
    {
        return await context.Stories
            .Where(s => s.IsPublished && !s.IsDraft)
            .OrderByDescending(s => s.ViewCount)
            .Take(20)
            .ProjectToType<StoryResponse>()
            .ToListAsync();
    }

    [HttpGet("new")]
    public async Task<ActionResult<IEnumerable<StoryResponse>>> GetNew()
    {
        return await context.Stories
            .Where(s => s.IsPublished && !s.IsDraft)
            .OrderByDescending(s => s.PublishedAt)
            .Take(20)
            .ProjectToType<StoryResponse>()
            .ToListAsync();
    }

    [HttpGet("genre/{genre}")]
    public async Task<ActionResult<IEnumerable<StoryResponse>>> GetByGenre(string genre)
    {
        return await context.Stories
            .Where(s => s.IsPublished && !s.IsDraft && s.Genre == genre)
            .OrderByDescending(s => s.ViewCount)
            .ProjectToType<StoryResponse>()
            .ToListAsync();
    }
}