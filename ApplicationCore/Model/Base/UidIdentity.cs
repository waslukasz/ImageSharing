using Application_Core.Common.Identity;
using Application_Core.Common.Repository;

namespace Application_Core.Model.Base;

public abstract class UidIdentity : IUidIdentity<int>
{
    public int Id { get; set; }
    
    public Guid Guid { get; set; }

    public UidIdentity()
    {
        this.Guid = Guid.NewGuid();
    }
}