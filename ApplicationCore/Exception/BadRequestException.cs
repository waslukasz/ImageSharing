using System.Net;

namespace Application_Core.Exception;

public class BadRequestException : HttpResponseException
{
    private readonly HttpStatusCode _statusCode;

    public BadRequestException(string message, HttpStatusCode statusCode) : base(message, statusCode)
    {
        this._statusCode = statusCode;
    }

    public HttpStatusCode GetStatusCode() => _statusCode;
}