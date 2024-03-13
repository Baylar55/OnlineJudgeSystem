namespace AlgoCode.Infrastructure.Data.Configurations
{
    public class StudyPlanConfiguration : IEntityTypeConfiguration<StudyPlan>
    {
        public void Configure(EntityTypeBuilder<StudyPlan> builder)
        {
            builder.HasMany(sp => sp.Users)
                .WithMany(u => u.StudyPlans);

            builder.HasMany(sp => sp.Problems)
                .WithMany(p => p.StudyPlans);
        }
    }
}
