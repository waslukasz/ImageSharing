using Application_Core.Common.Identity;
using Application_Core.Common.Repository;
using Application_Core.Model.Interface;

namespace Application_Core.Model;

public class Image : IUidIdentity<int>
{
    public int Id { get; set; }
    public Guid Guid { get; set; }
    
    public string Extension { get; set; }

    public string Slug { get; set; }
    
    public string Title { get; set; }
    
    public Post Post { get; set; }

    public ICollection<Album> Albums { get; set; }
    
    public IUser<int> User { get; set; }

    public int UserId { get; set; }
    
    public Stream? Stream { get; set; }

    public Thumbnail Thumbnail { get; set; }
    public int ThumbnailId { get; set; }
    public Image()
    {
        Guid = Guid.NewGuid();
    }

    public string GetStoragePath()
    {
        return Guid.ToString() + Extension;
    }
}