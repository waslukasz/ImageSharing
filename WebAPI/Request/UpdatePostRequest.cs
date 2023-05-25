using Microsoft.Build.Framework;

namespace WebAPI.Request;

public class UpdatePostRequest
{
    [Required]
    public Guid PostGuid { get; set; }
    public bool IsHidden { get; set; }
    public IFormFile? Image { get; set; }
    public List<string>? Tags { get; set; }

    public string? Title { get; set; }
}