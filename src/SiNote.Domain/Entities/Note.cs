namespace SiNote.Domain.Entities;

public class Note : Entity, IAuditableEntity
{
    public Note(
        Guid id,
        Guid userId,
        string title,
        string content)
        :base(id)
    {
        UserId = userId;
        Title = title;
        Content = content;
    }

    public Guid UserId { get; private set; }
    public string Title { get; set; }
    public string Content { get; set; }

    public DateTime Created { get; set; }
    public DateTime? LastModified { get; set; }
}
