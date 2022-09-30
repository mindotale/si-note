namespace SiNote.Contracts.Notes;

public record PaginationRequestQuery(
    int Page,
    int PageSize);
