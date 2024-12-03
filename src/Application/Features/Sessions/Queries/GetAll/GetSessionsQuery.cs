namespace AlgoCode.Application.Features.Sessions.Queries.GetAll;

public record GetSessionsQuery() : IRequest<ICollection<GetSessionsQueryResponse>>;

public record GetSessionsQueryResponse(Session Session, int AcceptedQuestionsCount, int SubmittedQuestionsCount, int AcceptedSubmissionsCount, int SubmissionsCount);

public class GetSessionsQueryHandler(IApplicationDbContext context, IHttpContextAccessor accessor) : IRequestHandler<GetSessionsQuery, ICollection<GetSessionsQueryResponse>>
{
    public async Task<ICollection<GetSessionsQueryResponse>> Handle(GetSessionsQuery request, CancellationToken cancellationToken)
    {
        var userId = accessor.HttpContext?.User?.FindFirstValue(ClaimTypes.NameIdentifier);

        var sessions = await context.Sessions.Where(s => s.UserId == userId).ToListAsync(cancellationToken);

        var responses = new List<GetSessionsQueryResponse>();

        foreach (var session in sessions)
        {
            var acceptedQuestions = await CalculateNumberOfAcceptedQuestions(session.Id);
            var submittedQuestions = await CalculateNumberOfSubmittedQuestions(session.Id);
            var acceptedSubmissions = await CalculateNumberOfAcceptedSubmissions(session.Id);
            var totalSubmissions = await CalculateTotalNumberOfSubmissions(session.Id);

            responses.Add(new GetSessionsQueryResponse(session, acceptedQuestions, submittedQuestions, acceptedSubmissions, totalSubmissions));
        }

        return responses;
    }

    private async Task<int> CalculateNumberOfAcceptedQuestions(int sessionId)
    {
        return await context.UserProblemStatuses
                             .CountAsync(ups => ups.SessionId == sessionId && ups.Status == ProblemStatus.Solved);
    }
    private async Task<int> CalculateNumberOfAcceptedSubmissions(int sessionId)
    {
        return await context.Submissions
                             .CountAsync(s => s.SessionId == sessionId && s.Status == SubmissionStatus.Accepted);
    }

    private async Task<int> CalculateNumberOfSubmittedQuestions(int sessionId)
    {
        return await context.UserProblemStatuses
                             .CountAsync(ups => ups.SessionId == sessionId);
    }

    private async Task<int> CalculateTotalNumberOfSubmissions(int sessionId)
    {
        return await context.Submissions
                             .CountAsync(ups => ups.SessionId == sessionId);
    }
}
