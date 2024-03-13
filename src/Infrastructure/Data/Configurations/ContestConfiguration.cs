namespace AlgoCode.Infrastructure.Data.Configurations
{
    public class ContestConfiguration : IEntityTypeConfiguration<Contest>
    {
        public void Configure(EntityTypeBuilder<Contest> builder)
        {
            builder.HasMany(c => c.Users)
                   .WithMany(u => u.Contests);

            builder.HasMany(c => c.Problems)
                   .WithMany(p => p.Contests);
        }
    }
}
