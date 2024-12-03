namespace AlgoCode.Application.Features.Contests.Queries.GetAll;

public record GetAllContestsWithPaginationQuery(int PageNumber = 1, int PageSize = 10) : IRequest<GetAllContestsWithPaginationQueryResponse>;

public record GetAllContestsWithPaginationQueryResponse(ICollection<Contest>? Contests, int PageNumber, int PageSize, int PageCount);

public class GetAllContestsQueryHandler(IApplicationDbContext context) : IRequestHandler<GetAllContestsWithPaginationQuery, GetAllContestsWithPaginationQueryResponse>
{
    public async Task<GetAllContestsWithPaginationQueryResponse> Handle(GetAllContestsWithPaginationQuery request, CancellationToken cancellationToken)
    {
        var contests = await context.Contests.OrderByDescending(p => p.Id)
                                    .Skip((request.PageNumber - 1) * request.PageSize)
                                    .Take(request.PageSize)
                                    .ToListAsync(cancellationToken);

        var pageCount = (int)Math.Ceiling((await context.Contests.CountAsync(cancellationToken)) / (decimal)request.PageSize);

        return new GetAllContestsWithPaginationQueryResponse(contests, request.PageNumber, request.PageSize, pageCount);
    }
}
