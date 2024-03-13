namespace AlgoCode.Application.Features.Tags.Queries
{
    public class GetTagsWithPaginationQueryValidator : AbstractValidator<GetTagsWithPaginationQuery>
    {
        public GetTagsWithPaginationQueryValidator()
        {
            RuleFor(p => p.PageNumber)
                .GreaterThan(0).WithMessage("Pagenumber must be greater than 0.");
            RuleFor(p => p.PageSize)
                .GreaterThan(0).WithMessage("Pagesize must be greater than 0.");
        }
    }
}
