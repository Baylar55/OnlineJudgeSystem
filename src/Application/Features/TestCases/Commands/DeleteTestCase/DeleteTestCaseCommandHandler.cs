namespace AlgoCode.Application.Features.TestCases.Commands.DeleteTestCase
{
    public class DeleteTestCaseCommandHandler : IRequestHandler<DeleteTestCaseCommand, ValidationResultModel>
    {
        private readonly IApplicationDbContext _context;

        public DeleteTestCaseCommandHandler(IApplicationDbContext context) => _context = context;

        public async Task<ValidationResultModel> Handle(DeleteTestCaseCommand request, CancellationToken cancellationToken)
        {
            var entity = await _context.TestCases.FindAsync(request.Id, cancellationToken);
            var validationResult = new ValidationResultModel();

            if (entity == null)
            {
                validationResult.IsValid = false;
                validationResult.Errors.Add("Id", new List<string> { "TestCase not found." });
                return validationResult;
            }

            _context.TestCases.Remove(entity);
            await _context.SaveChangesAsync(cancellationToken);
            return validationResult;
        }
    }
}
