using System.Net;

namespace CB.Application.Exceptions.Http;

public class UnsupportedMediaTypeException: ProblemDetailsException
{
    public UnsupportedMediaTypeException(string? title, string? detail, FieldException[] fields) 
        : base("UNSUPPORTED_MEDIA_TYPE", HttpStatusCode.UnsupportedMediaType, title, detail, fields)
    {
    }
}