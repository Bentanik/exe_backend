using exe_backend.Application.Persistence;

namespace exe_backend.Persistence;

public class UnitOfWork : IUnitOfWork, IAsyncDisposable, IDisposable
{
    private readonly ApplicationDbContext _context;

    public UnitOfWork(ApplicationDbContext context, IUserRepository userRepository, IRoleRepository roleRepository, ICourseRepository courseRepository, IChapterRepository chapterRepository, ILectureRepository lectureRepository, ICategoryRepository categoryRepository, ILevelRepository levelRepository, ISubscriptionRepository subscriptionRepository, ISubscriptionRepositoryPackage subscriptionPackageRepository)
    {
        _context = context;
        UserRepository = userRepository;
        RoleRepository = roleRepository;
        CourseRepository = courseRepository;
        ChapterRepository = chapterRepository;
        LectureRepository = lectureRepository;
        CategoryRepository = categoryRepository;
        LevelRepository = levelRepository;
        SubscriptionRepository = subscriptionRepository;
        SubscriptionPackageRepository = subscriptionPackageRepository;
    }

    public async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
     => await _context.SaveChangesAsync();

    async ValueTask IAsyncDisposable.DisposeAsync()
        => await _context.DisposeAsync();

    public void Dispose()
    {
        _context.Dispose();
        GC.SuppressFinalize(this);
    }

    public IUserRepository UserRepository { get; }
    public IRoleRepository RoleRepository { get; }
    public ICourseRepository CourseRepository { get; }
    public IChapterRepository ChapterRepository { get; }
    public ILectureRepository LectureRepository { get; }
    public ICategoryRepository CategoryRepository { get; }
    public ILevelRepository LevelRepository { get; }
    public ISubscriptionRepository SubscriptionRepository { get; }
    public ISubscriptionRepositoryPackage SubscriptionPackageRepository { get; }
}
