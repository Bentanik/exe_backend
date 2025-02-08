using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace exe_backend.Persistence.Configurations;


public class CourseConfiguration : IEntityTypeConfiguration<Domain.Models.Course>
{
    public void Configure(EntityTypeBuilder<Course> builder)
    {
        // Configure Thumbnail
        builder.OwnsOne(
        o => o.Thumbnail, identityBuilder =>
        {
            identityBuilder.Property(i => i.PublicId)
                .IsRequired();

            identityBuilder.Property(i => i.PublicUrl)
            .IsRequired();
        });
    }
}
