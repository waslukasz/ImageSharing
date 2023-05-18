using Microsoft.Build.Framework;

namespace WebAPI.Request
{
    public class AddReactionRequest
    {
        [Required]
        public Guid Id { get; set; }
    }
}
