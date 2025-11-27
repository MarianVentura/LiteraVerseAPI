using System.ComponentModel.DataAnnotations;

namespace LiteraVerseApi.Models;

public class Usuarios
{
    [Key]
    public int UsuarioId { get; set; }

    [Required]
    public string UserName { get; set; } = string.Empty;

    [Required]
    public string Password { get; set; } = string.Empty;
}