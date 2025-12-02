using System.ComponentModel.DataAnnotations;

namespace LiteraVerseApi.DTOs;

public class ReadingProgressRequest
{
    [Required]
    public int UserId { get; set; }

    [Required]
    public int StoryId { get; set; }

    [Required]
    public int ChapterId { get; set; }

    [Range(0, 1)]
    public double ScrollPosition { get; set; } = 0;
}