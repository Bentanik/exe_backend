using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace exe_backend.Persistence.Configurations;


public class CategoryConfiguration : IEntityTypeConfiguration<Category>
{
    public void Configure(EntityTypeBuilder<Category> builder)
    {
        builder.HasKey(o => o.Id);

        // builder.HasOne<Category>()
        //         .WithMany(c => )
        //         .OnDelete(DeleteBehavior.SetNull)
        //         .HasConstraintName("FK_Category_Course_CourseId");

        builder.HasMany(ct => ct.Courses)
                .WithOne(c => c.Category)
               .HasForeignKey(c => c.CategoryId)
               .OnDelete(DeleteBehavior.SetNull)
               .HasConstraintName("FK_Category_Course_CourseId");

        builder.Property(ct => ct.QuantityCourses);
    }
}