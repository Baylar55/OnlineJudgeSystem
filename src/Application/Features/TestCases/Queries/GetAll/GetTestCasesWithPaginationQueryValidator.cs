namespace AlgoCode.Application.Features.TestCases.Queries.GetAll;

public class GetTestCasesWithPaginationQueryValidator : AbstractValidator<GetTestCasesWithPaginationQuery>
{
    public GetTestCasesWithPaginationQueryValidator()
    {
        RuleFor(p => p.PageNumber)
            .GreaterThan(0).WithMessage("Page number should be greater than 0.");
        RuleFor(p => p.PageSize)
            .GreaterThan(0).WithMessage("Page size should be greater than 0.");
    }
}
