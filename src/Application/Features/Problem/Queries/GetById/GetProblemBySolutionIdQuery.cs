namespace AlgoCode.Application.Features.Problem.Queries.GetById
{
    public class GetProblemBySolutionIdQuery: IRequest<Domain.Entities.Problem>
    {
        public int SolutionId { get; set; }
    }

    public class GetProblemBySolutionIdQueryHandler : IRequestHandler<GetProblemBySolutionIdQuery, Domain.Entities.Problem>
    {
        private readonly IApplicationDbContext _context;

        public GetProblemBySolutionIdQueryHandler(IApplicationDbContext context) => _context = context;

        public async Task<Domain.Entities.Problem> Handle(GetProblemBySolutionIdQuery request, CancellationToken cancellationToken)
        {
            //get problem by solution id
            var solution = await _context.Solutions
                                        .Include(x => x.Submission)
                                        .ThenInclude(x => x.User)
                                        .FirstOrDefaultAsync(x => x.Id == request.SolutionId, cancellationToken);

            var submission = await _context.Submissions.FindAsync(solution.SubmissionId);

            var problem = await _context.Problems.FindAsync(submission.ProblemId);
            return problem;
        }
    }
}
