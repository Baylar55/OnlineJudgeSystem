using Microsoft.AspNetCore.Identity;

namespace AlgoCode.Domain.Identity;

public class ApplicationUser : IdentityUser
{
    public ICollection<Problem>? Problems { get; set; }
    public ICollection<Contest>? Contests { get; set; }
    public ICollection<Submission>? Submissions { get; set; }
    public ICollection<StudyPlan>? StudyPlans { get; set; }
    public ICollection<MockAssesment>? MockAssesments { get; set; }
    public ICollection<Session> Sessions { get; set; } = new List<Session>();
}
