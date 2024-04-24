

using Microsoft.AspNetCore.Http;

namespace AlgoCode.Application.Features.Comments.Commands.PostComment
{
    public class PostCommentCommandHandler : IRequestHandler<PostCommentCommandRequest, PostCommentCommandResponse>
    {
        private readonly IApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IHttpContextAccessor _httpContext;

        public PostCommentCommandHandler(IApplicationDbContext context, UserManager<ApplicationUser> userManager, IHttpContextAccessor httpContext)
        {
            _context = context;
            _userManager = userManager;
            _httpContext = httpContext;
        }

        public async Task<PostCommentCommandResponse> Handle(PostCommentCommandRequest request, CancellationToken cancellationToken)
        {
            //get the current user
            string userName = _httpContext.HttpContext.User.Identity.Name;
            var user = await _userManager.FindByNameAsync(userName);

            var comment = new Comment
            {
                Content = request.Content,
                UserId = user.Id,
                SolutionId = request.SolutionId,
            };

            if (request.ParentCommentId != null)
            {
                comment.ParentCommentId = request.ParentCommentId;
            }

            _context.Comments.Add(comment);
            await _context.SaveChangesAsync(cancellationToken);

            return new PostCommentCommandResponse
            {
                Id = comment.Id,
                Content = comment.Content,
                UserName = user.UserName,
                UserPhoto = user.PhotoName,
                SolutionId = comment.SolutionId,
                ParentCommentId = comment.ParentCommentId,
                Created = comment.Created
            };
        }
    }
}
