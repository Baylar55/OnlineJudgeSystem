namespace AlgoCode.Application.Features.Solutions.Queries.GetAll;

public class GetAllSolutionsByProblemQueryHandler(IApplicationDbContext context) : IRequestHandler<GetAllSolutionsByProblemQuery, List<GetAllSolutionsByProblemQueryResponse>>
{
    public async Task<List<GetAllSolutionsByProblemQueryResponse>> Handle(GetAllSolutionsByProblemQuery request, CancellationToken cancellationToken)
    {
        var solutions = await context.Solutions
                                      .Include(s => s.Submission)
                                      .Include(s => s.Submission.User)
                                      .Where(s => s.Submission.ProblemId == request.ProblemId)
                                      .ToListAsync(cancellationToken);

        return solutions.Adapt<List<GetAllSolutionsByProblemQueryResponse>>();
    }
}
