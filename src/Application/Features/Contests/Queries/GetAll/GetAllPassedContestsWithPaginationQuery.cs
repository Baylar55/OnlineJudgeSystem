namespace AlgoCode.Application.Features.Contests.Queries.GetAll
{
    public class GetAllPassedContestsWithPaginationQuery : IRequest<GetAllPassedContestsWithPaginationQueryResponse>

    {
        public int PageNumber { get; set; } = 1;
        public int PageSize { get; set; } = 10;
    }

    public class GetAllPassedContestsWithPaginationQueryResponse
    {
        public ICollection<Contest>? Contests { get; set; }
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
        public int PageCount { get; set; }
    }

    public class GetAllPassedContestsQueryHandler : IRequestHandler<GetAllPassedContestsWithPaginationQuery, GetAllPassedContestsWithPaginationQueryResponse>
    {
        private readonly IApplicationDbContext _context;
        public GetAllPassedContestsQueryHandler(IApplicationDbContext context) => _context = context;

        public async Task<GetAllPassedContestsWithPaginationQueryResponse> Handle(GetAllPassedContestsWithPaginationQuery request, CancellationToken cancellationToken)
        {
            var contests = await _context.Contests
                                         .Where(p => p.EndTime< DateTime.Now)
                                         .OrderByDescending(p => p.Id)
                                         .Skip((request.PageNumber - 1) * request.PageSize)
                                         .Take(request.PageSize)
                                         .ToListAsync();

            var pageCount = (int)Math.Ceiling((await _context.Contests.CountAsync()) / (decimal)request.PageSize);

            return new()
            {
                Contests = contests,
                PageNumber = request.PageNumber,
                PageSize = request.PageSize,
                PageCount = pageCount
            };
        }
    }
}
