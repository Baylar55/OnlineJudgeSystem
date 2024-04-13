namespace AlgoCode.Application.Features.Problem.Commands.UpdateProblem
{
    public class UpdateProblemCommandHandler : IRequestHandler<UpdateProblemCommand, ValidationResultModel>
    {
        private readonly IApplicationDbContext _context;
        public UpdateProblemCommandHandler(IApplicationDbContext context) => _context = context;
        public async Task<ValidationResultModel> Handle(UpdateProblemCommand request, CancellationToken cancellationToken)
        {
            var validationResult = new ValidationResultModel();
            var problem = await _context.Problems.FindAsync(request.Id);
            if (problem == null)
            {
                validationResult.Errors.Add("Problem", ["Problem not found"]);
                return validationResult;
            }

            problem.Title = request.Title;
            problem.Description = request.Description;
            problem.Difficulty = request.Difficulty;
            problem.Point = request.Point;
            problem.MethodName = request.MethodName;
            problem.CodeTemplate = request.CodeTemplate;
            problem.Tags = await _context.Tags.Where(x => request.TagIds.Contains(x.Id)).ToListAsync(cancellationToken);
            _context.Problems.Update(problem);
            await _context.SaveChangesAsync(cancellationToken);
            return validationResult;
        }
    }
}
