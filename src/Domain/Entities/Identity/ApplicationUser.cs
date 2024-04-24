using Microsoft.AspNetCore.Identity;

namespace AlgoCode.Domain.Identity;

public class ApplicationUser : IdentityUser
{
    public string? PhotoName { get; set; }
    public ICollection<Problem>? Problems { get; set; }
    public ICollection<Contest>? Contests { get; set; }
    public ICollection<Submission>? Submissions { get; set; }
    public ICollection<StudyPlan>? StudyPlans { get; set; }
    public ICollection<MockAssesment>? MockAssesments { get; set; }
    public ICollection<Session> Sessions { get; set; } = new List<Session>();
    public ICollection<Comment>? Comments { get; set; }
    public Subscription? Subscription { get; set; }
    public int? SubscriptionId { get; set; }
    public SubscriptionStatus SubscriptionStatus { get; set; } = SubscriptionStatus.Inactive;
}
