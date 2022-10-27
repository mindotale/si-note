namespace SiNote.Domain.Entities;

public class Note : AggregateRoot<Guid>
{
    private readonly List<Guid> _tagIds = new();

    public static Note Create(
        User user,
        string title,
        string content)
    {
        return new(user, title, content);
    }

    private Note(
        User user,
        string title,
        string content)
        :base(Guid.NewGuid())
    {
        UserId = user.Id;
        Title = title;
        Content = content;
    }

    public Guid UserId { get; private set; }

    public string Title { get; private set; }

    public string Content { get; private set; }

    public bool IsPinned { get; private set; }

    public bool IsArchived { get; private set; }

    public IReadOnlyCollection<Guid> TagIds => _tagIds;

    public void ChangeTitle(string title)
    {
        if (string.IsNullOrEmpty(title))
        {
            throw new ArgumentException("Note title can't be empty");
        }
        Title = title;
    }

    public void ChangeContent(string content)
    { 
        if(string.IsNullOrEmpty(content))
        {
            throw new ArgumentException("Note content can't be empty");
        }  
        Content = content;
    }

    public void Pin()
    {
        if(IsArchived)
        {
            Unarchive();
        }
        IsPinned = true;
    }

    public void Unpin()
    {
        IsPinned = false;
    }

    public void Archive()
    {
        if(IsPinned)
        {
            Unpin();
        }
        IsArchived = true;
    }

    public void Unarchive()
    {
        IsArchived = false;
    }

    public void AddTag(Tag tag)
    {
        if(UserId != tag.UserId)
        {
            throw new ArgumentException("Invalid user tag");
        }

        if(_tagIds.Contains(tag.Id))
        {
            throw new ArgumentException("Note already has the tag");
        }
        _tagIds.Add(tag.Id);
    }

    public void RemoveTag(Guid tagId)
    {
        if(!_tagIds.Contains(tagId))
        {
            throw new ArgumentException("Note doesn't contain the tag");
        }
        _tagIds.Remove(tagId);
    }
}
