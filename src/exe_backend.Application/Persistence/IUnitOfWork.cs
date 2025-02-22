using exe_backend.Application.Persistence.Repository;

namespace exe_backend.Application.Persistence;

public interface IUnitOfWork : IAsyncDisposable, IDisposable
{
    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
    IUserRepository UserRepository { get; }
    IRoleRepository RoleRepository { get; }
    ICourseRepository CourseRepository { get; }
    IChapterRepository ChapterRepository { get; }
    ILectureRepository LectureRepository { get; }
    ICategoryRepository CategoryRepository { get; }
    ILevelRepository LevelRepository { get; }
    ISubscriptionRepository SubscriptionRepository { get; }
    ISubscriptionRepositoryPackage SubscriptionPackageRepository { get; }
}