namespace SiNote.Api.Common.Validation;

public class CreateNoteRequestValidator : AbstractValidator<CreateNoteRequest>
{
    public CreateNoteRequestValidator()
    {
        RuleFor(x=>x.Title).NotEmpty().MaximumLength(50);
        RuleFor(x => x.Content).MaximumLength(500);
    }
}
