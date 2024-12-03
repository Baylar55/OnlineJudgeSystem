using AlgoCode.Domain.Entities.Identity;

namespace AlgoCode.Domain.Entities;

public class Comment : BaseAuditableEntity
{
    public string Content { get; set; }
    public string UserId { get; set; }
    public ApplicationUser User { get; set; }
    public int SolutionId { get; set; }
    public Solution Solution { get; set; }
    public int? ParentCommentId { get; set; }
    public Comment? ParentComment { get; set; }
    public ICollection<Comment>? Replies { get; set; }
}
