using Application_Core.Model.Base;
using Application_Core.Model.Interface;

namespace Application_Core.Model
{
    public class Reaction : UidIdentity
    {
        public Post Post { get; set; }

        public int PostId { get; set; }
        
        public IUser<int> User { get; set; }

        public int UserId { get; set; }
    }
}

