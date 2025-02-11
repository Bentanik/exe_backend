namespace exe_backend.Persistence.Repositories;

public class ChapterRepository(ApplicationDbContext context) : RepositoryBase<Chapter, Guid>(context), IChapterRepository
{
}