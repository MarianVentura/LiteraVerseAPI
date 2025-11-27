namespace LiteraVerseApi.DTOs;

public class SettingsRequest
{
    public string Theme { get; set; } = "light";
    public int FontSize { get; set; } = 16;
    public bool NotificationsEnabled { get; set; } = true;
    public bool AutoPlayNext { get; set; } = true;
}