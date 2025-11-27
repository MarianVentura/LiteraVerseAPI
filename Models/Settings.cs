using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LiteraVerseApi.Models;

public class Settings
{
    [Key]
    public int SettingId { get; set; }

    [Required]
    public int UserId { get; set; }

    [MaxLength(20)]
    public string Theme { get; set; } = "light";

    public int FontSize { get; set; } = 16;

    public bool NotificationsEnabled { get; set; } = true;

    public bool AutoPlayNext { get; set; } = true;

    public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;

    [ForeignKey("UserId")]
    public virtual Usuarios? User { get; set; }
}