using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LiteraVerseApi.Models;

public class Stories
{
    [Key]
    public int StoryId { get; set; }

    [Required]
    public int UserId { get; set; }

    [Required]
    [MaxLength(200)]
    public string Title { get; set; } = string.Empty;

    [MaxLength(1000)]
    public string Synopsis { get; set; } = string.Empty;

    [MaxLength(500)]
    public string? CoverImageUrl { get; set; }

    [Required]
    public bool IsDraft { get; set; } = true;

    public bool IsPublished { get; set; } = false;

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    public DateTime? PublishedAt { get; set; }

    public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;

    public int ViewCount { get; set; } = 0;

    [MaxLength(100)]
    public string? Genre { get; set; }

    [MaxLength(500)]
    public string? Tags { get; set; }

    [ForeignKey("UserId")]
    public virtual Usuarios? User { get; set; }

    public virtual ICollection<Chapters> Chapters { get; set; } = new List<Chapters>();
}