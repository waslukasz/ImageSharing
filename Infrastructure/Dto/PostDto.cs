using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Dto
{
    public class PostDto
    {
        public Guid Guid { get; set; }
        public string StatusName { get; set; } = default!;
        public int ImageId{ get; set; }
        public int UserId { get; set; }
        public List<string> Tags { get; set; } = new();
        public string Title { get; set; }=default!;
        //public Guid UserGuid { get; set; }
        //ToDo Coment and Reactinns 
        
    }
}
