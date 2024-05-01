namespace AlgoCode.Application.Features.Submissions.Queries.GetAll
{
    public class GetSubmissionsWithPaginationQuery : IRequest<GetSubmissionsWithPaginationQueryResponse>
    {
        public int PageNumber { get; set; } = 1;
        public int PageSize { get; set; } = 10;
    }

    public class GetSubmissionsWithPaginationQueryResponse
    {
        public ICollection<Submission>? Submissions { get; set; }
        public List<string> ProblemTitles { get; set; }
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
        public int PageCount { get; set; }
    }

    public class GetSubmissionsWithPaginationQueryHandler : IRequestHandler<GetSubmissionsWithPaginationQuery, GetSubmissionsWithPaginationQueryResponse>
    {
        private readonly IApplicationDbContext _context;
        public GetSubmissionsWithPaginationQueryHandler(IApplicationDbContext context) => _context = context;

        public async Task<GetSubmissionsWithPaginationQueryResponse> Handle(GetSubmissionsWithPaginationQuery request, CancellationToken cancellationToken)
        {
            var activeSessionId = _context.Sessions.Where(s => s.IsActive).Select(s => s.Id).FirstOrDefault();

            var submissions = await _context.Submissions.Where(s => s.SessionId == activeSessionId)
                                                        .OrderByDescending(s => s.Id)
                                                        .Skip((request.PageNumber - 1) * request.PageSize)
                                                        .Take(request.PageSize)
                                                        .Select(s => new
                                                        {
                                                            Submission = s,
                                                            ProblemTitle = s.Problem.Title
                                                        })
                                                        .ToListAsync();

            var pageCount = (int)Math.Ceiling((await _context.Submissions.CountAsync()) / (decimal)request.PageSize);

            return new()
            {
                Submissions = submissions.Select(x => x.Submission).ToList(),
                ProblemTitles = submissions.Select(x => x.ProblemTitle).ToList(),
                PageNumber = request.PageNumber,
                PageSize = request.PageSize,
                PageCount = pageCount
            };
        }
    }
}
