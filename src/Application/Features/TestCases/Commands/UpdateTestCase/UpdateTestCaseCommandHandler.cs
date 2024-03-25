namespace AlgoCode.Application.Features.TestCases.Commands.UpdateTestCase
{
    public class UpdateTestCaseCommandHandler : IRequestHandler<UpdateTestCaseCommand, ValidationResultModel>
    {
        private readonly IApplicationDbContext _context;
        public UpdateTestCaseCommandHandler(IApplicationDbContext context) => _context = context;

        public async Task<ValidationResultModel> Handle(UpdateTestCaseCommand request, CancellationToken cancellationToken)
        {
            var response = new ValidationResultModel();

            var testCase = await _context.TestCases.FindAsync(request.Id);

            if (testCase == null)
            {
                response.Errors.Add("Id", ["TestCase Not Found."]);
                return response;
            }

            testCase.ProblemId = request.ProblemId;
            testCase.Input = request.Input;
            testCase.ExpectedOutput = request.ExpectedOutput;

            _context.TestCases.Update(testCase);
            await _context.SaveChangesAsync(cancellationToken);

            return response;
        }
    }
}
