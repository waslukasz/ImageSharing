namespace WebAPI.Request
{
    public class CreatePostRequest
    {
        public IFormFile Image { get; set; } = default!;
        public bool isHidden { get; set; } = false;
        public List<string> Tags { get; set; } = new();
        public string Title { get; set; } = default!;

    }
}
