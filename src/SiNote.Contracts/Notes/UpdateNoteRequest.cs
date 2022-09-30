namespace SiNote.Contracts.Notes;

public record UpdateNoteRequest(
    string Title,
    string Content);
