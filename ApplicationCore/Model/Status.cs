using Application_Core.Model.Base;

namespace Application_Core.Model;

public class Status : Identity
{
    public string Name { get; set; }
    
    public ISet<Post> Posts { get; set; }

}