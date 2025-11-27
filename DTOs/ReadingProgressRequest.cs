namespace LiteraVerseApi.DTOs;

public class ReadingProgressRequest
{
    public required int UserId { get; set; }
    public required int StoryId { get; set; }
    public required int ChapterId { get; set; }
    public double ScrollPosition { get; set; }
}