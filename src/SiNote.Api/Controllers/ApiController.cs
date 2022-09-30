using MapsterMapper;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace SiNote.Api.Controllers;

[Authorize]
[ApiController]
[Produces("application/json")]
[Route("api/[controller]")]
public class ApiController : ControllerBase
{
    protected ISender Sender { get; private init; }
    protected IMapper Mapper { get; private init; }

    protected ApiController(ISender sender, IMapper mapper)
    {
        Sender = sender;
        Mapper = mapper;
    }
}
