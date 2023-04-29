using Application_Core.Model;
using Microsoft.AspNetCore.Identity;
using IUser = Application_Core.Model.Interface.IUser;

namespace Infrastructure.EF.Entity;

public class User : IdentityUser, IUser
{
    public ISet<Album> Albums { get; set; }
    
    public ISet<Comment> Comments { get; set; }
    
    public ISet<Post> Posts { get; set; }
    
    public ISet<Image> Images { get; set; }
    
    public ISet<Reaction> Reactions { get; set; }
    
}