namespace SiNote.Contracts.Notes;

public record CreateNoteRequest (
    string Title,
    string Content);
