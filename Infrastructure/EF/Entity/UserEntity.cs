using Application_Core.Model;
using Application_Core.Model.Interface;
using Microsoft.AspNetCore.Identity;

namespace Infrastructure.EF.Entity;

public class UserEntity : IdentityUser<int>, IUser<int>
{
    public Guid Guid { get; set; }
    
    public ICollection<Album> Albums { get; set; }
    
    public ICollection<Comment> Comments { get; set; }
    
    public ICollection<Post> Posts { get; set; }
    
    public ICollection<Image> Images { get; set; }
    
    public ICollection<Reaction> Reactions { get; set; }

    public UserEntity() : base()
    {
        this.Albums = new List<Album>();
        this.Comments = new List<Comment>();
        this.Posts = new List<Post>();
        this.Images = new List<Image>();
        this.Reactions = new List<Reaction>();
        this.Guid = Guid.NewGuid();
    }

}