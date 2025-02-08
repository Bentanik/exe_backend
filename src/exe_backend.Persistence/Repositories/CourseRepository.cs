namespace exe_backend.Persistence.Repositories;

public class CourseRepository(ApplicationDbContext context) : RepositoryBase<Course, Guid>(context), ICourseRepository
{
}