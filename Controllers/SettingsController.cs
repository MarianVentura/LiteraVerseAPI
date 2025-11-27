using LiteraVerseApi.DAL;
using LiteraVerseApi.DTOs;
using Mapster;
using MapsterMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

// Alias para evitar conflicto con NuGet.Configuration.Settings
using AppSettings = LiteraVerseApi.Models.Settings;

namespace LiteraVerseApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class SettingsController(
    Contexto context,
    IMapper mapper) : ControllerBase
{
    [HttpGet("{userId}")]
    public async Task<ActionResult<SettingsResponse>> GetSettings(int userId)
    {
        var settings = await context.Settings
            .FirstOrDefaultAsync(s => s.UserId == userId);

        if (settings == null)
        {
            var defaultSettings = new LiteraVerseApi.Models.Settings
            {
                UserId = userId,
                Theme = "light",
                FontSize = 16,
                NotificationsEnabled = true,
                AutoPlayNext = true
            };

            context.Settings.Add(defaultSettings);
            await context.SaveChangesAsync();

            return mapper.Map<SettingsResponse>(defaultSettings);
        }

        return mapper.Map<SettingsResponse>(settings);
    }

    [HttpPut("{userId}")]
    public async Task<IActionResult> PutSettings(int userId, SettingsRequest settingsRequest)
    {
        var settings = await context.Settings
            .FirstOrDefaultAsync(s => s.UserId == userId);

        if (settings == null)
        {
            var newSettings = mapper.Map<LiteraVerseApi.Models.Settings>(settingsRequest);
            newSettings.UserId = userId;
            context.Settings.Add(newSettings);
        }
        else
        {
            settings.Theme = settingsRequest.Theme;
            settings.FontSize = settingsRequest.FontSize;
            settings.NotificationsEnabled = settingsRequest.NotificationsEnabled;
            settings.AutoPlayNext = settingsRequest.AutoPlayNext;
            settings.UpdatedAt = DateTime.UtcNow;

            context.Settings.Update(settings);
        }

        await context.SaveChangesAsync();
        return Ok();
    }
}