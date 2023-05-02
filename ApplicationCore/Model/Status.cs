using Application_Core.Model.Base;

namespace Application_Core.Model;

public class Status : Identity
{
    public string Name { get; set; }
    
    public ICollection<Post> Posts { get; set; }

    public Status()
    {
        this.Posts = new List<Post>();
    }

}