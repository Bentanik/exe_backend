using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace exe_backend.Persistence.Configurations;

public class UserConfiguration : IEntityTypeConfiguration<Domain.Models.User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        // Config relationship 1-1 User and Subcription
        builder.HasOne(u => u.Subscription)
            .WithOne(s => s.User)
            .HasForeignKey<Subscription>(s => s.UserId)
            .OnDelete(DeleteBehavior.SetNull);
    }
}