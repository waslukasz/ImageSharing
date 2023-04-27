using System.Net;

namespace Application_Core.Exception;
public class NotFoundException : HttpResponseException
{
    private const string MESSAGE = "Not found !"; 
    private const HttpStatusCode STATUSCODE = HttpStatusCode.NotFound; 
    public NotFoundException() : base(MESSAGE, STATUSCODE) { } 
    public NotFoundException(string message) : base(message, STATUSCODE) { }
}


