namespace AlgoCode.Application.Features.Contests.Commands.DeleteContest
{
    public class DeleteContestCommandHandler : IRequestHandler<DeleteContestCommand, ValidationResultModel>
    {
        private readonly IApplicationDbContext _context;
        public DeleteContestCommandHandler(IApplicationDbContext context) => _context = context;
        public async Task<ValidationResultModel> Handle(DeleteContestCommand request, CancellationToken cancellationToken)
        {
            var validationResult = new ValidationResultModel();

            var contest = await _context.Contests.FindAsync(request.Id);
            if (contest == null)
            {
                validationResult.Errors.Add("Id", ["Contest not found"]);
                return validationResult;
            }

            _context.Contests.Remove(contest);
            await _context.SaveChangesAsync(cancellationToken);
            return validationResult;
        }
    }
}
