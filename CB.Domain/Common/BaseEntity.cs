namespace CB.Domain.Common;

public class BaseEntity
{
    public int Id { get; }

    public BaseEntity(int id)
    {
        Id = id;
    }
}