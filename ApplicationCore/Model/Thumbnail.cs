using Application_Core.Common.Identity;

namespace Application_Core.Model;

public class Thumbnail : IUidIdentity<int>
{
    public int Id { get; set; }
    public Guid Guid { get; set; }
    public string Name { get; set; }
    public Image Image { get; set; }

    public Thumbnail()
    {
        Guid = Guid.NewGuid();
    }
}