namespace AlgoCode.Application.Features.Comments.Queries.GetAll
{
    public class GetAllCommentsQuery : IRequest<GetAllCommentsQueryResponse>
    {
        public int SolutionId { get; set; }
    }

    public class GetAllCommentsQueryResponse
    {
        public List<Comment> Comments { get; set; }
    }

    public class GetAllCommentsQueryHandler : IRequestHandler<GetAllCommentsQuery, GetAllCommentsQueryResponse>
    {
        private readonly IApplicationDbContext _context;

        public GetAllCommentsQueryHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<GetAllCommentsQueryResponse> Handle(GetAllCommentsQuery request, CancellationToken cancellationToken)
        {
            var comments = await _context.Comments
                                         .Include(c => c.User)
                                         .Include(c => c.Replies)
                                         .Where(c => c.SolutionId == request.SolutionId && c.ParentCommentId == null)
                                         .ToListAsync(cancellationToken);

            return new GetAllCommentsQueryResponse { Comments = comments };
        }
    }
}
