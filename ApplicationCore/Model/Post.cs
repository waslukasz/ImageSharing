using Application_Core.Common.Repository;
using Application_Core.Model.Base;
using Application_Core.Model.Interface;

namespace Application_Core.Model;

public class Post : UidIdentity
{
    public string Tags { get; set; }

    public string StatusName { get; set; }
    
    public Status Status { get; set; }

    public string Title { get; set; }
    
    public ISet<Comment> Comments { get; set; }

    public ISet<Reaction> Reactions { get; set; }
   
    public int ImageId { get; set; }
    
    public Image Image { get; set; }

    public IUser User { get; set; }
}