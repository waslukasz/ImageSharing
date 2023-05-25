using Microsoft.Build.Framework;

namespace WebAPI.Request
{
    public class CreatePostRequest
    {
        [Required]
        public IFormFile Image { get; set; } = default!;
        public bool IsHidden { get; set; } = false;
        
        public List<string> Tags { get; set; } = default!;

        [Required]
        public string Title { get; set; } = default!;

    }
}
