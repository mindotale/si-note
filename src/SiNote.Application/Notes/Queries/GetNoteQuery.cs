using MapsterMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using SiNote.Application.Common.Exceptions;
using SiNote.Application.Common.Interfaces.Authentication;
using SiNote.Application.Common.Interfaces.Persistence;
using SiNote.Contracts.Notes;

namespace SiNote.Application.Notes.Queries;

public record GetNoteQuery(Guid Id) : IRequest<NoteResponse>;

public class GetNoteQueryHandler : IRequestHandler<GetNoteQuery, NoteResponse>
{
    private readonly ISiNoteDbContext _dbContext;
    private readonly ICurrentUserService _currentUserService;
    private readonly IMapper _mapper;

    public GetNoteQueryHandler(ISiNoteDbContext dbContext, ICurrentUserService currentUserService, IMapper mapper)
    {
        _dbContext = dbContext;
        _currentUserService = currentUserService;
        _mapper = mapper;
    }

    public async Task<NoteResponse> Handle(GetNoteQuery request, CancellationToken cancellationToken)
    {
        var userId = _currentUserService.UserId;
        var note = await _dbContext.Notes
            .AsNoTracking()
            .FirstOrDefaultAsync(x => x.Id == request.Id && x.UserId == userId, cancellationToken);
        
        if(note is null)
        {
            throw new NotFoundException(nameof(Note), request.Id);
        }

        return _mapper.Map<NoteResponse>(note);
    }
}
