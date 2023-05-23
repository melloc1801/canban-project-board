using CB.Application.Repositories;
using CB.Infrastructure.Persistence;
using CB.Infrastructure.Persistence.Repositories;

namespace CB.Infrastructure;

public static class ServiceExtensions
{
    public static void ConfigureInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        var connectionString = configuration
            .GetSection("Database")
            .GetValue<string>("connectionString");
        
        services.AddScoped<IUserRepository, UserRepository>(x => new UserRepository(connectionString));

        var migrator = new DatabaseMigrator();
        migrator.Update(connectionString);
    }
}