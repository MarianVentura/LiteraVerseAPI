namespace LiteraVerseApi.DTOs;

public class ChapterResponse
{
    public int ChapterId { get; set; }
    public int StoryId { get; set; }
    public required string Title { get; set; }
    public required string Content { get; set; }
    public int ChapterNumber { get; set; }
    public bool IsDraft { get; set; }
    public bool IsPublished { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime? PublishedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
}