namespace AlgoCode.Infrastructure.Data.Configurations
{
    public class SessionConfiguration : IEntityTypeConfiguration<Session>
    {
        public void Configure(EntityTypeBuilder<Session> builder)
        {
            builder.HasOne(s => s.User)
                .WithMany(u => u.Sessions)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
