namespace AlgoCode.Application.Features.Problem.Queries.GetAll;

public class GetProblemsWithPaginationQueryHandler(IApplicationDbContext context, IHttpContextAccessor accessor) : IRequestHandler<GetProblemsWithPaginationQuery, GetProblemsWithPaginationQueryResponse>
{
    public async Task<GetProblemsWithPaginationQueryResponse> Handle(GetProblemsWithPaginationQuery request, CancellationToken cancellationToken)
    {
        var problemsQuery = context.Problems.AsQueryable();

        if (!string.IsNullOrEmpty(request.Title))
            problemsQuery = problemsQuery.Where(p => p.Title.Contains(request.Title));


        var problems = await problemsQuery
                            .OrderByDescending(p => p.Id)
                            .Skip((request.PageNumber - 1) * request.PageSize)
                            .Take(request.PageSize)
                            .ToListAsync(cancellationToken);


        var userId = accessor.HttpContext?.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

        var activeSessionId = await context.Sessions
                                            .Where(s => s.UserId == userId && s.IsActive)
                                            .Select(s => s.Id)
                                            .FirstOrDefaultAsync(cancellationToken);

        var userProblemStatuses = await context.UserProblemStatuses
                                                .Where(ups => ups.UserId == userId && ups.SessionId == activeSessionId)
                                                .ToListAsync(cancellationToken);

        var problemsWithStatus = problems.Select(problem => new ProblemWithStatus
        {
            Problem = problem,
            UserProblemStatus = userProblemStatuses.FirstOrDefault(ups => ups.ProblemId == problem.Id)?.Status
        }).ToList();

        var pageCount = (int)Math.Ceiling(await context.Problems.CountAsync(cancellationToken) / (decimal)request.PageSize);

        var respone = new GetProblemsWithPaginationQueryResponse
        {
            Problems = problemsWithStatus,
            PageNumber = request.PageNumber,
            PageSize = request.PageSize,
            PageCount = pageCount
        };

        foreach (var problem in problems)
        {
            var totalSubmissions = await context.Submissions
                                                .Where(s => s.ProblemId == problem.Id)
                                                .CountAsync(cancellationToken);

            var acceptedSubmissions = await context.Submissions
                                                .Where(s => s.ProblemId == problem.Id && s.Status == SubmissionStatus.Accepted)
                                                .CountAsync(cancellationToken);

            if (totalSubmissions == 0)
                respone.Problems.First(p => p.Problem.Id == problem.Id).ProblemAcceptanceRate = 0;
            else
            {
                respone.Problems.First(p => p.Problem.Id == problem.Id).ProblemAcceptanceRate = (acceptedSubmissions * 100) / totalSubmissions;
            }
        }
        return respone;
    }

}
