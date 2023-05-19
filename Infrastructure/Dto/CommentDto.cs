using Application_Core.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Dto
{
    public class CommentDto
    {        
        public Guid Guid { get; set; }
        public Guid PostGuId { get; set; }
        public string Text { get; set; }
        public Guid UserGuId { get; set; }
    }
}
