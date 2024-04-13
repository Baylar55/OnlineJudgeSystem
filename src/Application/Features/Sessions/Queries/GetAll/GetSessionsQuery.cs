namespace AlgoCode.Application.Features.Sessions.Queries.GetAll
{
    public class GetSessionsQuery : IRequest<ICollection<GetSessionsQueryResponse>>
    {
    }

    public class GetSessionsQueryResponse
    {
        public Session Session { get; set; }
        public int AcceptedQuestionsCount { get; set; }
        public int SubmittedQuestionsCount { get; set; }
        public int AcceptedSubmissionsCount { get; set; }
        public int SubmissionsCount { get; set; }

    }

    public class GetSessionsQueryHandler : IRequestHandler<GetSessionsQuery, ICollection<GetSessionsQueryResponse>>
    {
        private readonly IApplicationDbContext _context;

        public GetSessionsQueryHandler(IApplicationDbContext context) => _context = context;

        public async Task<ICollection<GetSessionsQueryResponse>> Handle(GetSessionsQuery request, CancellationToken cancellationToken)
        {
            var sessions = await _context.Sessions.ToListAsync(cancellationToken);
            var responseList = new List<GetSessionsQueryResponse>();

            foreach (var session in sessions)
            {
                var response = new GetSessionsQueryResponse
                {
                    Session = session,
                    AcceptedQuestionsCount = await CalculateNumberOfAcceptedQuestions(session.Id),
                    AcceptedSubmissionsCount = await CalculateNumberOfAcceptedSubmissions(session.Id),
                    SubmittedQuestionsCount = await CalculateNumberOfSubmittedQuestions(session.Id),
                    SubmissionsCount = await CalculateTotalNumberOfSubmissions(session.Id)
                };

                responseList.Add(response);
            }

            return responseList;
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
