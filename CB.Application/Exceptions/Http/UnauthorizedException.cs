using System.Net;

namespace CB.Application.Exceptions.Http;

public class UnauthorizedException: ProblemDetailsException
{
    public UnauthorizedException(string? title, string detail, FieldException[] fields)
        : base("UNAUTHORIZED_EXCEPTION", HttpStatusCode.Unauthorized, title, detail, fields)
    {
    }
}