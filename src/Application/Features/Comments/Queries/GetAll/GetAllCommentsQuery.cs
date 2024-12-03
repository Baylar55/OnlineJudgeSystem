namespace AlgoCode.Application.Features.Comments.Queries.GetAll;

public record GetAllCommentsQuery(int SolutionId) : IRequest<GetAllCommentsQueryResponse>;

public record GetAllCommentsQueryResponse(List<Comment> Comments);

public class GetAllCommentsQueryHandler(IApplicationDbContext context) : IRequestHandler<GetAllCommentsQuery, GetAllCommentsQueryResponse>
{
    public async Task<GetAllCommentsQueryResponse> Handle(GetAllCommentsQuery request, CancellationToken cancellationToken)
    {
        var comments = await context.Comments
                                     .Include(c => c.User)
                                     .Include(c => c.Replies)
                                     .Where(c => c.SolutionId == request.SolutionId && c.ParentCommentId == null)
                                     .ToListAsync(cancellationToken);

        return new GetAllCommentsQueryResponse(comments);
    }
}
