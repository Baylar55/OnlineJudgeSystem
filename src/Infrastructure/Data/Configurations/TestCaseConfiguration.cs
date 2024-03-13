namespace AlgoCode.Infrastructure.Data.Configurations
{
    public class TestCaseConfiguration : IEntityTypeConfiguration<TestCase>
    {
        public void Configure(EntityTypeBuilder<TestCase> builder)
        {
            builder.HasOne(tc => tc.Problem)
                   .WithMany(p => p.TestCases)
                   .HasForeignKey(tc => tc.ProblemId);
        }
    }
}
