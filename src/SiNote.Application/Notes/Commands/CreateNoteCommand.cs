using MapsterMapper;
using MediatR;
using SiNote.Application.Common.Interfaces.Authentication;
using SiNote.Application.Common.Interfaces.Persistence;
using SiNote.Contracts.Notes;

namespace SiNote.Application.Notes.Commands;

public record CreateNoteCommand(
    string Title,
    string Content) : IRequest<Guid>;

public class CreateNoteCommandHandler : IRequestHandler<CreateNoteCommand, Guid>
{
    private readonly ISiNoteDbContext _dbContext;
    private readonly ICurrentUserService _currentUserService;
    private readonly IMapper _mapper;

    public CreateNoteCommandHandler(
        ISiNoteDbContext dbContext,
        ICurrentUserService currentUserService,
        IMapper mapper)
    {
        _dbContext = dbContext;
        _currentUserService = currentUserService;
        _mapper = mapper;
    }

    public async Task<Guid> Handle(CreateNoteCommand request, CancellationToken cancellationToken)
    {
        var userId = _currentUserService.UserId;
        var note = _mapper.Map<Note>((Guid.NewGuid(), userId, request));

        await _dbContext.Notes.AddAsync(note, cancellationToken);
        await _dbContext.SaveChangesAsync(cancellationToken);

        return note.Id;
    }
}
