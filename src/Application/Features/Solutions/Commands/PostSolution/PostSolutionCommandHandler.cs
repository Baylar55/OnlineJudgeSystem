namespace AlgoCode.Application.Features.Solutions.Commands.PostSolution
{
    public class PostSolutionCommandHandler : IRequestHandler<PostSolutionCommand, ValidationResultModel>
    {
        private readonly IApplicationDbContext _context;

        public PostSolutionCommandHandler(IApplicationDbContext context) => _context = context;

        public async Task<ValidationResultModel> Handle(PostSolutionCommand request, CancellationToken cancellationToken)
        {
            var validationResult = new ValidationResultModel();
            var submission = await _context.Submissions
                .Include(s => s.Problem)
                .Include(s => s.User)
                .FirstOrDefaultAsync(s => s.Id == request.SubmissionId, cancellationToken);

            if (submission == null)
            {
                validationResult.Errors.Add("", ["Submission not found"]);
                return validationResult;
            }

            var solution = new Solution
            {
                Title = request.Title,
                Description = request.Description,
                SubmissionId = submission.Id
            };

            await _context.Solutions.AddAsync(solution);
            await _context.SaveChangesAsync(cancellationToken);
            return validationResult;
        }
    }
}
