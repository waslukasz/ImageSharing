namespace WebAPI.Response;

public class ImageResponse
{
    public Guid Id { get; set; }
    
    public string Name { get; set; }

    public string Extension { get; set; }
    
    public string DownloadUrl { get; set; }
}