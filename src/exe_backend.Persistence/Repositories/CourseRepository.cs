
namespace exe_backend.Persistence.Repositories;

public class CourseRepository(ApplicationDbContext context) : RepositoryBase<Course, Guid>(context), ICourseRepository
{
    public Task<List<Course>> FindCoursesAsync()
    {
        throw new NotImplementedException();
    }
}