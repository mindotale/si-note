using MediatR;
using Microsoft.EntityFrameworkCore;
using SiNote.Application.Common.Interfaces.Authentication;
using SiNote.Application.Common.Interfaces.Persistence;
using System.Security.Cryptography;

namespace SiNote.Application.Authentication.Queries.Login;

public record LoginQuery(string Email, string Password) : IRequest<AuthenticationResult>;

public class LoginQueryHandler : IRequestHandler<LoginQuery, AuthenticationResult>
{
    private readonly ISiNoteDbContext _dbContext;
    private readonly IHasher _passwordHasher;
    private readonly IJwtGenerator _jwtGenerator;

    public LoginQueryHandler(ISiNoteDbContext dbContext, IHasher passwordHasher, IJwtGenerator jwtGenerator)
    {
        _dbContext = dbContext;
        _passwordHasher = passwordHasher;
        _jwtGenerator = jwtGenerator;
    }

    public async Task<AuthenticationResult> Handle(LoginQuery request, CancellationToken cancellationToken)
    {
        var user = await _dbContext.Users.FirstOrDefaultAsync(x => x.Email == request.Email);

        if(user is null)
        {
            throw new ArgumentException("User with this email does not exist.");
        }

        if(WrongPassword(request.Password, user.PasswordHash, user.PasswordSalt))
        {
            throw new ArgumentException("Wrong password.");
        }

        var token = _jwtGenerator.GenerateToken(user);

        return new AuthenticationResult(user, token);
    }

    private bool WrongPassword(string password, byte[] hash, byte[] salt)
    {
        var computedHash = _passwordHasher.ComputeHash(password, salt);
        return !hash.SequenceEqual(computedHash);
    }
}
