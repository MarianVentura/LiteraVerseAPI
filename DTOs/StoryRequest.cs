namespace LiteraVerseApi.DTOs;

public class StoryRequest
{
    public required int UserId { get; set; }
    public required string Title { get; set; }
    public string Synopsis { get; set; } = string.Empty;
    public string? CoverImageUrl { get; set; }
    public bool IsDraft { get; set; } = true;
    public string? Genre { get; set; }
    public string? Tags { get; set; }
}