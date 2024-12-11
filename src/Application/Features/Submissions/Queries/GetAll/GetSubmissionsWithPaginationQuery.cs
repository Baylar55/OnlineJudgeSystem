namespace AlgoCode.Application.Features.Submissions.Queries.GetAll;

public record GetSubmissionsWithPaginationQuery(int PageNumber = 1, int PageSize = 10) : IRequest<GetSubmissionsWithPaginationQueryResponse>;

public record GetSubmissionsWithPaginationQueryResponse(ICollection<Submission>? Submissions, List<string> ProblemTitles, int PageNumber, int PageSize, int PageCount);

public class GetSubmissionsWithPaginationQueryHandler(IApplicationDbContext context, IHttpContextAccessor accessor) : IRequestHandler<GetSubmissionsWithPaginationQuery, GetSubmissionsWithPaginationQueryResponse>
{
    public async Task<GetSubmissionsWithPaginationQueryResponse> Handle(GetSubmissionsWithPaginationQuery request, CancellationToken cancellationToken)
    {
        var activeSessionId = context.Sessions.Where(s => s.IsActive).Select(s => s.Id).FirstOrDefault();

        var userId = accessor.HttpContext?.User?.FindFirstValue(ClaimTypes.NameIdentifier);

        var submissions = await context.Submissions.Where(s => s.SessionId == activeSessionId && s.UserId == userId)
                                                   .OrderByDescending(s => s.Id)
                                                   .Skip((request.PageNumber - 1) * request.PageSize)
                                                   .Take(request.PageSize)
                                                   .Select(s => new
                                                   {
                                                       Submission = s,
                                                       ProblemTitle = s.Problem.Title
                                                   })
                                                   .ToListAsync(cancellationToken);

        var pageCount = (int)Math.Ceiling((await context.Submissions.CountAsync(cancellationToken)) / (decimal)request.PageSize);

        return new GetSubmissionsWithPaginationQueryResponse(
            Submissions: submissions.Select(s=> new Submission
            {
                Id = s.Submission.Id,
                Status = s.Submission.Status,
                ProblemId = s.Submission.ProblemId,
                Problem = s.Submission.Problem,
                Created = s.Submission.Created,
                ExecutionTime = s.Submission.ExecutionTime,
            }).ToList(),
            ProblemTitles: submissions.Select(s => s.ProblemTitle).ToList(),
            PageNumber: request.PageNumber,
            PageSize: request.PageSize,
            PageCount: pageCount
        );
    }
}
