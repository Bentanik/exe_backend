namespace exe_backend.Persistence.Repositories;

public class LectureRepository(ApplicationDbContext context) : RepositoryBase<Lecture, Guid>(context), ILectureRepository
{
    public async Task<ICollection<Lecture>> GetLecturesNotChapterAsync(Guid[] lectureIds)
    {
        var result = await context.Lectures.Where(ct => lectureIds.Contains(ct.Id)).ToListAsync();

        return result;
    }
}