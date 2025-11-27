using LiteraVerseApi.DAL;
using LiteraVerseApi.DTOs;
using Mapster;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace LiteraVerseApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class SearchController(Contexto context) : ControllerBase
{
    [HttpGet]
    public async Task<ActionResult<IEnumerable<StoryResponse>>> Search(
        [FromQuery] string? query,
        [FromQuery] string? genre,
        [FromQuery] string? status)
    {
        var storiesQuery = context.Stories.AsQueryable();

        if (!string.IsNullOrEmpty(query))
        {
            storiesQuery = storiesQuery.Where(s =>
                s.Title.Contains(query) ||
                s.Synopsis.Contains(query) ||
                s.Tags!.Contains(query));
        }

        if (!string.IsNullOrEmpty(genre))
        {
            storiesQuery = storiesQuery.Where(s => s.Genre == genre);
        }

        if (!string.IsNullOrEmpty(status))
        {
            if (status.ToLower() == "published")
            {
                storiesQuery = storiesQuery.Where(s => s.IsPublished);
            }
            else if (status.ToLower() == "draft")
            {
                storiesQuery = storiesQuery.Where(s => s.IsDraft);
            }
        }
        else
        {
            storiesQuery = storiesQuery.Where(s => s.IsPublished);
        }

        return await storiesQuery
            .OrderByDescending(s => s.UpdatedAt)
            .ProjectToType<StoryResponse>()
            .ToListAsync();
    }
}