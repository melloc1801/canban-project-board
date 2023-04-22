using System.Reflection;
using DbUp;

namespace CB.Infrastructure.Persistence;

public static class DatabaseMigrator
{
    public static void Update(string connectionString)
    {
        EnsureDatabase.For.PostgresqlDatabase(connectionString);
            
        var updater = DeployChanges.To
            .PostgresqlDatabase(connectionString)
            .WithScriptsEmbeddedInAssembly(Assembly.GetExecutingAssembly())
            .WithTransaction()
            .LogScriptOutput()
            .Build();
        updater.PerformUpgrade();
    }
}