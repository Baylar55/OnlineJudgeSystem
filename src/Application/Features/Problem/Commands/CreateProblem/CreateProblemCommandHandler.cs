namespace AlgoCode.Application.Features.Problem.Commands.CreateProblem;

public class CreateProblemCommandHandler(IApplicationDbContext context) : IRequestHandler<CreateProblemCommand, ValidationResultModel>
{
    public async Task<ValidationResultModel> Handle(CreateProblemCommand request, CancellationToken cancellationToken)
    {
        var validationResult = new ValidationResultModel();
        bool isExist = await context.Problems.AnyAsync(x => x.Title.Trim().ToLower() == request.Title.Trim().ToLower(), cancellationToken);

        if (isExist)
        {
            validationResult.Errors.Add("Title", ["Problem with this title already exists"]);
            return validationResult;
        }

        var problem = request.Adapt<Domain.Entities.Problem>();

        if (request.TagIds != null && request.TagIds.Count != 0)
        {
            problem.Tags = await context.Tags.Where(t => request.TagIds.Contains(t.Id)).ToListAsync(cancellationToken);
        }

        context.Problems.Add(problem);
        await context.SaveChangesAsync(cancellationToken);
        return validationResult;
    }
}
