namespace AlgoCode.Application.Features.Tags.Queries.GetAll
{
    public class GetAllTagsQuery : IRequest<GetAllTagsQueryResponse> { }


    public class GetAllTagsQueryResponse
    {
        public List<Tag> Tags { get; set; }
    }

    public class GetAllTagsQueryHandler : IRequestHandler<GetAllTagsQuery, GetAllTagsQueryResponse>
    {
        private readonly IApplicationDbContext _context;

        public GetAllTagsQueryHandler(IApplicationDbContext context) => _context = context;

        public async Task<GetAllTagsQueryResponse> Handle(GetAllTagsQuery request, CancellationToken cancellationToken)
        {
            var tags = await _context.Tags.ToListAsync(cancellationToken);
            return new GetAllTagsQueryResponse { Tags = tags };
        }
    }
}
