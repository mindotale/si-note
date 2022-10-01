namespace SiNote.Api.Common.Validation;

public class LoginRequestValidator : AbstractValidator<LoginRequest>
{
    public LoginRequestValidator()
    {
        RuleFor(x=>x.Password).MinimumLength(6);
        RuleFor(x=>x.Email).NotEmpty().EmailAddress();
    }
}
