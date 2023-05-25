using Microsoft.Build.Framework;

namespace WebAPI.Request;

public class DeletePostRequest
{
    [Required]
    public Guid PostGuid { get; set; }
}