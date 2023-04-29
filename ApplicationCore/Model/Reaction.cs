using Application_Core.Common.Repository;
using Application_Core.Model.Base;
using Microsoft.AspNet.Identity;

namespace Application_Core.Model
{
    public class Reaction : UidIdentity
    {
        public Post Post { get; set; }
        
        public IUser User { get; set; }

    }
}

