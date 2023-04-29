using Application_Core.Common.Repository;
using Application_Core.Model.Base;

namespace Application_Core.Model
{
    public class Comment : UidIdentity
    {
        public Post Post { get; set; }

        public string Text { get; set; }

	}
}
