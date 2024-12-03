namespace AlgoCode.Infrastructure.Data.Configurations;

public class MockAssesmentConfiguration : IEntityTypeConfiguration<MockAssesment>
{
    public void Configure(EntityTypeBuilder<MockAssesment> builder)
    {
        builder.HasMany(ma => ma.Users)
               .WithMany(u => u.MockAssesments);
        builder.HasMany(ma => ma.Problems)
               .WithMany(p => p.MockAssesments);
    }
}
