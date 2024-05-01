namespace AlgoCode.Application.Features.Contests.Queries.GetById
{
    public class GetContestByIdQuery : IRequest<GetContestByIdQueryResponse>
    {
        public int Id { get; set; }
    }

    public class GetContestByIdQueryResponse
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public bool IsActive { get; set; }
        public string Photo { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public DateTimeOffset Created { get; set; }
        public string? CreatedBy { get; set; }
        public DateTimeOffset? LastModified { get; set; }
        public string? LastModifiedBy { get; set; }
        public List<Domain.Entities.Problem> Problems { get; set; }
    }

    public class GetContestByIdQueryHandler : IRequestHandler<GetContestByIdQuery, GetContestByIdQueryResponse>
    {
        private readonly IApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;

        public GetContestByIdQueryHandler(IApplicationDbContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        public async Task<GetContestByIdQueryResponse> Handle(GetContestByIdQuery request, CancellationToken cancellationToken)
        {
            var contest = await _context.Contests
                                        .Include(c => c.Problems)
                                        .FirstOrDefaultAsync(c => c.Id == request.Id);

            var createdBy = await _userManager.FindByIdAsync(contest.CreatedBy);
            var lastModifiedBy = await _userManager.FindByIdAsync(contest.LastModifiedBy);

            if (contest == null)
                return null;

            return new GetContestByIdQueryResponse
            {
                Id = contest.Id,
                Title = contest.Title,
                Description = contest.Description,
                Photo = contest.CoverPhoto,
                IsActive = contest.IsActive,
                StartTime = contest.StartTime,
                EndTime = contest.EndTime,
                Created = contest.Created,
                CreatedBy = createdBy.UserName,
                LastModified = contest.LastModified,
                LastModifiedBy = lastModifiedBy.UserName,
                Problems = contest.Problems.ToList()
            };
        }
    }
}
