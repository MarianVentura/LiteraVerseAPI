namespace LiteraVerseApi.DTOs;

public class GenreResponse
{
    public int GenreId { get; set; }
    public required string Name { get; set; }
    public string? Description { get; set; }
    public DateTime CreatedAt { get; set; }
}