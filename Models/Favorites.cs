using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LiteraVerseApi.Models;

public class Favorites
{
    [Key]
    public int FavoriteId { get; set; }

    [Required]
    public int UserId { get; set; }

    [Required]
    public int StoryId { get; set; }

    public DateTime AddedAt { get; set; } = DateTime.UtcNow;

    [ForeignKey("UserId")]
    public virtual Usuarios? User { get; set; }

    [ForeignKey("StoryId")]
    public virtual Stories? Story { get; set; }
}