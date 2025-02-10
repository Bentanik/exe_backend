using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace exe_backend.Persistence.Configurations;

public class LectureConfiguration : IEntityTypeConfiguration<Lecture>
{
    public void Configure(EntityTypeBuilder<Lecture> builder)
    {
        builder.HasKey(o => o.Id);

        // Configure Image Lecture
        builder.OwnsOne(
        o => o.ImageLecture, identityBuilder =>
        {
            identityBuilder.Property(i => i.PublicId)
                .IsRequired();

            identityBuilder.Property(i => i.PublicUrl)
            .IsRequired();
        });

        // Configure Image Lecture
        builder.OwnsOne(
        o => o.VideoLecture, identityBuilder =>
        {
            identityBuilder.Property(i => i.PublicId)
                .IsRequired();

            identityBuilder.Property(i => i.Duration)
            .IsRequired();
        });

        builder.HasOne(l => l.Chapter)
            .WithMany(c => c.Lectures)
            .HasForeignKey(l => l.ChapterId)
            .OnDelete(DeleteBehavior.SetNull)
            .HasConstraintName("FK_Lecture_Chapter_ChapterId"); 

    }
}