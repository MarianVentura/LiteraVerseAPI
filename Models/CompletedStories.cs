using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LiteraVerseApi.Models;

public class CompletedStories
{
    [Key]
    public int CompletedId { get; set; }

    [Required]
    public int UserId { get; set; }

    [Required]
    public int StoryId { get; set; }

    public DateTime CompletedAt { get; set; } = DateTime.UtcNow;

    [ForeignKey("UserId")]
    public virtual Usuarios? User { get; set; }

    [ForeignKey("StoryId")]
    public virtual Stories? Story { get; set; }
}