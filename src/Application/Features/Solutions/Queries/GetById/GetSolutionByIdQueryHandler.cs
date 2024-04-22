namespace AlgoCode.Application.Features.Solutions.Queries.GetById
{
    public class GetSolutionByIdQueryHandler : IRequestHandler<GetSolutionByIdQuery, GetSolutionByIdQueryResponse>
    {
        private readonly IApplicationDbContext _context;

        public GetSolutionByIdQueryHandler(IApplicationDbContext context) => _context = context;

        public async Task<GetSolutionByIdQueryResponse> Handle(GetSolutionByIdQuery request, CancellationToken cancellationToken)
        {
            var solution = await _context.Solutions
                                         .Include(x => x.Submission)
                                         .ThenInclude(x => x.User)
                                         .FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);

            return new GetSolutionByIdQueryResponse
            {
                Id = solution.Id,
                Title = solution.Title,
                Description = solution.Description,
                UserName = solution.Submission.User.UserName,
                Created = solution.Created                
            };
        }
    }
}
