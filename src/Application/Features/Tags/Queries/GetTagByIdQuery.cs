using AlgoCode.Application.Features.Tags.Commands.UpdateTag;

namespace AlgoCode.Application.Features.Tags.Queries
{
    public class GetTagByIdQuery : IRequest<UpdateTagCommand>
    {
        public int Id { get; set; }
    }

    public class GetTagByIdQueryHandler : IRequestHandler<GetTagByIdQuery, UpdateTagCommand>
    {
        private readonly IApplicationDbContext _context;
        public GetTagByIdQueryHandler(IApplicationDbContext context) => _context = context;
        public async Task<UpdateTagCommand> Handle(GetTagByIdQuery request, CancellationToken cancellationToken)
        {
            var entity = await _context.Tags.FindAsync(request.Id, cancellationToken);
            return new UpdateTagCommand
            {
                Id = entity.Id,
                Title = entity.Title
            };
        }
    }
}
