using Ardalis.GuardClauses;

namespace AlgoCode.Application.Features.Tags.Commands.DeleteTag
{
    public class DeleteTagCommand : IRequest
    {
        public int Id { get; set; }
    }

    public class DeleteTagCommandHandler : IRequestHandler<DeleteTagCommand>
    {
        private readonly IApplicationDbContext _context;

        public DeleteTagCommandHandler(IApplicationDbContext context) => _context = context;

        public async Task Handle(DeleteTagCommand request, CancellationToken cancellationToken)
        {
            var entity = await _context.Tags.FindAsync(request.Id, cancellationToken);
            Guard.Against.NotFound(request.Id, entity);

            _context.Tags.Remove(entity);

            await _context.SaveChangesAsync(cancellationToken);
        }
    }
}
