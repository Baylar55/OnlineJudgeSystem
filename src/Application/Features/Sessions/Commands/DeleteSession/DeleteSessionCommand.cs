namespace AlgoCode.Application.Features.Sessions.Commands.DeleteSession
{
    public class DeleteSessionCommand : IRequest<ValidationResultModel>
    {
        public int Id { get; set; }
    }

    public class DeleteSessionCommandHandler : IRequestHandler<DeleteSessionCommand, ValidationResultModel>
    {
        private readonly IApplicationDbContext _context;

        public DeleteSessionCommandHandler(IApplicationDbContext context) => _context = context;

        public async Task<ValidationResultModel> Handle(DeleteSessionCommand request, CancellationToken cancellationToken)
        {
            var sessions = await _context.Sessions.ToListAsync(cancellationToken);
            var validationResult = new ValidationResultModel();
            if (sessions.Count == 1)
            {
                validationResult = new ValidationResultModel();
                validationResult.Errors.Add("Session", ["You can't delete the last session"]);
                return validationResult;
            }
            else
            {
                var session = await _context.Sessions.FindAsync(request.Id);
                if (session == null)
                {
                    validationResult.Errors.Add("Session", ["Session not found"]);
                    return validationResult;
                }

                _context.Sessions.Remove(session);
                await _context.SaveChangesAsync(cancellationToken);
            }
            return validationResult;
        }
    }
}
