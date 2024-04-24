namespace AlgoCode.Application.Features.Sessions.Queries.GetById
{
    public class GetSessionDetailsByIdQuery : IRequest<GetSessionDetailsByIdQueryResponse> { }

    public class GetSessionDetailsByIdQueryResponse
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public int AllProblemsCount { get; set; }
        public int SolvedProblemsCount { get; set; }
        public int AllEasyProblemsCount { get; set; }
        public int AllMediumProblemsCount { get; set; }
        public int AllHardProblemsCount { get; set; }
        public int SolvedEasyProblemsCount { get; set; }
        public int SolvedMediumProblemsCount { get; set; }
        public int SolvedHardProblemsCount { get; set; }
    }

    public class GetSessionDetailsByIdQueryHandler : IRequestHandler<GetSessionDetailsByIdQuery, GetSessionDetailsByIdQueryResponse>
    {
        private readonly IApplicationDbContext _context;

        public GetSessionDetailsByIdQueryHandler(IApplicationDbContext context) => _context = context;

        public async Task<GetSessionDetailsByIdQueryResponse> Handle(GetSessionDetailsByIdQuery request, CancellationToken cancellationToken)
        {
            var session = await _context.Sessions
                                        .Include(s => s.Submissions)
                                        .ThenInclude(sub => sub.Problem)
                                        .ThenInclude(p => p.UserProblemStatuses)
                                        .FirstOrDefaultAsync(s => s.IsActive, cancellationToken);

            if (session == null)
                return null;

            var userProblemStatuses = session.Submissions.SelectMany(sub => sub.Problem.UserProblemStatuses);
            var solvedProblemsCount = userProblemStatuses.Count(u => u.Status == ProblemStatus.Solved);

            var solvedEasyProblemsCount = userProblemStatuses.Count(u => u.Status == ProblemStatus.Solved && u.Problem.Difficulty == ProblemDifficulty.Easy);
            var solvedMediumProblemsCount = userProblemStatuses.Count(u => u.Status == ProblemStatus.Solved && u.Problem.Difficulty == ProblemDifficulty.Medium);
            var solvedHardProblemsCount = userProblemStatuses.Count(u => u.Status == ProblemStatus.Solved && u.Problem.Difficulty == ProblemDifficulty.Hard);

            var allEasyProblemsCount = _context.Problems.Count(p => p.Difficulty == ProblemDifficulty.Easy);
            var allMediumProblemsCount = _context.Problems.Count(p => p.Difficulty == ProblemDifficulty.Medium);
            var allHardProblemsCount = _context.Problems.Count(p => p.Difficulty == ProblemDifficulty.Hard);

            var response = new GetSessionDetailsByIdQueryResponse
            {
                Id = session.Id,
                Title = session.Title,
                AllProblemsCount = _context.Problems.Count(),
                SolvedProblemsCount = solvedProblemsCount,
                AllEasyProblemsCount = allEasyProblemsCount,
                AllMediumProblemsCount = allMediumProblemsCount,
                AllHardProblemsCount = allHardProblemsCount,
                SolvedEasyProblemsCount = solvedEasyProblemsCount,
                SolvedMediumProblemsCount = solvedMediumProblemsCount,
                SolvedHardProblemsCount = solvedHardProblemsCount
            };

            return response;
        }
    }


}

