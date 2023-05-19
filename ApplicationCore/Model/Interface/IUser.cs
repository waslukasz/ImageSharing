using Application_Core.Common.Identity;
using Application_Core.Common.Repository;

namespace Application_Core.Model.Interface;

public interface IUser<TKey> : IUidIdentity<TKey> where TKey: IEquatable<TKey>
{
    TKey Id { get; set; }
    ICollection<Album> Albums { get; set; }
    
    ICollection<Comment> Comments { get; set; }
    
    ICollection<Post> Posts { get; set; }
    
    ICollection<Image> Images { get; set; }
    
    ICollection<Reaction> Reactions { get; set; }
}