using System.ComponentModel.DataAnnotations;
using Application_Core.Common.Specification;

namespace Infrastructure.Manager.Param;

public class AlbumSearchDto
{
    public string? AlbumTitle { get; set; }
    
    public int? MaxImages { get; set; }
    
    public int? MinImages { get; set; }

    public OrderBy OrderBy { get; set; } = OrderBy.Desc;
    
}