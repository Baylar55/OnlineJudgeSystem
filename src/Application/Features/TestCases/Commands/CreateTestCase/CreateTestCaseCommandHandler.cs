namespace AlgoCode.Application.Features.TestCases.Commands.CreateTestCase;

public class CreateTestCaseCommandHandler(IApplicationDbContext context) : IRequestHandler<CreateTestCaseCommand, ValidationResultModel>
{
    public async Task<ValidationResultModel> Handle(CreateTestCaseCommand request, CancellationToken cancellationToken)
    {
        var entity = request.Adapt<TestCase>();

        await context.TestCases.AddAsync(entity, cancellationToken);

        await context.SaveChangesAsync(cancellationToken);

        return new ValidationResultModel();
    }
}
