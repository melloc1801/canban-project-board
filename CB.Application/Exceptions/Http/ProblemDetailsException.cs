using System.Net;
using System.Text.Json.Serialization;

namespace CB.Application.Exceptions.Http;

[Serializable]
public class ProblemDetailsException: Exception
{
    public string Type { get; }
    public HttpStatusCode Code { get; }
    public string? Title { get; }
    public string? Detail { get; }
    public FieldException[] Fields { get; }

    public ProblemDetailsException(string type, HttpStatusCode code, string? title, string? detail, FieldException[] fields)
    {
        Type = type;
        Code = code;
        Title = title;
        Detail = detail;
        Fields = fields;
    }
}