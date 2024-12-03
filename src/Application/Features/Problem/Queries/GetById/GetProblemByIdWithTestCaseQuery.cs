namespace AlgoCode.Application.Features.Problem.Queries.GetById
{
    public record GetProblemByIdWithTestCaseQuery(int Id) : IRequest<GetProblemByIdWithTestCaseQueryResponse>;

    public record GetProblemByIdWithTestCaseQueryResponse(int Id, string Title, string Description, string MethodName, 
        string CodeTemplate, int Point, string Difficulty, AccessLevel AccessLevel, DateTimeOffset Created, 
        string? CreatedBy, DateTimeOffset? LastModified, string? LastModifiedBy, List<TestCase> TestCases, List<Tag> Tags);


    public class GetProblemByIdWithTestCaseQueryHandler(IApplicationDbContext context) : IRequestHandler<GetProblemByIdWithTestCaseQuery, GetProblemByIdWithTestCaseQueryResponse>
    {
        public async Task<GetProblemByIdWithTestCaseQueryResponse> Handle(GetProblemByIdWithTestCaseQuery request, CancellationToken cancellationToken)
        {
            var entity = await context.Problems.Include(x => x.TestCases).Include(x => x.Tags).FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken) 
                ?? throw new NotFoundException(request.Id.ToString(), nameof(Problem));

            return entity.Adapt<GetProblemByIdWithTestCaseQueryResponse>();
        }
    }
}
