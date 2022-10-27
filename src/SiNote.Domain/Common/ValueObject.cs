namespace SiNote.Domain.Common;

public abstract class ValueObject : IEquatable<ValueObject>
{
    protected abstract IEnumerable<object> GetEqualityComponents();

    public static bool operator ==(ValueObject left, ValueObject right)
    {
        if(left is null ^ right is null)
        {
            return false;
        }

        return left is null || left.Equals(right);
    }

    public static bool operator !=(ValueObject left, ValueObject right)
    {
        return !(left == right);
    }

    public override bool Equals(object? obj)
    {
        if(obj is ValueObject valueObject)
        {
            return Equals(valueObject);
        }

        return false;
    }

    public bool Equals(ValueObject? other)
    {
        if(other is null)
        {
            return false;
        }

        return GetEqualityComponents().SequenceEqual(other.GetEqualityComponents());
    }

    public override int GetHashCode()
    {
        return GetEqualityComponents()
            .Select(x => x?.GetHashCode() ?? 0)
            .Aggregate((x, y) => x ^ y);
    }
}