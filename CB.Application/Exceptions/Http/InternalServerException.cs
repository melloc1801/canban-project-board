using System.Net;

namespace CB.Application.Exceptions.Http;

public class InternalServerException: ProblemDetailsException
{
    public InternalServerException() : 
    base(
        "INTERNAL_SERVER_EXCEPTION",
        HttpStatusCode.InternalServerError,
        "Something went wrong",
        "Internal server error. Please try again later.",
        null
    )
    { }
}