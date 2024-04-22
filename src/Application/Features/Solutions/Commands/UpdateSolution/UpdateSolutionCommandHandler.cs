namespace AlgoCode.Application.Features.Solutions.Commands.UpdateSolution
{
    public class UpdateSolutionCommandHandler : IRequestHandler<UpdateSolutionCommand, ValidationResultModel>
    {
        private readonly IApplicationDbContext _context;

        public UpdateSolutionCommandHandler(IApplicationDbContext context) => _context = context;

        public async Task<ValidationResultModel> Handle(UpdateSolutionCommand request, CancellationToken cancellationToken)
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

            solution.Title = request.Title;
            solution.Description = request.Description;

            await _context.SaveChangesAsync(cancellationToken);
            return validationResult;
        }
    }
}
