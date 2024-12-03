namespace AlgoCode.Application.Features.Comments.Commands.PostComment;

public record PostCommentCommandResponse(int Id, string Content, string UserName, string? UserPhoto, int SolutionId, int? ParentCommentId, DateTimeOffset Created);
