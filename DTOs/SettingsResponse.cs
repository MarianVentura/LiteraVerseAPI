namespace LiteraVerseApi.DTOs;

public class SettingsResponse
{
    public int SettingId { get; set; }
    public int UserId { get; set; }
    public string Theme { get; set; } = "light";
    public int FontSize { get; set; } = 16;
    public bool NotificationsEnabled { get; set; } = true;
    public bool AutoPlayNext { get; set; } = true;
    public DateTime UpdatedAt { get; set; }
}