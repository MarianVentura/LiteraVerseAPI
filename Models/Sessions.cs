using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LiteraVerseApi.Models;

public class Sessions
{
    [Key]
    public int SessionId { get; set; }

    [Required]
    public int UserId { get; set; }

    [Required]
    [MaxLength(100)]
    public string Token { get; set; } = string.Empty;

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    public DateTime? LastActivity { get; set; }

    public bool IsActive { get; set; } = true;

    [MaxLength(50)]
    public string? DeviceInfo { get; set; }

    [ForeignKey("UserId")]
    public virtual Usuarios? User { get; set; }
}