namespace AlgoCode.Application.Features.Problem.Queries.GetById
{
    public class GetProblemByIdQuery : IRequest<GetProblemByIdQueryResponse>
    {
        public int Id { get; set; }
    }

    public class GetProblemByIdQueryHandler : IRequestHandler<GetProblemByIdQuery, GetProblemByIdQueryResponse>
    {

        private readonly IApplicationDbContext _context;
        public GetProblemByIdQueryHandler(IApplicationDbContext context) => _context = context;

        public async Task<GetProblemByIdQueryResponse> Handle(GetProblemByIdQuery request, CancellationToken cancellationToken)
        {
            var entity = await _context.Problems.Include(x => x.Tags).FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);
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
                CreatedBy = entity.CreatedBy,
                LastModified = entity.LastModified,
                LastModifiedBy = entity.LastModifiedBy,
                Tags = entity.Tags.Select(x => x.Title).ToList()
            };
        }
    }
}
