namespace Application_Core.Model;

public class Status
{
    public string Name { get; set; }
    
    public ISet<Post> Posts { get; set; }
    
}