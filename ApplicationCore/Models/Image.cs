using Application_Core.Commons.Repository;

namespace Application_Core.Models;

public class Image : IIdentity<int>
{
    public int Id { get; set; }
    
    public Guid Guid { get; set; }
    
    public string Slug { get; set; }
    
    public string Title { get; set; }

    public ISet<Album> Albums { get; set; }

}