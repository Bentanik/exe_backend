namespace exe_backend.Application.Persistence.Repository;

public interface IChapterRepository : IRepositoryBase<Chapter, Guid>
{
    Task<ICollection<Chapter>> GetChaptersNotCourseAsync(Guid[] chapterIds);
}
