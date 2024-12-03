using AlgoCode.Domain.Entities.Identity;

namespace AlgoCode.Application.Features.Comments.Commands.PostComment;

public class PostCommentCommandHandler(IApplicationDbContext context, UserManager<ApplicationUser> userManager, IHttpContextAccessor httpContext) : IRequestHandler<PostCommentCommandRequest, PostCommentCommandResponse>
{
    public async Task<PostCommentCommandResponse> Handle(PostCommentCommandRequest request, CancellationToken cancellationToken)
    {
        if (httpContext.HttpContext?.User?.Identity?.IsAuthenticated != true) throw new UnauthorizedAccessException("You must be logged in to post a comment.");

        string? userName = httpContext.HttpContext.User.Identity.Name;

        var user = await userManager.FindByNameAsync(userName!);

        var comment = request.Adapt<Comment>();

        comment.UserId = user!.Id;

        if (request.ParentCommentId != null)
        {
            comment.ParentCommentId = request.ParentCommentId;
        }

        context.Comments.Add(comment);
        await context.SaveChangesAsync(cancellationToken);

        return comment.Adapt<PostCommentCommandResponse>() with
        {
            UserName = user.UserName!,
            UserPhoto = user.PhotoName
        };
    }
}
