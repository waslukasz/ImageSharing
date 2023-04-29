using Application_Core.Model;
using Microsoft.AspNetCore.Identity;
using IUser = Application_Core.Model.Interface.IUser<int>;

namespace Infrastructure.EF.Entity;

public class User : IdentityUser, IUser
{
    public int Id { get; set; }
    
    public Guid Guid { get; set; }
    
    public ISet<Album> Albums { get; set; }
    
    public ISet<Comment> Comments { get; set; }
    
    public ISet<Post> Posts { get; set; }
    
    public ISet<Image> Images { get; set; }
    
    public ISet<Reaction> Reactions { get; set; }

    public User()
    {
        this.Guid = Guid.NewGuid();
    }
}