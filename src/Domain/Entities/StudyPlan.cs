using AlgoCode.Domain.Entities.Identity;

namespace AlgoCode.Domain.Entities;

public class StudyPlan : BaseAuditableEntity
{
    public string Title { get; set; } = null!;
    public string Description { get; set; } = null!;
    public ICollection<Problem> Problems { get; set; } = null!;
    public ICollection<ApplicationUser>? Users { get; set; }
}
