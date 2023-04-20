using CB.DatabaseMigration;

var builder = WebApplication.CreateBuilder(args);

var conn = builder.Configuration
    .GetSection("Database")
    .GetValue<string>("connectionString");

DatabaseMigrator.Update(conn);

var app = builder.Build();
app.Run();