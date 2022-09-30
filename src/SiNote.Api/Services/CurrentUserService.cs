using SiNote.Application.Common.Interfaces.Authentication;
using System.Security.Claims;

namespace SiNote.Api.Services;

public class CurrentUserService : ICurrentUserService
{
    private readonly IHttpContextAccessor _httpContextAccessor;

    public CurrentUserService(IHttpContextAccessor httpContextAccessor)
    {
        _httpContextAccessor = httpContextAccessor;
    }

    public Guid UserId
    {
        get
        {
            var nameIdentifier = _httpContextAccessor.HttpContext?.User?.FindFirstValue(ClaimTypes.NameIdentifier);
            Guid userId;
            if(Guid.TryParse(nameIdentifier, out userId))
            {
                return userId;
            }
            return Guid.Empty;
        }
    }
    
}
