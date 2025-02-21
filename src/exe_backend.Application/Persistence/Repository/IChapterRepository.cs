namespace exe_backend.Application.Persistence.Repository;

public interface IChapterRepository : IRepositoryBase<Chapter, Guid>
{
    Task<ICollection<Chapter>> GetChapterNotCourse(Guid[] chapterIds);
}
