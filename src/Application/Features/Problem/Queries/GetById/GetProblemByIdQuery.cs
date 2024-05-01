namespace AlgoCode.Application.Features.Problem.Queries.GetById
{
    public class GetProblemByIdQuery : IRequest<GetProblemByIdQueryResponse>
    {
        public int Id { get; set; }
    }

    public class GetProblemByIdQueryHandler : IRequestHandler<GetProblemByIdQuery, GetProblemByIdQueryResponse>
    {

        private readonly IApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;

        public GetProblemByIdQueryHandler(IApplicationDbContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        public async Task<GetProblemByIdQueryResponse> Handle(GetProblemByIdQuery request, CancellationToken cancellationToken)
        {
            var entity = await _context.Problems.Include(x => x.Tags).FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);

            if (entity == null)
                return null;

            var createdBy = await _userManager.FindByIdAsync(entity.CreatedBy);
            var lastModifiedBy = await _userManager.FindByIdAsync(entity.LastModifiedBy);

            return new GetProblemByIdQueryResponse
            {
                Title = entity.Title,
                Description = entity.Description,
                MethodName = entity.MethodName,
                CodeTemplate = entity.CodeTemplate,
                Point = entity.Point,
                Difficulty = entity.Difficulty.ToString(),
                AccessLevel = entity.AccessLevel.ToString(),
                Created = entity.Created,
                CreatedBy = createdBy.UserName,
                LastModified = entity.LastModified,
                LastModifiedBy = lastModifiedBy.UserName,
                Tags = entity.Tags.Select(x => new Tag
                {
                    Id = x.Id,
                    Title = x.Title
                }).ToList()
            };
        }
    }
}
