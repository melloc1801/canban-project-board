namespace CB.Infrastructure.Persistence.common;

public class BaseRepository
{
    protected string _connectionString;
    
    public BaseRepository(string connectionString)
    {
        _connectionString = connectionString;
    }
}