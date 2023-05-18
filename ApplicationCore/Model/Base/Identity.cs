using Application_Core.Common.Identity;
using Application_Core.Common.Repository;

namespace Application_Core.Model.Base;

public abstract class Identity : IIdentity<int>
{
    public int Id { get; set; }

}