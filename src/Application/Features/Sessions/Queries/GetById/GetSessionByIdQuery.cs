namespace AlgoCode.Application.Features.Sessions.Queries.GetById
{
    public class GetSessionByIdQuery : IRequest<Session>
    {
        public int Id { get; set; }
    }

    public class GetSessionByIdQueryHandler : IRequestHandler<GetSessionByIdQuery, Session>
    {
        private readonly IApplicationDbContext _context;
        public GetSessionByIdQueryHandler(IApplicationDbContext context) => _context = context;

        public async Task<Session> Handle(GetSessionByIdQuery request, CancellationToken cancellationToken)
        {
            return await _context.Sessions.FindAsync(request.Id, cancellationToken);
        }
    }
}
