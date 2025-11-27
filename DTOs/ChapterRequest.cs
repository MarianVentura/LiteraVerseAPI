namespace LiteraVerseApi.DTOs;

public class ChapterRequest
{
    public required string Title { get; set; }
    public required string Content { get; set; }
    public required int ChapterNumber { get; set; }
    public bool IsDraft { get; set; } = true;
}