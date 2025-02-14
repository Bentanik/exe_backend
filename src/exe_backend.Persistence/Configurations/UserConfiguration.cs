using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace exe_backend.Persistence.Configurations;

public class UserConfiguration : IEntityTypeConfiguration<Domain.Models.User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        // Configure UserSubcription
        builder.OwnsOne(
        o => o.UserSubcription, identityBuilder =>
        {
            identityBuilder.Property(c => c.StartDate)
                .IsRequired();

            identityBuilder.Property(c => c.EndDate)
                .IsRequired();
            
            identityBuilder.Property(c => c.Type)
                .IsRequired();
        });
    }
}