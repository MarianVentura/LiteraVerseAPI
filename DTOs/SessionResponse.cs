namespace LiteraVerseApi.DTOs;

public class SessionResponse
{
    public int SessionId { get; set; }
    public int UserId { get; set; }
    public string Token { get; set; } = string.Empty;
    public DateTime CreatedAt { get; set; }
    public DateTime? LastActivity { get; set; }
    public bool IsActive { get; set; }
    public string? DeviceInfo { get; set; }
}