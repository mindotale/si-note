using MediatR;
using Microsoft.EntityFrameworkCore;
using SiNote.Application.Common.Interfaces.Authentication;
using SiNote.Application.Common.Interfaces.Persistence;

namespace SiNote.Application.Authentication.Commands.Register;

public record RegisterCommand(string Username, string Email, string Password) : IRequest<AuthenticationResult>;

public class RegisterCommandHandler : IRequestHandler<RegisterCommand, AuthenticationResult>
{
    private readonly ISiNoteDbContext _dbContext;
    private readonly IHasher _passwordHasher;
    private readonly IJwtGenerator _jwtGenerator;

    public RegisterCommandHandler(ISiNoteDbContext dbContext, IHasher passwordHasher, IJwtGenerator jwtGenerator)
    {
        _dbContext = dbContext;
        _passwordHasher = passwordHasher;
        _jwtGenerator = jwtGenerator;
    }

    public async Task<AuthenticationResult> Handle(RegisterCommand request, CancellationToken cancellationToken)
    {
        if(await _dbContext.Users.AnyAsync(x => x.Email == request.Email))
        {
            throw new ArgumentException("Email already exist.");
        }

        var (hash, salt) = _passwordHasher.ComputeHash(request.Password);

        var user = new User(
            id: Guid.NewGuid(),
            username: request.Username,
            email: request.Email,
            passwordHash: hash,
            passwordSalt: salt);

        var token = _jwtGenerator.GenerateToken(user);

        await _dbContext.Users.AddAsync(user, cancellationToken);
        await _dbContext.SaveChangesAsync(cancellationToken);

        return new AuthenticationResult(user, token);
    }
}
