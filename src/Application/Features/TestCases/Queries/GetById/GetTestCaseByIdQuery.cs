namespace AlgoCode.Application.Features.TestCases.Queries.GetById
{
    public class GetTestCaseByIdQuery : IRequest<GetTestCaseByIdQueryResponse>
    {
        public int Id { get; set; }
    }

    public class GetTestCaseByIdQueryHandler : IRequestHandler<GetTestCaseByIdQuery, GetTestCaseByIdQueryResponse>
    {
        private readonly IApplicationDbContext _context;
        public GetTestCaseByIdQueryHandler(IApplicationDbContext context) => _context = context;

        public async Task<GetTestCaseByIdQueryResponse> Handle(GetTestCaseByIdQuery request, CancellationToken cancellationToken)
        {
            var validationResult = new ValidationResultModel();

            var testCase = await _context.TestCases.FindAsync(request.Id);
            if (testCase == null)
            {
                validationResult.IsValid = false;
                validationResult.Errors.Add("Id", ["Test case not found"]);
            }
            var inp = testCase.InputParameter;
            return new GetTestCaseByIdQueryResponse
            {
                Id = testCase.Id,
                ProblemId = testCase.ProblemId,
                Inputs = testCase.InputParameter,
                ExpectedOutput = testCase.ExpectedOutput
            };
        }
    }
}
