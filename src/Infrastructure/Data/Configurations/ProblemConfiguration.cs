namespace AlgoCode.Infrastructure.Data.Configurations;

public class ProblemConfiguration : IEntityTypeConfiguration<Problem>
{
    public void Configure(EntityTypeBuilder<Problem> builder)
    {
        builder.HasMany(p => p.Users)
            .WithMany(u => u.Problems);

        builder.HasMany(p => p.Contests)
            .WithMany(c => c.Problems);

        builder.HasMany(p => p.Submissions)
            .WithOne(s => s.Problem);

        builder.HasMany(p => p.Tags)
           .WithMany(t => t.Problems);

        builder.HasMany(p => p.StudyPlans)
            .WithMany(sp => sp.Problems);

        builder.HasMany(p => p.MockAssesments)
            .WithMany(ma => ma.Problems);

        builder.HasMany(p => p.Sessions)
            .WithMany(s => s.Problems);
    }
}
