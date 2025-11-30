using LiteraVerseApi.DAL;
using LiteraVerseApi.DTOs;
using LiteraVerseApi.Models;
using LiteraVerseApi.Services;
using LiteraVerseApi.Utilities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace LiteraVerseApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AuthController : ControllerBase
{
    private readonly Contexto _context;
    private readonly JwtService _jwtService;

    public AuthController(Contexto context, JwtService jwtService)
    {
        _context = context;
        _jwtService = jwtService;
    }

    [HttpPost("Login")]
    public async Task<ActionResult<LoginResponse>> Login(LoginRequest request)
    {
        var hashedPassword = PasswordHasher.HashPassword(request.Password);

        var usuario = await _context.Usuarios
            .FirstOrDefaultAsync(u =>
                u.UserName == request.UserName &&
                u.Password == hashedPassword);

        if (usuario == null)
        {
            return Unauthorized(new { message = "Usuario o contraseña incorrectos" });
        }

        var token = _jwtService.GenerateToken(usuario.UsuarioId, usuario.UserName);

        var session = new Sessions
        {
            UserId = usuario.UsuarioId,
            Token = token,
            CreatedAt = DateTime.UtcNow,
            LastActivity = DateTime.UtcNow,
            IsActive = true,
            DeviceInfo = Request.Headers.UserAgent.ToString()
        };

        _context.Sessions.Add(session);
        await _context.SaveChangesAsync();

        var response = new LoginResponse
        {
            UserId = usuario.UsuarioId,
            UserName = usuario.UserName,
            Token = token,
            LoginDate = DateTime.UtcNow
        };

        return Ok(response);
    }

    [HttpPost("Register")]
    public async Task<ActionResult<LoginResponse>> Register(RegisterRequest request)
    {
        var existingUser = await _context.Usuarios
            .FirstOrDefaultAsync(u => u.UserName == request.UserName);

        if (existingUser != null)
        {
            return BadRequest(new { message = "El nombre de usuario ya existe" });
        }

        var hashedPassword = PasswordHasher.HashPassword(request.Password);

        var newUser = new Usuarios
        {
            UserName = request.UserName,
            Password = hashedPassword
        };

        _context.Usuarios.Add(newUser);
        await _context.SaveChangesAsync();

        var token = _jwtService.GenerateToken(newUser.UsuarioId, newUser.UserName);

        var session = new Sessions
        {
            UserId = newUser.UsuarioId,
            Token = token,
            CreatedAt = DateTime.UtcNow,
            LastActivity = DateTime.UtcNow,
            IsActive = true,
            DeviceInfo = Request.Headers.UserAgent.ToString()
        };

        _context.Sessions.Add(session);
        await _context.SaveChangesAsync();

        var response = new LoginResponse
        {
            UserId = newUser.UsuarioId,
            UserName = newUser.UserName,
            Token = token,
            LoginDate = DateTime.UtcNow
        };

        return CreatedAtAction(nameof(Login), response);
    }

    [HttpPost("Logout")]
    public async Task<IActionResult> Logout([FromBody] string token)
    {
        var session = await _context.Sessions
            .FirstOrDefaultAsync(s => s.Token == token && s.IsActive);

        if (session == null)
        {
            return NotFound(new { message = "Sesión no encontrada o ya cerrada" });
        }

        session.IsActive = false;
        _context.Sessions.Update(session);
        await _context.SaveChangesAsync();

        return Ok(new { message = "Sesión cerrada exitosamente" });
    }

    [HttpPost("ValidateToken")]
    public ActionResult<object> ValidateToken([FromBody] string token)
    {
        var principal = _jwtService.ValidateToken(token);

        if (principal == null)
        {
            return Ok(new { isValid = false, message = "Token inválido o expirado" });
        }

        var userId = principal.FindFirst("userId")?.Value;

        return Ok(new { isValid = true, userId = int.Parse(userId!) });
    }

    [HttpGet("Sessions/{userId}")]
    public async Task<ActionResult<IEnumerable<SessionResponse>>> GetUserSessions(int userId)
    {
        var sessions = await _context.Sessions
            .Where(s => s.UserId == userId && s.IsActive)
            .Select(s => new SessionResponse
            {
                SessionId = s.SessionId,
                UserId = s.UserId,
                Token = s.Token,
                CreatedAt = s.CreatedAt,
                LastActivity = s.LastActivity,
                IsActive = s.IsActive,
                DeviceInfo = s.DeviceInfo
            })
            .ToListAsync();

        return Ok(sessions);
    }

    [HttpPost("LogoutAll/{userId}")]
    public async Task<IActionResult> LogoutAllSessions(int userId)
    {
        var affected = await _context.Sessions
            .Where(s => s.UserId == userId && s.IsActive)
            .ExecuteUpdateAsync(s => s.SetProperty(x => x.IsActive, false));

        return Ok(new { message = $"{affected} sesiones cerradas", sessionsClosedCount = affected });
    }
}