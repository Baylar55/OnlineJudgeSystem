using AlgoCode.Application.Common.Interfaces;
using AlgoCode.Domain.Entities.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using System.Reflection;

namespace AlgoCode.Infrastructure.Data;

public class ApplicationDbContext : IdentityDbContext<ApplicationUser>, IApplicationDbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }
    public DbSet<Tag> Tags => Set<Tag>();
    public DbSet<Problem> Problems => Set<Problem>();
    public DbSet<TestCase> TestCases => Set<TestCase>();
    public DbSet<Submission> Submissions => Set<Submission>();
    public DbSet<Session> Sessions => Set<Session>();
    public DbSet<UserProblemStatus> UserProblemStatuses => Set<UserProblemStatus>();
    public DbSet<Subscription> Subscriptions => Set<Subscription>();
    public DbSet<Solution> Solutions => Set<Solution>();
    public DbSet<Comment> Comments => Set<Comment>();
    public DbSet<Contest> Contests => Set<Contest>();
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
    }
}
