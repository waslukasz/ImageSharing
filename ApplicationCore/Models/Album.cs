using Application_Core.Commons.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

namespace Application_Core.Models
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
