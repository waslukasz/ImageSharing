using System.ComponentModel.DataAnnotations;

namespace WebAPI.Request;

public class UpdateAlbumRequest
{
    [StringLength(50, MinimumLength = 3)]
    public string? Title { get; set; }
    
    [StringLength(250, MinimumLength = 10)]
    public string? Description { get; set; }
    
    [MinLength(2)]
    [MaxLength(20)]
    public List<Guid>? Images { get; set; }
}