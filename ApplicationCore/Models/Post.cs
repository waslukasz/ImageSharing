using Application_Core.Commons.Repository;

namespace Application_Core.Models;

public class Post : IIdentity<int>
{
    public int Id { get; set; }
    
    public Guid Guid { get; set; }
    
    public List<string> Tags { get; set; }
    
    public Status Status { get; set; }
    
    public string Title { get; set; }
}