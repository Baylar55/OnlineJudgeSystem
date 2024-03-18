namespace AlgoCode.Application.Features.Problem.Queries.GetAll
{
    public class GetProblemsWithPaginationQueryHandler : IRequestHandler<GetProblemsWithPaginationQuery, GetProblemsWithPaginationQueryResponse>
    {
        private readonly IApplicationDbContext _context;
        public GetProblemsWithPaginationQueryHandler(IApplicationDbContext context) => _context = context;
        public async Task<GetProblemsWithPaginationQueryResponse> Handle(GetProblemsWithPaginationQuery request, CancellationToken cancellationToken)
        {
            var problems = await _context.Problems.OrderByDescending(p => p.Id)
                                      .Skip((request.PageNumber - 1) * request.PageSize)
                                      .Take(request.PageSize)
                                      .ToListAsync();

            return new()
            {
                Problems = problems,
                PageNumber = request.PageNumber,
                PageSize = request.PageSize,
                PageCount = (int)Math.Ceiling((await _context.Problems.CountAsync()) / (decimal)request.PageSize)
            };
        }
    }
}
