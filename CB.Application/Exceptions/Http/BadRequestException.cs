using System.Net;

namespace CB.Application.Exceptions.Http;

public class BadRequestException: ProblemDetailsException
{
    public BadRequestException(string? title, string? detail, FieldException[] fields)
    : base("BAD_REQUEST_EXCEPTION", HttpStatusCode.BadRequest, title, detail, fields)
    {
    }
}