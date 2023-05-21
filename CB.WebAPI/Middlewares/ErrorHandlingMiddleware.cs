using System.Text.Json;
using CB.Application.Exceptions;
using CB.Application.Exceptions.Http;
using Microsoft.AspNetCore.Mvc;

namespace CB.WebAPI.Middlewares;

public class ErrorHandlingMiddleware
{
    private readonly RequestDelegate _next;

    public ErrorHandlingMiddleware(RequestDelegate next)
    {
        _next = next;
    }
    
    public async Task Invoke(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (ProblemDetailsException problemDetailsException)
        {
            await HandleExceptionAsync(context, problemDetailsException);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            var internalServerError = new InternalServerException();
            await HandleExceptionAsync(context, internalServerError);
        }
    }

    private static Task HandleExceptionAsync(HttpContext context, ProblemDetailsException exception)
    {
        context.Response.ContentType = "application/problem+json";
        context.Response.StatusCode = (int)exception.Code;

        var problemDetails = new ProblemDetails
        {
            Type = exception.Type,
            Title = exception.Title,
            Detail = exception.Detail,
            Status = (int)exception.Code,
            Extensions =
            {
                new KeyValuePair<string, object?>(nameof(exception.Fields), exception.Fields)
            }
        };
        
        var result = JsonSerializer.Serialize(problemDetails);
        
        return context.Response.WriteAsync(result);
    }
}