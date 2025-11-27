using System.ComponentModel.DataAnnotations;

sing System.ComponentModel.DataAnnotations;

namespace LiteraVerseApi.Models;

public class PendingSync
{
    [Key]
    public int SyncId { get; set; }

    [Required]
    public int UserId { get; set; }

    [Required]
    [MaxLength(50)]
    public string EntityType { get; set; } = string.Empty;

    [Required]
    public int EntityId { get; set; }

    [Required]
    [MaxLength(20)]
    public string Action { get; set; } = string.Empty;

    [Required]
    public string Data { get; set; } = string.Empty;

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    public bool IsSynced { get; set; } = false;
}