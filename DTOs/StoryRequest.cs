using System.ComponentModel.DataAnnotations;

namespace LiteraVerseApi.DTOs;

public class StoryRequest
{
    [Required]
    public int UserId { get; set; }

    [Required]
    [MaxLength(200)]
    public required string Title { get; set; }

    [MaxLength(1000)]
    public string Synopsis { get; set; } = string.Empty;

    [MaxLength(500)]
    public string? CoverImageUrl { get; set; }

    [MaxLength(100)]
    public string? Genre { get; set; }

    [MaxLength(500)]
    public string? Tags { get; set; }

    public bool IsDraft { get; set; } = true;
}