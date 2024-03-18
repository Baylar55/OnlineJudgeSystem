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
            var entity = await _context.Problems.FindAsync(request.Id, cancellationToken);
            return new GetProblemByIdQueryResponse
            {
                Title = entity.Title,
                Description = entity.Description,
                MethodName = entity.MethodName,
                Point = entity.Point,
                Difficulty = entity.Difficulty.ToString(),
                Status = entity.Status.ToString(),
                Created = entity.Created,
                CreatedBy = entity.CreatedBy,
                LastModified = entity.LastModified,
                LastModifiedBy = entity.LastModifiedBy
            };
        }
    }
}
