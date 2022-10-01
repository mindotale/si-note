namespace SiNote.Api.Common.Validation;

public class RegisterRequestValidator : AbstractValidator<RegisterRequest>
{
    public RegisterRequestValidator()
    {
        RuleFor(x=>x.Username).MinimumLength(3).MaximumLength(60);
        RuleFor(x=>x.Password).MinimumLength(6);
        RuleFor(x=>x.Email).NotEmpty().EmailAddress();
    }
}
