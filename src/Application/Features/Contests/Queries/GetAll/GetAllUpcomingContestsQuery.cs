namespace AlgoCode.Application.Features.Contests.Queries.GetAll;

public record GetAllUpcomingContestsQuery() : IRequest<List<GetAllUpcomingContestsQueryResponse>>;

public record GetAllUpcomingContestsQueryResponse(int Id, string Title, string ImageUrl, DateTime StartTime);

public class GetAllUpcomingContestsQueryHandler(IApplicationDbContext context) : IRequestHandler<GetAllUpcomingContestsQuery, List<GetAllUpcomingContestsQueryResponse>>
{
    public async Task<List<GetAllUpcomingContestsQueryResponse>> Handle(GetAllUpcomingContestsQuery request, CancellationToken cancellationToken)
    {
        var contests = await context.Contests
                                     .Where(c => c.StartTime > DateTime.Now)
                                     .OrderBy(c => c.StartTime)
                                     .Take(3)
                                     .ToListAsync(cancellationToken);
       
         return contests.Adapt<List<GetAllUpcomingContestsQueryResponse>>();
    }
}
