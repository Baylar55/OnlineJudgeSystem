namespace AlgoCode.Application.Features.TestCases.Commands.UpdateTestCase;

public class UpdateTestCaseCommandHandler(IApplicationDbContext context) : IRequestHandler<UpdateTestCaseCommand, ValidationResultModel>
{
    public async Task<ValidationResultModel> Handle(UpdateTestCaseCommand request, CancellationToken cancellationToken)
    {
        var testCase = await context.TestCases.FindAsync([request.Id], cancellationToken) ?? throw new NotFoundException(request.Id.ToString(), nameof(TestCase));

        testCase.ProblemId = request.ProblemId;
        testCase.InputParameter = request.Inputs;
        testCase.ExpectedOutput = request.ExpectedOutput;

        await context.SaveChangesAsync(cancellationToken);

        return new();
    }
}
