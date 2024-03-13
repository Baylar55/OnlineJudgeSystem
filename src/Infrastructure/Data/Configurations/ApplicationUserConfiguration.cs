using AlgoCode.Domain.Identity;

namespace AlgoCode.Infrastructure.Data.Configurations
{
    public class ApplicationUserConfiguration : Microsoft.EntityFrameworkCore.IEntityTypeConfiguration<ApplicationUser>
    {
        public void Configure(EntityTypeBuilder<ApplicationUser> builder)
        {
            builder.HasMany(u => u.Problems)
                   .WithMany(p => p.Users);

            builder.HasMany(u => u.Contests)
                   .WithMany(c => c.Users);

            builder.HasMany(u => u.Submissions)
                   .WithOne(s => s.User);

            builder.HasMany(u => u.StudyPlans)
                   .WithMany(sp => sp.Users);

            builder.HasMany(u => u.MockAssesments)
                   .WithMany(ma => ma.Users);

            builder.HasMany(u => u.Sessions)
                   .WithOne(s => s.User);
        }
    }
}
