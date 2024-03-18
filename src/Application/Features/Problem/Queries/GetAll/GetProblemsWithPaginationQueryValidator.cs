namespace AlgoCode.Application.Features.Problem.Queries.GetAll
{
    public class GetProblemsWithPaginationQueryValidator : AbstractValidator<GetProblemsWithPaginationQuery>
    {
        public GetProblemsWithPaginationQueryValidator()
        {
            RuleFor(p => p.PageNumber)
                .GreaterThan(0).WithMessage("Page number must be greater than 0.");
            RuleFor(p => p.PageSize)
                .GreaterThan(0).WithMessage("Page size must be greater than 0.");
        }
    }
}
