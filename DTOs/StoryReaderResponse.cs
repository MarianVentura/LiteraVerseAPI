namespace LiteraVerseApi.DTOs;

public class StoryReaderResponse
{
    public int StoryId { get; set; }
    public int UserId { get; set; }
    public required string Title { get; set; }
    public string Synopsis { get; set; } = string.Empty;
    public string? CoverImageUrl { get; set; }
    public required string Author { get; set; }
    public int AuthorId { get; set; }
    public string? Genre { get; set; }
    public string? Tags { get; set; }
    public int ViewCount { get; set; }
    public int LikeCount { get; set; }
    public List<ChapterResponse> Chapters { get; set; } = new();
}