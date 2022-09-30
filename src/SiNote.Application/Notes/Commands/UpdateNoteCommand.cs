using MapsterMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using SiNote.Application.Common.Exceptions;
using SiNote.Application.Common.Interfaces.Authentication;
using SiNote.Application.Common.Interfaces.Persistence;

namespace SiNote.Application.Notes.Commands;

public record UpdateNoteCommand(Guid Id, string Title, string Content) : IRequest;

public class UpdateNoteCommandHandler : IRequestHandler<UpdateNoteCommand>
{
    private readonly ISiNoteDbContext _dbContext;
    private readonly ICurrentUserService _currentUserService;
    private readonly IMapper _mapper;

    public UpdateNoteCommandHandler(ISiNoteDbContext dbContext,
        ICurrentUserService currentUserService,
        IMapper mapper)
    {
        _dbContext = dbContext;
        _currentUserService = currentUserService;
        _mapper = mapper;
    }

    public async Task<Unit> Handle(UpdateNoteCommand request, CancellationToken cancellationToken)
    {
        var userId = _currentUserService.UserId;

        var note = await _dbContext.Notes
            .FirstOrDefaultAsync(x => x.Id == request.Id && x.UserId == userId, cancellationToken);

        if (note is null)
        {
            throw new NotFoundException(nameof(Note), request.Id);
        }

        note.Title = request.Title;
        note.Content = request.Content;

        await _dbContext.SaveChangesAsync(cancellationToken);

        return Unit.Value;
    }
}
