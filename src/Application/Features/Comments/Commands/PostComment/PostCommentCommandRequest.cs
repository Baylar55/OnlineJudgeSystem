namespace AlgoCode.Application.Features.Comments.Commands.PostComment;

public record PostCommentCommandRequest(string Content, string UserId, int SolutionId, int? ParentCommentId) : IRequest<PostCommentCommandResponse>;
