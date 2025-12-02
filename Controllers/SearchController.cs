using LiteraVerseApi.DAL;
using LiteraVerseApi.DTOs;
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
        [FromQuery] string? status,
        [FromQuery] string? sortBy)
    {
        var storiesQuery = context.Stories
            .Include(s => s.User)
            .AsQueryable();

        if (!string.IsNullOrEmpty(query))
        {
            var q = query.Trim();
            var qLower = q.ToLower();

            storiesQuery = storiesQuery.Where(s =>
                (!string.IsNullOrEmpty(s.Title) && s.Title.ToLower().Contains(qLower)) ||
                (!string.IsNullOrEmpty(s.Synopsis) && s.Synopsis.ToLower().Contains(qLower)) ||
                (!string.IsNullOrEmpty(s.Tags) && s.Tags!.ToLower().Contains(qLower)) ||
                (s.User != null && !string.IsNullOrEmpty(s.User.UserName) && s.User.UserName.ToLower().Contains(qLower))
            );
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

        storiesQuery = sortBy?.ToLower() switch
        {
            "popular" => storiesQuery.OrderByDescending(s => s.ViewCount),
            "title" => storiesQuery.OrderBy(s => s.Title),
            _ => storiesQuery.OrderByDescending(s => s.UpdatedAt)
        };

        var results = await storiesQuery
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

        return Ok(results);
    }
}