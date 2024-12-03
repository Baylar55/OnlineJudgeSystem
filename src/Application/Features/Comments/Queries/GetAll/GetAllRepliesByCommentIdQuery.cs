namespace AlgoCode.Application.Features.Comments.Queries.GetAll;

public record GetAllRepliesByCommentIdQuery(int CommentId) : IRequest<GetAllRepliesByCommentIdQueryResponse>;

public record GetAllRepliesByCommentIdQueryResponse(List<Comment> Replies);

public class GetAllRepliesByCommentIdQueryHandler(IApplicationDbContext context) : IRequestHandler<GetAllRepliesByCommentIdQuery, GetAllRepliesByCommentIdQueryResponse>
{
    public async Task<GetAllRepliesByCommentIdQueryResponse> Handle(GetAllRepliesByCommentIdQuery request, CancellationToken cancellationToken)
    {
        var replies = await context.Comments
                                     .Include(c => c.User)
                                     .Include(c => c.Replies)
                                     .Where(c => c.ParentCommentId == request.CommentId)
                                     .ToListAsync(cancellationToken);

        return new GetAllRepliesByCommentIdQueryResponse (replies);
    }
}
