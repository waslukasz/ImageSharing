namespace WebAPI.Response;

public class PostResponse
{
    public Guid Id { get; set; }
    
    public string Title { get; set; }

    public UserResponse User { get; set; }
    
    public List<string> Tags { get; set; }
    
    public string Status { get; set; }
    
    public ImageResponse Image { get; set; }
    
    public ThumbnailResponse Thumbnail { get; set; }
}