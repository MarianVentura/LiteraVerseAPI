using LiteraVerseApi.DAL;
using LiteraVerseApi.DTOs;
using LiteraVerseApi.Models;
using Mapster;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace LiteraVerseApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class SyncController(Contexto context) : ControllerBase
{
    [HttpPost("push")]
    public async Task<ActionResult> Push(SyncPushRequest request)
    {
        var pendingSync = new PendingSync
        {
            UserId = request.UserId,
            EntityType = request.EntityType,
            EntityId = request.EntityId,
            Action = request.Action,
            Data = request.Data,
            IsSynced = true
        };

        context.PendingSync.Add(pendingSync);
        await context.SaveChangesAsync();

        return Ok(new { message = "Data synced successfully", syncId = pendingSync.SyncId });
    }

    [HttpGet("pull")]
    public async Task<ActionResult<SyncPullResponse>> Pull([FromQuery] DateTime? lastSync)
    {
        var syncTime = lastSync ?? DateTime.UtcNow.AddDays(-30);

        var stories = await context.Stories
            .Where(s => s.UpdatedAt > syncTime && s.IsPublished)
            .ProjectToType<StoryResponse>()
            .ToListAsync();

        var chapters = await context.Chapters
            .Where(c => c.UpdatedAt > syncTime && c.IsPublished)
            .ProjectToType<ChapterResponse>()
            .ToListAsync();

        var response = new SyncPullResponse
        {
            Stories = stories,
            Chapters = chapters,
            SyncTime = DateTime.UtcNow
        };

        return Ok(response);
    }

    [HttpGet("status/{userId}")]
    public async Task<ActionResult> GetSyncStatus(int userId)
    {
        var lastSync = await context.PendingSync
            .Where(s => s.UserId == userId && s.IsSynced)
            .OrderByDescending(s => s.CreatedAt)
            .FirstOrDefaultAsync();

        var pendingCount = await context.PendingSync
            .CountAsync(s => s.UserId == userId && !s.IsSynced);

        return Ok(new
        {
            lastSyncDate = lastSync?.CreatedAt,
            hasPendingChanges = pendingCount > 0,
            pendingSyncsCount = pendingCount,
            syncStatus = pendingCount > 0 ? "pending" : "synced"
        });
    }
}