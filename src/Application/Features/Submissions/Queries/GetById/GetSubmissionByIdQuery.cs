namespace AlgoCode.Application.Features.Submissions.Queries.GetById
{
    public class GetSubmissionByIdQuery : IRequest<GetSubmissionByIdQueryResponse>
    {
        public int Id { get; set; }
    }

    public class GetSubmissionByIdQueryResponse
    {
        public int Id { get; set; }
        public string ProblemName { get; set; }
        public string SourceCode { get; set; }
        public string Language { get; set; }
        public string Status { get; set; }
        public DateTimeOffset SubmissionTime { get; set; }
        public string MemoryUsage { get; set; }
        public long ExecutionTime { get; set; }
    }

    public class GetSubmissionByIdQueryHandler : IRequestHandler<GetSubmissionByIdQuery, GetSubmissionByIdQueryResponse>
    {
        private readonly IApplicationDbContext _context;
        public GetSubmissionByIdQueryHandler(IApplicationDbContext context) => _context = context;

        public async Task<GetSubmissionByIdQueryResponse> Handle(GetSubmissionByIdQuery request, CancellationToken cancellationToken)
        {
            var entity = await _context.Submissions.FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);
            return new GetSubmissionByIdQueryResponse
            {
                ProblemName = _context.Problems.FirstOrDefault(x => x.Id == entity.ProblemId).Title,
                Id = entity.Id,
                SourceCode = entity.SourceCode,
                Language = entity.Language,
                Status = entity.Status.ToString(),
                SubmissionTime = entity.Created,
                ExecutionTime = entity.ExecutionTime
            };
        }
    }
}
