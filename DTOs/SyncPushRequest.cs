namespace LiteraVerseApi.DTOs;

public class SyncPushRequest
{
    public required int UserId { get; set; }
    public required string EntityType { get; set; }
    public required int EntityId { get; set; }
    public required string Action { get; set; }
    public required string Data { get; set; }
}