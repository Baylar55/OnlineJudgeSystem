namespace AlgoCode.Application.Features.Sessions.Commands.UpdateSession
{
    public class UpdateSessionCommand : IRequest<ValidationResultModel>
    {
        public int Id { get; set; }
        public string Title { get; set; } = null!;
    }

    public class UpdateSessionCommandHandler : IRequestHandler<UpdateSessionCommand, ValidationResultModel>
    {
        private readonly IApplicationDbContext _context;

        public UpdateSessionCommandHandler(IApplicationDbContext context) => _context = context;

        public async Task<ValidationResultModel> Handle(UpdateSessionCommand request, CancellationToken cancellationToken)
        {
            var session = await _context.Sessions.FindAsync(request.Id);
            var validationResult = new ValidationResultModel();
            if (session == null)
            {
                validationResult.Errors.Add("Session", ["Session not found"]);
                return validationResult;
            }

            var isExist = await _context.Sessions.AnyAsync(x => x.Title.Trim().ToLower() == request.Title.Trim().ToLower(), cancellationToken);
            if (isExist)
            {
                validationResult.Errors.Add("Title", ["Session with this title already exists"]);
                return validationResult;
            }

            session.Title = request.Title;
            _context.Sessions.Update(session);
            await _context.SaveChangesAsync(cancellationToken);
            return validationResult;
        }
    }
}
