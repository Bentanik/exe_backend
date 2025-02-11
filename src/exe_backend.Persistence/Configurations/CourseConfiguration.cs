using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace exe_backend.Persistence.Configurations;


public class CourseConfiguration : IEntityTypeConfiguration<Domain.Models.Course>
{
    public void Configure(EntityTypeBuilder<Course> builder)
    {
        // Configure Id
        builder.HasKey(o => o.Id);

        // Configure Thumbnail
        builder.OwnsOne(
        o => o.Thumbnail, identityBuilder =>
        {
            identityBuilder.Property(i => i.PublicId)
                .IsRequired();

            identityBuilder.Property(i => i.PublicUrl)
            .IsRequired();
        });

        builder.HasMany(c => c.Chapters)
               .WithOne(ch => ch.Course)
               .HasForeignKey(ch => ch.CourseId)
               .OnDelete(DeleteBehavior.SetNull)
               .HasConstraintName("FK_Chapter_Course_CourseId");

        builder.Property(c => c.QuantityChapters);
    }
}
