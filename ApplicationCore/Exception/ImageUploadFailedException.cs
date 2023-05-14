using System.Net;

namespace Application_Core.Exception;

public class ImageUploadFailedException : HttpResponseException
{
    public ImageUploadFailedException(string message, HttpStatusCode statusCode) : base(message, statusCode)
    {
    }
}