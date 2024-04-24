namespace AlgoCode.Infrastructure.Data.Configurations
{
    public class CommentConfiguration : IEntityTypeConfiguration<Comment>
    {
        public void Configure(EntityTypeBuilder<Comment> builder)
        {
            builder.HasOne(c => c.Solution)
                   .WithMany(s => s.Comments)
                   .HasForeignKey(c => c.SolutionId)
                   .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
