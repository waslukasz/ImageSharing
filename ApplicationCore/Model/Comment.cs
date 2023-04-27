using Application_Core.Common.Repository;

namespace Application_Core.Model
{
    public class Comment : IUidIdentity<int>
    {
        public int Id { get; set; }

        public Guid Guid { get; set; }

        public Post Post { get; set; }

        public string Text { get; set; }    
    }
}
