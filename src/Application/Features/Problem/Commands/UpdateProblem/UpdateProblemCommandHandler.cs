namespace AlgoCode.Application.Features.Problem.Commands.UpdateProblem;

public class UpdateProblemCommandHandler(IApplicationDbContext context) : IRequestHandler<UpdateProblemCommand, ValidationResultModel>
{
    public async Task<ValidationResultModel> Handle(UpdateProblemCommand request, CancellationToken cancellationToken)
    {
        var validationResult = new ValidationResultModel();
        var problem = await context.Problems.Include(p => p.Tags).FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);
        
        if (problem == null)
        {
            validationResult.Errors.Add("Problem", ["Problem not found"]);
            return validationResult;
        }

        request.Adapt(problem);

        problem.Tags?.Clear();
        
        problem.Tags = await context.Tags.Where(x => request.TagIds.Contains(x.Id)).ToListAsync(cancellationToken);
        
        await context.SaveChangesAsync(cancellationToken);
        
        return validationResult;
    }
}
