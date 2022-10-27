namespace SiNote.Domain.Common;

public abstract class Entity<TId> : IEquatable<Entity<TId>>
    where TId : notnull
{
    protected Entity(TId id)
    {
        if(Equals(id, default(TId)))
        {
            throw new ArgumentException("The ID cannot be the default value", nameof(id));
        }
        Id = id;
    }

    public TId Id { get; private set; }

    public static bool operator ==(Entity<TId>? left, Entity<TId>? right)
    {
        return left?.Equals(right) ?? false;
    }

    public static bool operator !=(Entity<TId>? left, Entity<TId>? right)
    {
        return !(left == right);
    }

    public override bool Equals(object? obj)
    {
        return obj is Entity<TId> entity && Equals(entity); 
    }

    public bool Equals(Entity<TId>? other)
    {
        if (other is null)
        {
            return false;
        }

        return Id.Equals(other.Id);
    }

    public override int GetHashCode()
    {
        return Id.GetHashCode();
    }
}
