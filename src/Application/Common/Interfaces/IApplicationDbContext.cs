namespace AlgoCode.Application.Common.Interfaces;

public interface IApplicationDbContext
{
    DbSet<Tag> Tags { get; }
    DbSet<Problem> Problems { get; }
    DbSet<TestCase> TestCases { get; }
    DbSet<Submission> Submissions { get; }
    DbSet<Session> Sessions { get; }
    DbSet<UserProblemStatus> UserProblemStatuses { get; }
    DbSet<Subscription> Subscriptions { get; }
    DbSet<Solution> Solutions { get; }
    Task<int> SaveChangesAsync(CancellationToken cancellationToken);
}
