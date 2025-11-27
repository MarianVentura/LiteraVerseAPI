namespace LiteraVerseApi.DTOs;

public class StoryResponse
{
    public int StoryId { get; set; }
    public int UserId { get; set; }
    public required string Title { get; set; }
    public string Synopsis { get; set; } = string.Empty;
    public string? CoverImageUrl { get; set; }
    public bool IsDraft { get; set; }
    public bool IsPublished { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime? PublishedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
    public int ViewCount { get; set; }
    public string? Genre { get; set; }
    public string? Tags { get; set; }
}