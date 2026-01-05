namespace TodoTasks.Domain.Entities;

public abstract class Entity
{
    public int Id { get; protected set; }
    public DateTime CreatedAt { get; protected set; }
    public DateTime? UpdatedAt { get; protected set; }

    protected Entity()
    {
        CreatedAt = DateTime.UtcNow;
    }

    protected Entity(int id)
    {
        Id = id;
        CreatedAt = DateTime.UtcNow;
    }

    protected void SetUpdatedAt()
    {
        UpdatedAt = DateTime.UtcNow;
    }

    public override bool Equals(object? obj)
    {
        if (obj is not Entity other || GetType() != other.GetType())
            return false;

        return Id == other.Id;
    }

    public override int GetHashCode() => Id.GetHashCode();
}