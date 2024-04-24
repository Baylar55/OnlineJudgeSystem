namespace AlgoCode.Application.Features.Comments.Commands.PostComment
{
    public class PostCommentCommandRequest : IRequest<PostCommentCommandResponse>
    {
        public string Content { get; set; }
        public string UserId { get; set; }
        public int SolutionId { get; set; }
        public int? ParentCommentId { get; set; }
    }
}
