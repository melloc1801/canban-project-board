using CB.Application;
using CB.Infrastructure;
using CB.WebAPI;
using CB.WebAPI.Middlewares;

var builder = WebApplication.CreateBuilder(args);

builder.Services.ConfigureControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.ConfigureInfrastructure(builder.Configuration);
builder.Services.ConfigureApplication();
var app = builder.Build();
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseMiddleware<ErrorHandlingMiddleware>();
app.UseStaticFiles();
app.UseAuthorization();
app.MapControllers();
app.Run();