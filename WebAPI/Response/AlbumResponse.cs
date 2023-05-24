namespace WebAPI.Response;

public class AlbumResponse
{
    public Guid Id { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    public int ImageCount { get; set; }
    public object User { get; set; }
}