using MediatR;
using Microsoft.EntityFrameworkCore;
using SiNote.Application.Common.Exceptions;
using SiNote.Application.Common.Interfaces.Authentication;
using SiNote.Application.Common.Interfaces.Persistence;

namespace SiNote.Application.Notes.Commands;

public record DeleteNoteCommand(Guid Id) : IRequest;

public class DeleteNoteCommandHandler : IRequestHandler<DeleteNoteCommand>
{
    private readonly ISiNoteDbContext _dbContext;
    private readonly ICurrentUserService _currentUserService;

    public DeleteNoteCommandHandler(ISiNoteDbContext dbContext, ICurrentUserService currentUserService)
    {
        _dbContext = dbContext;
        _currentUserService = currentUserService;
    }

    public async Task<Unit> Handle(DeleteNoteCommand request, CancellationToken cancellationToken)
    {
        var userId = _currentUserService.UserId;

        var note = await _dbContext.Notes
            .FirstOrDefaultAsync(x => x.Id == request.Id && x.UserId == userId, cancellationToken);

        if(note is null)
        {
            throw new NotFoundException(nameof(Note), request.Id);
        }

        _dbContext.Notes.Remove(note);
        await _dbContext.SaveChangesAsync(cancellationToken);

        return Unit.Value;
    }
}
