using Application_Core.Common.Repository;

namespace Application_Core.Model.Interface;

public interface IUser<TKey> : IUidIdentity<int> where TKey: IEquatable<TKey>
{
    new TKey Id { get; set; }
    ISet<Album> Albums { get; set; }
    
    ISet<Comment> Comments { get; set; }
    
    ISet<Post> Posts { get; set; }
    
    ISet<Image> Images { get; set; }
    
    ISet<Reaction> Reactions { get; set; }
}