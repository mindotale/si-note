namespace SiNote.Contracts.Notes;

public record NoteResponse(
    string Title,
    string Content,
    DateTime Created,
    DateTime? LastModified);
