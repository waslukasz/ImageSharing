namespace WebAPI.Response;

public class AlbumResponse
{
    public Guid Id { get; set; }
    public string Titile { get; set; }
    public string Description { get; set; }
    
    public int ImageCount { get; set; }
}