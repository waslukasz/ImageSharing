using Microsoft.Build.Framework;

namespace WebAPI.Request
{
    public class AddReactionRequest
    {
        [Required]
        public Guid PostId { get; set; }
    }
}
