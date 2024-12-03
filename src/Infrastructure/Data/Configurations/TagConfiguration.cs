namespace AlgoCode.Infrastructure.Data.Configurations;

public class TagConfiguration : IEntityTypeConfiguration<Tag>
{
    public void Configure(EntityTypeBuilder<Tag> builder)
    {
        builder.HasMany(t => t.Problems)
               .WithMany(p => p.Tags);
    }
}
