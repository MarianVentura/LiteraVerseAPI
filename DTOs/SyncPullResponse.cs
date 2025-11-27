namespace LiteraVerseApi.DTOs;

public class SyncPullResponse
{
    public List<StoryResponse> Stories { get; set; } = new();
    public List<ChapterResponse> Chapters { get; set; } = new();
    public DateTime SyncTime { get; set; } = DateTime.UtcNow;
}