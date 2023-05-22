using System.Net;
using System.Runtime.CompilerServices;

namespace Application_Core.Exception;

public class NotAuthorizedException : HttpResponseException
{
    private readonly HttpStatusCode _statusCode;
    public NotAuthorizedException() : base("You are not authorized!", HttpStatusCode.Unauthorized)
    {
        _statusCode = HttpStatusCode.Unauthorized;
    }

    public HttpStatusCode GetStatusCode() => _statusCode;
}