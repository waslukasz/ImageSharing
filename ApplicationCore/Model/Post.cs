using Application_Core.Common.Repository;

namespace Application_Core.Model;

public class Post : IUidIdentity<int>
{
    public int Id { get; set; }

    public Guid Guid { get; set; }

    public List<string> Tags { get; set; }

    public Status Status { get; set; }

    public string Title { get; set; }

    public ISet<Reaction> Reactions { get; set; }

    public Image Image { get; set; }
}