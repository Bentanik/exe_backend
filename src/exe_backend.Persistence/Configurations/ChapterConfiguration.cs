using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace exe_backend.Persistence.Configurations;

public class ChapterConfiguration : IEntityTypeConfiguration<Chapter>
{
    public void Configure(EntityTypeBuilder<Chapter> builder)
    {
        builder.HasKey(o => o.Id);

        builder.HasOne<Course>()
                .WithMany(c => c.Chapters)
                .HasForeignKey(ch => ch.CourseId)
                .OnDelete(DeleteBehavior.SetNull)
                .HasConstraintName("FK_Chapter_Course_CourseId");

        builder.Property(ct => ct.QuantityLectures);
        builder.Property(ct => ct.TotalDurationLectures);

    }
}