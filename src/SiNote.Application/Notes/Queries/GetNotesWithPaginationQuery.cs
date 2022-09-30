using Mapster;
using MapsterMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using SiNote.Application.Common.Interfaces.Authentication;
using SiNote.Application.Common.Interfaces.Persistence;
using SiNote.Contracts.Notes;

namespace SiNote.Application.Notes.Queries;

public record GetNotesWithPaginationQuery(int Page, int PageSize) : IRequest<IEnumerable<NoteResponse>>;

public class GetNotesWithPaginationQueryHandler :
    IRequestHandler<GetNotesWithPaginationQuery, IEnumerable<NoteResponse>>
{
    private readonly ISiNoteDbContext _dbContext;
    private readonly ICurrentUserService _currentUserService;
    private readonly IMapper _mapper;

    public GetNotesWithPaginationQueryHandler(ISiNoteDbContext dbContext,
        ICurrentUserService currentUserService,
        IMapper mapper)
    {
        _dbContext = dbContext;
        _currentUserService = currentUserService;
        _mapper = mapper;
    }

    public async Task<IEnumerable<NoteResponse>> Handle(
        GetNotesWithPaginationQuery request,
        CancellationToken cancellationToken)
    {
        var userId = _currentUserService.UserId;
        var notesToSkip = (request.Page - 1) * request.PageSize;
        var notesResponse = await _dbContext.Notes
            .AsNoTracking()
            .Where(x => x.UserId == userId)
            .Skip(notesToSkip)
            .Take(request.PageSize)
            .ProjectToType<NoteResponse>()
            .ToArrayAsync();

        return notesResponse;
    }
}
