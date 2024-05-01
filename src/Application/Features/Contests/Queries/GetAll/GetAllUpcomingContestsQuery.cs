namespace AlgoCode.Application.Features.Contests.Queries.GetAll
{
    public class GetAllUpcomingContestsQuery : IRequest<List<GetAllUpcomingContestsQueryResponse>> { }

    public class GetAllUpcomingContestsQueryResponse
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string ImageUrl { get; set; }
        public DateTime StartTime { get; set; }
    }

    public class GetAllUpcomingContestsQueryHandler : IRequestHandler<GetAllUpcomingContestsQuery, List<GetAllUpcomingContestsQueryResponse>>
    {
        private readonly IApplicationDbContext _context;
        public GetAllUpcomingContestsQueryHandler(IApplicationDbContext context) => _context = context;

        public async Task<List<GetAllUpcomingContestsQueryResponse>> Handle(GetAllUpcomingContestsQuery request, CancellationToken cancellationToken)
        {
            var contests = await _context.Contests
                                         .Where(c => c.StartTime > DateTime.Now)
                                         .OrderBy(c => c.StartTime)
                                         .Take(3)
                                         .ToListAsync();

            return contests.Select(contest => new GetAllUpcomingContestsQueryResponse
            {
                Id = contest.Id,
                Title = contest.Title,
                ImageUrl = contest.CoverPhoto,
                StartTime = contest.StartTime
            }).ToList();
        }
    }
}
