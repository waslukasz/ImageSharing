using Application_Core.Common.Repository;
using Application_Core.Model.Base;
using Application_Core.Model.Interface;

namespace Application_Core.Model;

public class Post : UidIdentity
{
    public ICollection<string> Tags { get; set; }

    public Status Status { get; set; }


    public int StatusId { get; set; }
    
    public string Title { get; set; }
    
    public ICollection<Comment> Comments { get; set; }

    public ICollection<Reaction> Reactions { get; set; }


    public int ImageId { get; set; }
    
    public Image Image { get; set; }

    public IUser<int> User { get; set; }

    public int UserId { get; set; }

    public Post() : base()
    {
        this.Tags = new List<string>();
        this.Comments = new List<Comment>();
        this.Reactions = new List<Reaction>();
    }
}