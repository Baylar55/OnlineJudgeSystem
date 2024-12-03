namespace AlgoCode.Application.Features.Problem.Queries.GetById;

public record GetProblemBySolutionIdQuery(int SolutionId) : IRequest<Domain.Entities.Problem>;

public class GetProblemBySolutionIdQueryHandler(IApplicationDbContext context) : IRequestHandler<GetProblemBySolutionIdQuery, Domain.Entities.Problem>
{
    public async Task<Domain.Entities.Problem> Handle(GetProblemBySolutionIdQuery request, CancellationToken cancellationToken)
    {
        var solution = await context.Solutions
                                    .Include(x => x.Submission)
                                    .ThenInclude(x => x.User)
                                    .FirstOrDefaultAsync(x => x.Id == request.SolutionId, cancellationToken)
                                    ?? throw new NotFoundException(request.SolutionId.ToString(), nameof(Solution));

        var submission = await context.Submissions.FindAsync([solution.SubmissionId], cancellationToken)
            ?? throw new NotFoundException(solution.SubmissionId.ToString(), nameof(Submission));

        var problem = await context.Problems.FindAsync([submission.ProblemId], cancellationToken)
            ?? throw new NotFoundException(submission.ProblemId.ToString(), nameof(Problem));

        return problem;
    }
}
