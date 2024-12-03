namespace AlgoCode.Application.Features.Submissions.Queries.GetAll;

public record GetSubmissionsByProblemIdQuery(int ProblemId) : IRequest<ICollection<GetSubmissionsByProblemIdQueryResponse>>;

public record GetSubmissionsByProblemIdQueryResponse(int Id, string Language, string Status, DateTimeOffset SubmissionTime, double MemoryUsage, long ExecutionTime);

public class GetSubmissionsByProblemIdQueryHandler(IApplicationDbContext context) : IRequestHandler<GetSubmissionsByProblemIdQuery, ICollection<GetSubmissionsByProblemIdQueryResponse>>
{
    public async Task<ICollection<GetSubmissionsByProblemIdQueryResponse>> Handle(GetSubmissionsByProblemIdQuery request, CancellationToken cancellationToken)
    {
        int activeSessionId = await context.Sessions.Where(x => x.IsActive)
                                                    .Select(x => x.Id)
                                                    .FirstOrDefaultAsync(cancellationToken);

        var submissions = await context.Submissions
                                       .Where(x => x.ProblemId == request.ProblemId && x.SessionId == activeSessionId)
                                       .ToListAsync(cancellationToken);

        return submissions.Select(x => x.Adapt<GetSubmissionsByProblemIdQueryResponse>()).ToList();
    }
}
