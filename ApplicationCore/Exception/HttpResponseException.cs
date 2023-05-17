using System.Net;

namespace Application_Core.Exception;
public class HttpResponseException : System.Exception
{ 
    private readonly HttpStatusCode _statusCode;
    protected HttpResponseException(string message, HttpStatusCode statusCode): base(message)
    { 
        this._statusCode = statusCode;
        
    }
    public HttpStatusCode GetStatusCode() => _statusCode;
}


