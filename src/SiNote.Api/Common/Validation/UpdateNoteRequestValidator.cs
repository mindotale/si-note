namespace SiNote.Api.Common.Validation;

public class UpdateNoteRequestValidator : AbstractValidator<UpdateNoteRequest>
{
    public UpdateNoteRequestValidator()
    {
        RuleFor(x=>x.Title).NotEmpty().MaximumLength(50);
        RuleFor(x => x.Content).MaximumLength(500);
    }
}
