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
public class GenresController(
    Contexto context,
    IMapper mapper) : ControllerBase
{
    [HttpGet]
    public async Task<ActionResult<IEnumerable<GenreResponse>>> GetGenres()
    {
        return await context.Genres
            .ProjectToType<GenreResponse>()
            .ToListAsync();
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<GenreResponse>> GetGenre(int id)
    {
        var genre = await context.Genres.FindAsync(id);

        if (genre == null)
        {
            return NotFound();
        }

        return mapper.Map<GenreResponse>(genre);
    }

    [HttpPost]
    public async Task<ActionResult<GenreResponse>> PostGenre(GenreRequest genreRequest)
    {
        var entity = mapper.Map<Genres>(genreRequest);
        context.Genres.Add(entity);
        await context.SaveChangesAsync();

        var response = mapper.Map<GenreResponse>(entity);
        return CreatedAtAction("GetGenre", new { id = entity.GenreId }, response);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> PutGenre(int id, GenreRequest genreRequest)
    {
        var affected = await context.Genres
            .Where(g => g.GenreId == id)
            .ExecuteUpdateAsync(s => s
                .SetProperty(g => g.Name, genreRequest.Name)
                .SetProperty(g => g.Description, genreRequest.Description)
            );

        if (affected == 0)
            return NotFound();

        return Ok();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteGenre(int id)
    {
        var affected = await context.Genres
            .Where(g => g.GenreId == id)
            .ExecuteDeleteAsync();

        if (affected == 0)
            return NotFound();

        return NoContent();
    }
}