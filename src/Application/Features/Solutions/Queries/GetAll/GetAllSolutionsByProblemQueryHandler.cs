namespace AlgoCode.Application.Features.Solutions.Queries.GetAll
{
    public class GetAllSolutionsByProblemQueryHandler : IRequestHandler<GetAllSolutionsByProblemQuery, List<GetAllSolutionsByProblemQueryResponse>>
    {
        private readonly IApplicationDbContext _context;

        public GetAllSolutionsByProblemQueryHandler(IApplicationDbContext context) => _context = context;

        public async Task<List<GetAllSolutionsByProblemQueryResponse>> Handle(GetAllSolutionsByProblemQuery request, CancellationToken cancellationToken)
        {
            var solutions = await _context.Solutions
                                          .Include(s => s.Submission)
                                          .Include(s => s.Submission.User)
                                          .Where(s => s.Submission.ProblemId == request.ProblemId)
                                          .ToListAsync(cancellationToken);

            return solutions.Select(solution => new GetAllSolutionsByProblemQueryResponse
            {
                Id = solution.Id,
                Title = solution.Title,
                User = solution.Submission.User,
                SubmissionId = solution.SubmissionId
            }).ToList();
        }
    }


}
