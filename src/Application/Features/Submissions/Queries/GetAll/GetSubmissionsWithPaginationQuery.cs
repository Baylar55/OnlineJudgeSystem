namespace AlgoCode.Application.Features.Submissions.Queries.GetAll;

public record GetSubmissionsWithPaginationQuery(int PageNumber = 1, int PageSize = 10) : IRequest<GetSubmissionsWithPaginationQueryResponse>;

public record GetSubmissionsWithPaginationQueryResponse(ICollection<Submission>? Submissions, List<string> ProblemTitles, int PageNumber, int PageSize, int PageCount);

public class GetSubmissionsWithPaginationQueryHandler(IApplicationDbContext context) : IRequestHandler<GetSubmissionsWithPaginationQuery, GetSubmissionsWithPaginationQueryResponse>
{
    public async Task<GetSubmissionsWithPaginationQueryResponse> Handle(GetSubmissionsWithPaginationQuery request, CancellationToken cancellationToken)
    {
        var activeSessionId = context.Sessions.Where(s => s.IsActive).Select(s => s.Id).FirstOrDefault();

        var submissions = await context.Submissions.Where(s => s.SessionId == activeSessionId)
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
            Submissions: submissions.Adapt<List<Submission>>(),
            ProblemTitles: submissions.Select(s => s.ProblemTitle).ToList(),
            PageNumber: request.PageNumber,
            PageSize: request.PageSize,
            PageCount: pageCount
        );
    }
}
