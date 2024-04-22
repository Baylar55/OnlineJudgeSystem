namespace AlgoCode.Application.Features.Submissions.Queries.GetAll
{
    public class GetSubmissionsByProblemIdQuery : IRequest<ICollection<GetSubmissionsByProblemIdQueryResponse>>
    {
        public int ProblemId { get; set; }
    }

    public class GetSubmissionsByProblemIdQueryResponse
    {
        public int Id { get; set; }
        public string Language { get; set; }
        public string Status { get; set; }
        public DateTimeOffset SubmissionTime { get; set; }
        public double MemoryUsage { get; set; }
        public long ExecutionTime { get; set; }
    }

    public class GetSubmissionsByProblemIdQueryHandler : IRequestHandler<GetSubmissionsByProblemIdQuery, ICollection<GetSubmissionsByProblemIdQueryResponse>>
    {
        private readonly IApplicationDbContext _context;
        public GetSubmissionsByProblemIdQueryHandler(IApplicationDbContext context) => _context = context;

        public async Task<ICollection<GetSubmissionsByProblemIdQueryResponse>> Handle(GetSubmissionsByProblemIdQuery request, CancellationToken cancellationToken)
        {
            int activeSessionId = await _context.Sessions.Where(x => x.IsActive)
                                                         .Select(x => x.Id)
                                                         .FirstOrDefaultAsync();

            var submissions = await _context.Submissions
                                            .Where(x => x.ProblemId == request.ProblemId && x.SessionId == activeSessionId)
                                            .ToListAsync(cancellationToken);
            return submissions.Select(x => new GetSubmissionsByProblemIdQueryResponse
            {
                Id = x.Id,
                Language = x.Language,
                Status = x.Status.ToString(),
                SubmissionTime = x.Created,
                MemoryUsage = x.MemoryUsage,
                ExecutionTime = x.ExecutionTime
            }).ToList();
        }
    }
}
