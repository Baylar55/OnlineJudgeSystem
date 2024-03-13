namespace AlgoCode.Infrastructure.Data.Configurations
{
    public class SessionConfiguration : IEntityTypeConfiguration<Session>
    {
        public void Configure(EntityTypeBuilder<Session> builder)
        {
            builder.HasMany(s => s.Problems)
                .WithMany(u => u.Sessions);
        }
    }
}
