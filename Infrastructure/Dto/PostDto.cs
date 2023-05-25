using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Dto
{
    public class PostDto
    {
        public Guid Id { get; set; }
        public string StatusName { get; set; } = default!;
        public Guid ImageId { get; set; }
        public Guid UserId { get; set; }
        public List<string> Tags { get; set; } = new();
        public string Title { get; set; }=default!;
        public int Reactions { get; set;}        
    }
}
