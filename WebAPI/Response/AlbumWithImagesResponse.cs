namespace WebAPI.Response;

public class AlbumWithImagesResponse
{
    public Guid Id { get; set; }
    
    public UserResponse User { get; set; }
    
    public string Title { get; set; }
    
    public string Description { get; set; }
    
    public List<ImageResponse> Images { get; set; }
}