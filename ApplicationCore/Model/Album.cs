using Application_Core.Common.Repository;

namespace Application_Core.Model
{
    public class Album : IUidIdentity<int>
    {       
        public int Id { get; set; }
        
        public Guid Guid { get; set; }

        public string Description { get; set; }

        public string Title { get; set; }

        public ISet<Image> Images { get; set; }

        
    }
}
