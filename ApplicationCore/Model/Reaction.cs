using Application_Core.Common.Repository;

namespace Application_Core.Model
{
    public class Reaction : IUidIdentity<int>
    {
        public int Id { get; set; }
        
        public Guid Guid { get; set; }

        public Post Post { get; set; }
    }
}

