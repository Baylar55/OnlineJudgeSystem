namespace AlgoCode.Application.Features.Tags.Queries
{
    public class GetTagsWithPaginationQueryHandler : IRequestHandler<GetTagsWithPaginationQuery, GetTagsWithPaginationQueryResponse>
    {
        private readonly IApplicationDbContext _context;

        public GetTagsWithPaginationQueryHandler(IApplicationDbContext context) => _context = context;

        public async Task<GetTagsWithPaginationQueryResponse> Handle(GetTagsWithPaginationQuery request, CancellationToken cancellationToken)
        {
            var tags = await _context.Tags.OrderByDescending(p => p.Id)
                                      .Skip((request.PageNumber - 1) * request.PageSize)
                                      .Take(request.PageSize)
                                      .ToListAsync();
            return new()
            {
                Tags = tags
            };
        }
    }
}
