using Application_Core.Common.Repository;
using Application_Core.Model.Base;

namespace Application_Core.Model
{
    public class Album : UidIdentity
    {
        public string Description { get; set; }

        public string Title { get; set; }

        public ISet<Image> Images { get; set; }

    }
}
