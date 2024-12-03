namespace AlgoCode.Domain.Entities;

public class Solution : BaseAuditableEntity
{
    public string Title { get; set; } = null!;
    public string Description { get; set; } = null!;
    public Submission Submission { get; set; }
    public int SubmissionId { get; set; }
    public ICollection<Comment>? Comments { get; set; }
}

