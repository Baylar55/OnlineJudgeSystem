namespace AlgoCode.Application.Features.Sessions.Commands.ActivateSession
{
    public class ActivateSessionCommand : IRequest<ValidationResultModel>
    {
        public int SessionId { get; set; }
    }

    public class ActivateSessionCommandHandler : IRequestHandler<ActivateSessionCommand, ValidationResultModel>
    {
        private readonly IApplicationDbContext _context;
        public ActivateSessionCommandHandler(IApplicationDbContext context) => _context = context;

        public async Task<ValidationResultModel> Handle(ActivateSessionCommand request, CancellationToken cancellationToken)
        {
            var validationResult = new ValidationResultModel();
            var sessionToActivate = await _context.Sessions.FindAsync(request.SessionId);

            if (sessionToActivate == null)
            {
                validationResult.Errors.Add("Session", ["Session not found"]);
                return validationResult;
            }

            var activeSession = await _context.Sessions.FirstOrDefaultAsync(x => x.IsActive, cancellationToken);
            if (activeSession != null)
                activeSession.IsActive = false;

            sessionToActivate.IsActive = true;
            await _context.SaveChangesAsync(cancellationToken);

            return validationResult;
        }
    }
}
