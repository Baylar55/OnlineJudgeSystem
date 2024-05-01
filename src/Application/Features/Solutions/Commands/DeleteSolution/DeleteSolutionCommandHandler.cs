using AlgoCode.Domain.Entities;

namespace AlgoCode.Application.Features.Solutions.Commands.DeleteSolution
{
    public class DeleteSolutionCommandHandler : IRequestHandler<DeleteSolutionCommand, ValidationResultModel>
    {
        private readonly IApplicationDbContext _context;

        public DeleteSolutionCommandHandler(IApplicationDbContext context) => _context = context;

        public async Task<ValidationResultModel> Handle(DeleteSolutionCommand request, CancellationToken cancellationToken)
        {
            var validationResult = new ValidationResultModel();
            var solution = await _context.Solutions
                .Include(s => s.Submission)
                .FirstOrDefaultAsync(s => s.Id == request.Id, cancellationToken);

            if (solution == null)
            {
                validationResult.Errors.Add("", ["Solution not found"]);
                return validationResult;
            }

            _context.Comments.RemoveRange(_context.Comments.Where(c => c.SolutionId == solution.Id));
            await _context.SaveChangesAsync(cancellationToken);


            _context.Solutions.Remove(solution);
            await _context.SaveChangesAsync(cancellationToken);
            return validationResult;
        }
    }
}
