namespace exe_backend.Persistence.Repositories;

public class LectureRepository(ApplicationDbContext context) : RepositoryBase<Lecture, Guid>(context), ILectureRepository
{
}