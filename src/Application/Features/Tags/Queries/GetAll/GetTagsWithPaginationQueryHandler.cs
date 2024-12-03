namespace AlgoCode.Application.Features.Tags.Queries.GetAll;

public class GetTagsWithPaginationQueryHandler(IApplicationDbContext context) : IRequestHandler<GetTagsWithPaginationQuery, GetTagsWithPaginationQueryResponse>
{
    public async Task<GetTagsWithPaginationQueryResponse> Handle(GetTagsWithPaginationQuery request, CancellationToken cancellationToken)
    {
        var tags = await context.Tags.OrderByDescending(p => p.Id)
                                  .Skip((request.PageNumber - 1) * request.PageSize)
                                  .Take(request.PageSize)
                                  .ToListAsync(cancellationToken);

        var pageCount = (int)Math.Ceiling((await context.Tags.CountAsync(cancellationToken)) / (decimal)request.PageSize);

        return new GetTagsWithPaginationQueryResponse(tags, request.PageNumber, request.PageSize, pageCount);
    }
}
