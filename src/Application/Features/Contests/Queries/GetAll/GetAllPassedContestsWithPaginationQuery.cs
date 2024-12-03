namespace AlgoCode.Application.Features.Contests.Queries.GetAll;

public record GetAllPassedContestsWithPaginationQuery(int PageNumber = 1, int PageSize = 10) : IRequest<GetAllPassedContestsWithPaginationQueryResponse>;

public record GetAllPassedContestsWithPaginationQueryResponse(ICollection<Contest>? Contests, int PageNumber, int PageSize, int PageCount);

public class GetAllPassedContestsQueryHandler(IApplicationDbContext context) : IRequestHandler<GetAllPassedContestsWithPaginationQuery, GetAllPassedContestsWithPaginationQueryResponse>
{

    public async Task<GetAllPassedContestsWithPaginationQueryResponse> Handle(GetAllPassedContestsWithPaginationQuery request, CancellationToken cancellationToken)
    {
        var contests = await context.Contests
                                     .Where(p => p.EndTime < DateTime.Now)
                                     .OrderByDescending(p => p.Id)
                                     .Skip((request.PageNumber - 1) * request.PageSize)
                                     .Take(request.PageSize)
                                     .ToListAsync(cancellationToken);

        var pageCount = (int)Math.Ceiling((await context.Contests.CountAsync(cancellationToken)) / (decimal)request.PageSize);

        return new GetAllPassedContestsWithPaginationQueryResponse(contests, request.PageNumber, request.PageSize, pageCount);
    }
}
