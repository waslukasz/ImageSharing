using Application_Core.Common.Specification;

namespace WebAPI.Request;

public class SearchPostRequest
{
    public string? Title { get; set; }
    public List<string>? Tags { get; set; } 
    public OrderBy OrderBy { get; set; } = OrderBy.Desc;
    public Guid ImageId { get; set; }
}