namespace AlgoCode.Application.Features.TestCases.Queries.GetAll;

public class GetTestCasesWithPaginationQueryHandler(IApplicationDbContext context) : IRequestHandler<GetTestCasesWithPaginationQuery, GetTestCasesWithPaginationQueryResponse>
{
    public async Task<GetTestCasesWithPaginationQueryResponse> Handle(GetTestCasesWithPaginationQuery request, CancellationToken cancellationToken)
    {
        var testCases = await context.TestCases.OrderByDescending(p => p.Id)
                                  .Skip((request.PageNumber - 1) * request.PageSize)
                                  .Take(request.PageSize)
                                  .ToListAsync(cancellationToken);

        var pageCount = (int)Math.Ceiling((await context.TestCases.CountAsync(cancellationToken)) / (decimal)request.PageSize);

        return new GetTestCasesWithPaginationQueryResponse(testCases, request.PageNumber, request.PageSize, pageCount);
    }
}
