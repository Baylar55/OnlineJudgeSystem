namespace AlgoCode.Application.Features.Comments.Commands.PostComment
{
    public class PostCommentCommandResponse
    {
        public int Id { get; set; }
        public string Content { get; set; }
        public string UserName { get; set; }
        public string? UserPhoto { get; set; }
        public int SolutionId { get; set; }
        public int? ParentCommentId { get; set; }
        public DateTimeOffset Created { get; set; }
    }
}
