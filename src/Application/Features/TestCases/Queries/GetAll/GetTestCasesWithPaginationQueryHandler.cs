namespace AlgoCode.Application.Features.TestCases.Queries.GetAll
{
    public class GetTestCasesWithPaginationQueryHandler : IRequestHandler<GetTestCasesWithPaginationQuery, GetTestCasesWithPaginationQueryResponse>
    {
        private readonly IApplicationDbContext _context;

        public GetTestCasesWithPaginationQueryHandler(IApplicationDbContext context) => _context = context;

        public async Task<GetTestCasesWithPaginationQueryResponse> Handle(GetTestCasesWithPaginationQuery request, CancellationToken cancellationToken)
        {
            var testCases = await _context.TestCases.OrderByDescending(p => p.Id)
                                      .Skip((request.PageNumber - 1) * request.PageSize)
                                      .Take(request.PageSize)
                                      .ToListAsync();
            return new()
            {
                TestCases = testCases,
                PageNumber = request.PageNumber,
                PageSize = request.PageSize,
                PageCount = (int)Math.Ceiling((await _context.TestCases.CountAsync()) / (decimal)request.PageSize)
            };
        }
    }
}
