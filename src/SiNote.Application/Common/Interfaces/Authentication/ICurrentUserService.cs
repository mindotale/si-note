namespace SiNote.Application.Common.Interfaces.Authentication;

public interface ICurrentUserService
{
    Guid UserId { get; }
}
