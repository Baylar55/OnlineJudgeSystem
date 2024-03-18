namespace AlgoCode.Application.Features.Tags.Queries.GetById
{
    public class GetTagByIdQueryHandler : IRequestHandler<GetTagByIdQuery, GetTagByIdQueryResponse>
    {
        private readonly IApplicationDbContext _context;
        public GetTagByIdQueryHandler(IApplicationDbContext context) => _context = context;
        public async Task<GetTagByIdQueryResponse> Handle(GetTagByIdQuery request, CancellationToken cancellationToken)
        {
            var entity = await _context.Tags.FindAsync(request.Id, cancellationToken);
            return new GetTagByIdQueryResponse
            {
                Id = entity.Id,
                Title = entity.Title
            };
        }
    }
}
