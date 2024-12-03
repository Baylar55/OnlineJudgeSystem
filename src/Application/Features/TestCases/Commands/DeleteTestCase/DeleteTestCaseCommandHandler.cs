namespace AlgoCode.Application.Features.TestCases.Commands.DeleteTestCase;

public class DeleteTestCaseCommandHandler(IApplicationDbContext context) : IRequestHandler<DeleteTestCaseCommand, ValidationResultModel>
{
    public async Task<ValidationResultModel> Handle(DeleteTestCaseCommand request, CancellationToken cancellationToken)
    {
        var entity = await context.TestCases.FindAsync([request.Id], cancellationToken) ?? throw new NotFoundException(request.Id.ToString(), nameof(TestCase));

        context.TestCases.Remove(entity);
        
        await context.SaveChangesAsync(cancellationToken);

        return new();
    }
}
