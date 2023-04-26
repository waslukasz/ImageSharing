namespace Infrastructure.Exception;

using System.Net;
using System;

public class HttpResponseException : Exception
{
    private readonly HttpStatusCode _statusCode;

    protected HttpResponseException(HttpStatusCode statusCode, string message): base(message)
    {
        this._statusCode = statusCode;
    }

    public HttpStatusCode GetStatusCode() => _statusCode;
}