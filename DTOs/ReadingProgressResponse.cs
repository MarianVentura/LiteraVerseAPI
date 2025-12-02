namespace LiteraVerseApi.DTOs;

public class ReadingProgressResponse
{
    public int ProgressId { get; set; }
    public int UserId { get; set; }
    public int StoryId { get; set; }
    public int ChapterId { get; set; }
    public double ScrollPosition { get; set; }
    public DateTime LastReadAt { get; set; }
    public string? StoryTitle { get; set; }
    public string? ChapterTitle { get; set; }
}