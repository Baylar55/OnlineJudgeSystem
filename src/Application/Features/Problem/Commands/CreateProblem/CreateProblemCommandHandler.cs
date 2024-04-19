namespace AlgoCode.Application.Features.Problem.Commands.CreateProblem
{
    public class CreateProblemCommandHandler : IRequestHandler<CreateProblemCommand, ValidationResultModel>
    {
        private readonly IApplicationDbContext _context;
        public CreateProblemCommandHandler(IApplicationDbContext context) => _context = context;
        public async Task<ValidationResultModel> Handle(CreateProblemCommand request, CancellationToken cancellationToken)
        {
            var validationResult = new ValidationResultModel();
            bool isExist = await _context.Problems.AnyAsync(x => x.Title.Trim().ToLower() == request.Title.Trim().ToLower(), cancellationToken);

            if (isExist)
            {
                validationResult.Errors.Add("Title", ["Problem with this title already exists"]);
                return validationResult;
            }

            var problem = new Domain.Entities.Problem
            {
                Title = request.Title,
                Description = request.Description,
                Difficulty = request.Difficulty,
                Point = request.Point,
                MethodName = request.MethodName,
                CodeTemplate = request.CodeTemplate,
                AccessLevel = request.AccessLevel
            };

            if (request.TagIds != null && request.TagIds.Any())
            {
                problem.Tags = await _context.Tags.Where(t => request.TagIds.Contains(t.Id)).ToListAsync(cancellationToken);
            }

            _context.Problems.Add(problem);
            await _context.SaveChangesAsync(cancellationToken);
            return validationResult;
        }
    }
}
