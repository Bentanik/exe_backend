namespace exe_backend.Persistence.Repositories;

public class ChapterRepository(ApplicationDbContext context) : RepositoryBase<Chapter, Guid>(context), IChapterRepository
{
    public async Task<ICollection<Chapter>> GetChaptersNotCourseAsync(Guid[] chapterIds)
    {
        var result = await context.Chapters.Where(ct => chapterIds.Contains(ct.Id)).ToListAsync();

        return result;
    }
}