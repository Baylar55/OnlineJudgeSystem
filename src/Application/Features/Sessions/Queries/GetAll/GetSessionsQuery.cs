namespace AlgoCode.Application.Features.Sessions.Queries.GetAll
{
    public record GetSessionsQuery() : IRequest<ICollection<GetSessionsQueryResponse>>;

    public record GetSessionsQueryResponse(Session Session, int AcceptedQuestionsCount, int SubmittedQuestionsCount, int AcceptedSubmissionsCount, int SubmissionsCount);

    public class GetSessionsQueryHandler : IRequestHandler<GetSessionsQuery, ICollection<GetSessionsQueryResponse>>
    {
        private readonly IApplicationDbContext _context;
        private readonly IHttpContextAccessor _accessor;

        public GetSessionsQueryHandler(IApplicationDbContext context, IHttpContextAccessor accessor)
        {
            _accessor = accessor;
            _context = context;
        }

        public async Task<ICollection<GetSessionsQueryResponse>> Handle(GetSessionsQuery request, CancellationToken cancellationToken)
        {
            var userId = _accessor.HttpContext?.User?.FindFirstValue(ClaimTypes.NameIdentifier);

            var sessions = await _context.Sessions.Where(s => s.UserId == userId).ToListAsync(cancellationToken);

            var responseTasks = sessions.Select(async x => new GetSessionsQueryResponse(x,
                await CalculateNumberOfAcceptedQuestions(x.Id),
                await CalculateNumberOfSubmittedQuestions(x.Id),
                await CalculateNumberOfAcceptedSubmissions(x.Id),
                await CalculateTotalNumberOfSubmissions(x.Id)
            )).ToList();

            return await Task.WhenAll(responseTasks);
        }

        private async Task<int> CalculateNumberOfAcceptedQuestions(int sessionId)
        {
            return await _context.UserProblemStatuses
                                 .CountAsync(ups => ups.SessionId == sessionId && ups.Status == ProblemStatus.Solved);
        }
        private async Task<int> CalculateNumberOfAcceptedSubmissions(int sessionId)
        {
            return await _context.Submissions
                                 .CountAsync(s => s.SessionId == sessionId && s.Status == SubmissionStatus.Accepted);
        }

        private async Task<int> CalculateNumberOfSubmittedQuestions(int sessionId)
        {
            return await _context.UserProblemStatuses
                                 .CountAsync(ups => ups.SessionId == sessionId);
        }

        private async Task<int> CalculateTotalNumberOfSubmissions(int sessionId)
        {
            return await _context.Submissions
                                 .CountAsync(ups => ups.SessionId == sessionId);
        }
    }
}
