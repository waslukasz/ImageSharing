using Application_Core.Commons.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application_Core.Models
{
    public class Reaction : IIdentity<int>
    {
        public int Id { get; set; }

        public Post Post { get; set; }
    }
}

