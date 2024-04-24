namespace AlgoCode.Application.Features.Comments.Queries.GetAll
{
    public class GetAllRepliesByCommentIdQuery : IRequest<GetAllRepliesByCommentIdQueryResponse>
    {
        public int CommentId { get; set; }
    }

    public class GetAllRepliesByCommentIdQueryResponse
    {
        public List<Comment> Replies { get; set; }
    }

    public class GetAllRepliesByCommentIdQueryHandler : IRequestHandler<GetAllRepliesByCommentIdQuery, GetAllRepliesByCommentIdQueryResponse>
    {
        private readonly IApplicationDbContext _context;

        public GetAllRepliesByCommentIdQueryHandler(IApplicationDbContext context) => _context = context;

        public async Task<GetAllRepliesByCommentIdQueryResponse> Handle(GetAllRepliesByCommentIdQuery request, CancellationToken cancellationToken)
        {
            var replies = await _context.Comments
                                         .Include(c => c.User)
                                         .Include(c => c.Replies)
                                         .Where(c => c.ParentCommentId == request.CommentId)
                                         .ToListAsync(cancellationToken);

            return new GetAllRepliesByCommentIdQueryResponse { Replies = replies };
        }
    }
}
