using System.ComponentModel.DataAnnotations;

namespace LiteraVerseApi.Models;

public class Genres
{
    [Key]
    public int GenreId { get; set; }

    [Required]
    [MaxLength(100)]
    public string Name { get; set; } = string.Empty;

    [MaxLength(500)]
    public string? Description { get; set; }

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
}