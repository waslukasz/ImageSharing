using Application_Core.Common.Repository;

namespace Application_Core.Model;

public class Post : IUidIdentity<int>
{
    public int Id { get; set; }

    public Guid Guid { get; set; }

    public string Tags { get; set; }

    public string StatusName { get; set; }
    public Status Status { get; set; }

    public string Title { get; set; }

    public List<Comment> Comment { get; set; }

    public ISet<Reaction> Reactions { get; set; }
   
    public int ImageId { get; set; }
    public Image Image { get; set; }


}