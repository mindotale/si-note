using Mapster;
using SiNote.Application.Authentication;
using SiNote.Application.Authentication.Commands.Register;
using SiNote.Application.Authentication.Queries.Login;

namespace BubberDinner.Api.Common.Mapping;

public class AuthenticationMappingConfig : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        config.NewConfig<RegisterRequest, RegisterCommand>();
        config.NewConfig<LoginRequest, LoginQuery>();
        config.NewConfig<AuthenticationResult, AuthenticationResponse>()
            .Map(d => d.Token, s => s.Token)
            .Map(d => d, s => s.User);
    }
}
