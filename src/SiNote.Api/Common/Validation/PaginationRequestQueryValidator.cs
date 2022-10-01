namespace SiNote.Api.Common.Validation;

public class PaginationRequestQueryValidator : AbstractValidator<PaginationRequestQuery>
{
    public PaginationRequestQueryValidator()
    {
        RuleFor(x=>x.Page).GreaterThanOrEqualTo(1);
        RuleFor(x=>x.PageSize).GreaterThanOrEqualTo(1);
    }
}
