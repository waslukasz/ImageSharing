using Application_Core.Common.Repository;

namespace Application_Core.Model;

public class Image : IUidIdentity<int>
{
    public int Id { get; set; }
    
    public Guid Guid { get; set; }
    
    public string Slug { get; set; }
    
    public string Title { get; set; }
    public Post Post { get; set; }

    public ISet<Album> Albums { get; set; }

    public string UserId { get; set; }

}