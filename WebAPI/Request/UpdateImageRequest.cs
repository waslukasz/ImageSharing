namespace WebAPI.Request;

public class UpdateImageRequest : UploadImageRequest
{
    public Guid Id { get; set; }
}