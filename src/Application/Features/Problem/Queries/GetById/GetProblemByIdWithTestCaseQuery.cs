namespace AlgoCode.Application.Features.Problem.Queries.GetById
{
    public class GetProblemByIdWithTestCaseQuery : IRequest<GetProblemByIdWithTestCaseQueryResponse>
    {
        public int Id { get; set; }
    }

    public class GetProblemByIdWithTestCaseQueryResponse
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public string MethodName { get; set; }
        public string CodeTemplate { get; set; }
        public int Point { get; set; }
        public string Difficulty { get; set; }
        public string Status { get; set; }
        public DateTimeOffset Created { get; set; }
        public string? CreatedBy { get; set; }
        public DateTimeOffset? LastModified { get; set; }
        public string? LastModifiedBy { get; set; }
        public List<TestCase> TestCases { get; set; }
    }

    public class GetProblemByIdWithTestCaseQueryHandler : IRequestHandler<GetProblemByIdWithTestCaseQuery, GetProblemByIdWithTestCaseQueryResponse>
    {
        private readonly IApplicationDbContext _context;
        public GetProblemByIdWithTestCaseQueryHandler(IApplicationDbContext context) => _context = context;

        public async Task<GetProblemByIdWithTestCaseQueryResponse> Handle(GetProblemByIdWithTestCaseQuery request, CancellationToken cancellationToken)
        {
            var entity = await _context.Problems.Include(x => x.TestCases).FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);
            return new GetProblemByIdWithTestCaseQueryResponse
            {
                Title = entity.Title,
                Description = entity.Description,
                MethodName = entity.MethodName,
                CodeTemplate = entity.CodeTemplate,
                Point = entity.Point,
                Difficulty = entity.Difficulty.ToString(),
                Status = entity.Status.ToString(),
                Created = entity.Created,
                CreatedBy = entity.CreatedBy,
                LastModified = entity.LastModified,
                LastModifiedBy = entity.LastModifiedBy,
                TestCases = entity.TestCases.Select(x => new TestCase
                {
                    Input = x.Input,
                    ExpectedOutput = x.ExpectedOutput
                }).ToList()
            };
        }
    }
}
