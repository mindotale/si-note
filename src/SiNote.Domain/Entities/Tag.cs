namespace SiNote.Domain.Entities;

public class Tag : AggregateRoot<Guid>
{
    public static Tag Create(User user, string value)
    {
        return new(user, value);
    }

    public Tag(User user, string value) : base(Guid.NewGuid())
    {
        UserId = user.Id;
        ChangeValue(value);
    }

    public Guid UserId { get; private set; }

    public string Value { get; private set; } = null!;

    public void ChangeValue(string value)
    {
        if(string.IsNullOrEmpty(value))
        {
            throw new ArgumentNullException($"Tag value can't be empty.");
        }

        Value = value;
    }
}
