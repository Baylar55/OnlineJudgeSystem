namespace AlgoCode.Domain.Entities;

public class Session : BaseAuditableEntity
{
    public string Title { get; set; } = null!;
    public bool IsActive { get; set; }
    public string UserId { get; set; }
    public ApplicationUser User { get; set; } = null!;
    public ICollection<Problem> Problems { get; set; } = new List<Problem>();
    public ICollection<Submission> Submissions { get; set; } = new List<Submission>();
}
