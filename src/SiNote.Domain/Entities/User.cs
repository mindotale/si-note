namespace SiNote.Domain.Entities;

public class User : Entity, IAuditableEntity
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

    public string Username { get; set; }
    public string Email { get; set; }
    public byte[] PasswordHash { get; set; }
    public byte[] PasswordSalt { get; set; }

    public DateTime Created { get; set; }
    public DateTime? LastModified { get; set; }
}
