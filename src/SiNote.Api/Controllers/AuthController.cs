using MapsterMapper;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SiNote.Application.Authentication.Commands.Register;
using SiNote.Application.Authentication.Queries.Login;

namespace SiNote.Api.Controllers;

[AllowAnonymous]
public class AuthController : ApiController
{
    public AuthController(ISender sender, IMapper mapper)
        : base(sender, mapper) { }

    [HttpPost("Login")]
    public async Task<IActionResult> Login([FromBody] LoginRequest request)
    {
        var query = Mapper.Map<LoginQuery>(request);
        var result = await Sender.Send(query);
        var response = Mapper.Map<AuthenticationResponse>(result);
        return Ok(response);
    }

    [HttpPost("Register")]
    public async Task<IActionResult> Register([FromBody] RegisterRequest request)
    {
        var command = Mapper.Map<RegisterCommand>(request);
        var result = await Sender.Send(command);
        var response = Mapper.Map<AuthenticationResponse>(result);
        return Ok(response);
    }
}
