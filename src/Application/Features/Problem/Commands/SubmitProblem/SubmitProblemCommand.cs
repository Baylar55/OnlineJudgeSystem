namespace AlgoCode.Application.Features.Problem.Commands.SubmitProblem
{
    public class SubmitProblemCommand : IRequest<string>
    {
        public string SourceCode { get; set; }
        public string Language { get; set; }
        public int ProblemId { get; set; }
        public double MemoryUsage { get; set; }
        public long ExecutionTime { get; set; }
        public SubmissionStatus Status { get; set; } = SubmissionStatus.Accepted;
        public string UserId { get; set; }
    }

    public class SubmitProblemCommandHandler : IRequestHandler<SubmitProblemCommand, string>
    {
        private readonly IApplicationDbContext _context;
        public SubmitProblemCommandHandler(IApplicationDbContext context) => _context = context;

        public async Task<string> Handle(SubmitProblemCommand request, CancellationToken cancellationToken)
        {
            var entity = new Submission
            {
                SourceCode = request.SourceCode,
                Language = request.Language,
                ProblemId = request.ProblemId,
                MemoryUsage = request.MemoryUsage,
                ExecutionTime = request.ExecutionTime,
                Status = request.Status,
                UserId = request.UserId
            };

            await _context.Submissions.AddAsync(entity, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);

            return entity.Id.ToString();
        }
    }
}
