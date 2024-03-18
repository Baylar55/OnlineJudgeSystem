namespace AlgoCode.Application.Features.TestCases.Commands.CreateTestCase
{
    public class CreateTestCaseCommandHandler : IRequestHandler<CreateTestCaseCommand, ValidationResultModel>
    {
        private readonly IApplicationDbContext _context;
        public CreateTestCaseCommandHandler(IApplicationDbContext context) => _context = context;

        public async Task<ValidationResultModel> Handle(CreateTestCaseCommand request, CancellationToken cancellationToken)
        {
            var entity = new TestCase
            {
                ProblemId = request.ProblemId,
                Inputs = request.Input,
                ExpectedOutput = request.ExpectedOutput
            };

            _context.TestCases.Add(entity);
            await _context.SaveChangesAsync(cancellationToken);

            return new ValidationResultModel { IsValid = true };
        }
    }
}
