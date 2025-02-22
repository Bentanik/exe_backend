using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace exe_backend.Persistence.Configurations;


public class LevelConfiguration : IEntityTypeConfiguration<Level>
{
    public void Configure(EntityTypeBuilder<Level> builder)
    {
        builder.HasKey(o => o.Id);

        builder.HasMany(ct => ct.Courses)
               .WithOne(c => c.Level)
               .HasForeignKey(c => c.LevelId)
               .OnDelete(DeleteBehavior.SetNull)
               .HasConstraintName("FK_Level_Course_CourseId");

        builder.Property(ct => ct.QuantityCourses);
    }
}