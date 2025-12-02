using LiteraVerseApi.DAL;
using LiteraVerseApi.DTOs;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace LiteraVerseApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class UsersController : ControllerBase
{
    private readonly Contexto _context;

    public UsersController(Contexto context)
    {
        _context = context;
    }

    [HttpGet("{userId}")]
    public async Task<ActionResult<UserProfileResponse>> GetUserProfile(int userId)
    {
        var user = await _context.Usuarios.FindAsync(userId);

        if (user == null)
            return NotFound();

        var storiesCount = await _context.Stories.CountAsync(s => s.UserId == userId);
        var publishedCount = await _context.Stories.CountAsync(s => s.UserId == userId && s.IsPublished);
        var totalViews = await _context.Stories.Where(s => s.UserId == userId).SumAsync(s => s.ViewCount);
        var favoritesCount = await _context.Favorites.CountAsync(f => f.UserId == userId);

        var profile = new UserProfileResponse
        {
            UserId = user.UsuarioId,
            UserName = user.UserName,
            StoriesCount = storiesCount,
            PublishedStoriesCount = publishedCount,
            TotalViews = totalViews,
            FavoritesCount = favoritesCount
        };

        return Ok(profile);
    }
}