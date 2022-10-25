using System.Net;

namespace MSHelper.WebApi.Exceptions;

public class ExceptionResponse
{
    public object Response { get; }
    public HttpStatusCode StatusCode { get; }

    public ExceptionResponse(object response, HttpStatusCode statusCode)
    {
        Response = response;
        StatusCode = statusCode;
    }
}