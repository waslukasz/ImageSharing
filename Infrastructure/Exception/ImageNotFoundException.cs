using System.Net;

namespace Infrastructure.Exception;

public class ImageNotFoundException : HttpResponseException
{
    private const string MESSAGE = "Image not found !";
    
    public ImageNotFoundException() : base(HttpStatusCode.NotFound, MESSAGE)
    {
        
    }

    public ImageNotFoundException(string message) : base(HttpStatusCode.NotFound, message)
    {
        
    }
}