using Microsoft.AspNetCore.Mvc;

namespace LiteraVerseApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class UploadController : ControllerBase
{
    private readonly IWebHostEnvironment _env;
    private const long MaxFileSize = 5 * 1024 * 1024;
    private readonly string[] AllowedExtensions = { ".jpg", ".jpeg", ".png", ".webp" };

    public UploadController(IWebHostEnvironment env)
    {
        _env = env;
    }

    [HttpPost("cover")]
    public async Task<ActionResult<string>> UploadCover(IFormFile file)
    {
        if (file == null || file.Length == 0)
            return BadRequest(new { message = "No file uploaded" });

        if (file.Length > MaxFileSize)
            return BadRequest(new { message = "File size exceeds 5MB" });

        var extension = Path.GetExtension(file.FileName).ToLowerInvariant();
        if (!AllowedExtensions.Contains(extension))
            return BadRequest(new { message = "Invalid file type. Only images allowed." });

        var fileName = $"{Guid.NewGuid()}{extension}";
        var uploadsFolder = Path.Combine(_env.WebRootPath, "uploads", "covers");
        Directory.CreateDirectory(uploadsFolder);

        var filePath = Path.Combine(uploadsFolder, fileName);

        using (var stream = new FileStream(filePath, FileMode.Create))
        {
            await file.CopyToAsync(stream);
        }

        var fileUrl = $"/uploads/covers/{fileName}";
        return Ok(new { url = fileUrl });
    }
}