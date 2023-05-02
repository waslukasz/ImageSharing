using Application_Core.Common.Repository;
using Application_Core.Model.Base;
using Application_Core.Model.Interface;

namespace Application_Core.Model
{
    public class Album : UidIdentity
    {
        public string Description { get; set; }

        public string Title { get; set; }

        public ICollection<Image> Images { get; set; }
        
        public IUser<int> User { get; set; }
        
        public int UserId { get; set; }

        public Album() : base()
        {
            this.Images = new List<Image>();
        }

    }
}
