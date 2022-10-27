namespace SiNote.Domain.Entities;

public class User : AggregateRoot<Guid>
{ 
    public User(
        Guid id,
        string username,
        string email,
        byte[] passwordHash,
        byte[] passwordSalt)
        : base(id)
    {
        Username = username;
        Email = email;
        PasswordHash = passwordHash;
        PasswordSalt = passwordSalt;
    }

    public string Username { get; private set; }
    public string Email { get; private set; }

    public byte[] PasswordHash { get; private set; }
    public byte[] PasswordSalt { get; private set; }
}
