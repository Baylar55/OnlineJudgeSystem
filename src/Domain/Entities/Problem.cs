using AlgoCode.Domain.Entities.Identity;

namespace AlgoCode.Domain.Entities;

public class Problem : BaseAuditableEntity
{
    public string Title { get; set; } = null!;
    public string Description { get; set; } = null!;
    public string MethodName { get; set; } = null!;
    public string CodeTemplate { get; set; } = null!;
    public int Point { get; set; }
    public ProblemDifficulty Difficulty { get; set; }
    public AccessLevel AccessLevel { get; set; }
    public ICollection<TestCase>? TestCases { get; set; }
    public ICollection<ApplicationUser>? Users { get; set; }
    public ICollection<Contest>? Contests { get; set; }
    public ICollection<Submission>? Submissions { get; set; }
    public ICollection<Tag>? Tags { get; set; }
    public ICollection<StudyPlan>? StudyPlans { get; set; }
    public ICollection<MockAssesment>? MockAssesments { get; set; }
    public ICollection<Session>? Sessions { get; set; }
    public ICollection<UserProblemStatus>? UserProblemStatuses { get; set; }
}
