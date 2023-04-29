namespace Application_Core.Model.Interface;

public interface IUser
{
    ISet<Album> Albums { get; set; }
    
    ISet<Comment> Comments { get; set; }
    
    ISet<Post> Posts { get; set; }
    
    ISet<Image> Images { get; set; }
    
    ISet<Reaction> Reactions { get; set; }
}