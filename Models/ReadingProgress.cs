using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LiteraVerseApi.Models;

public class ReadingProgress
{
    [Key]
    public int ProgressId { get; set; }

    [Required]
    public int UserId { get; set; }

    [Required]
    public int StoryId { get; set; }

    [Required]
    public int ChapterId { get; set; }

    public double ScrollPosition { get; set; } = 0;

    public DateTime LastReadAt { get; set; } = DateTime.UtcNow;

    [ForeignKey("UserId")]
    public virtual Usuarios? User { get; set; }

    [ForeignKey("StoryId")]
    public virtual Stories? Story { get; set; }

    [ForeignKey("ChapterId")]
    public virtual Chapters? Chapter { get; set; }
}