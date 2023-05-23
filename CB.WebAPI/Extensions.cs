using CB.Application.Exceptions;
using Microsoft.AspNetCore.Mvc;

namespace CB.WebAPI;

public static class Extensions
{
    public static void ConfigureControllers(this IServiceCollection collection)
    {
        collection
            .AddControllers()
            .ConfigureApiBehaviorOptions(opt =>
            {
                opt.InvalidModelStateResponseFactory = context =>
                {
                    var fieldErrors = context.ModelState.Keys.Select(key =>
                    {
                        var message = context.ModelState[key]?.Errors.Select(e => e.ErrorMessage).First();
                        return new FieldException(key.ToLower(), message);
                    }).ToArray();

                    var type = "https://tools.ietf.org/html/rfc7231#section-6.5.1";
                    var title = "One or more validation errors occurred.";

                    var problemDetails = new ProblemDetails()
                    {
                        Title = title,
                        Detail = null,
                        Extensions =
                        {
                            new KeyValuePair<string, object?>("fields", fieldErrors)
                        }
                    };
                        
                    return new BadRequestObjectResult(problemDetails);
                };
            });
    }
}