namespace AlgoCode.Application.Features.Problem.Commands.DeleteProblem;

public record DeleteProblemCommand(int Id) : IRequest<ValidationResultModel>;

public class DeleteProblemCommandRequest(IApplicationDbContext context) : IRequestHandler<DeleteProblemCommand, ValidationResultModel>
{
    public async Task<ValidationResultModel> Handle(DeleteProblemCommand request, CancellationToken cancellationToken)
    {
        var validationResult = new ValidationResultModel();

        var problem = await context.Problems.FindAsync([request.Id], cancellationToken);

        if (problem == null) throw new NotFoundException(request.Id.ToString(), "Problem");

        context.Problems.Remove(problem);
        
        await context.SaveChangesAsync(cancellationToken);
        
        return validationResult;
    }
}
