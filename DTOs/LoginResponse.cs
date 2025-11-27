namespace LiteraVerseApi.DTOs;

public class LoginResponse
{
    public int UserId { get; set; }
    public required string UserName { get; set; }
    public required string Token { get; set; }
    public DateTime LoginDate { get; set; }
}