namespace CB.Infrastructure.Persistence;

public interface IDatabaseMigrator
{
    public void Update(string connectionString);
}