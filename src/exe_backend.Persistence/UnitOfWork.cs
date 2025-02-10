using exe_backend.Application.Persistence;

namespace exe_backend.Persistence;

public class UnitOfWork : IUnitOfWork
{
    private readonly ApplicationDbContext _context;

    public UnitOfWork(ApplicationDbContext context, IUserRepository userRepository, IRoleRepository roleRepository, ICourseRepository courseRepository, IChapterRepository chapterRepository, ILectureRepository lectureRepository)
    {
        _context = context;
        UserRepository = userRepository;
        RoleRepository = roleRepository;
        CourseRepository = courseRepository;
        ChapterRepository = chapterRepository;
        LectureRepository = lectureRepository;
    }

    public async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
     => await _context.SaveChangesAsync();

    async ValueTask IAsyncDisposable.DisposeAsync()
        => await _context.DisposeAsync();

    public IUserRepository UserRepository { get; }
    public IRoleRepository RoleRepository { get; }
    public ICourseRepository CourseRepository { get; }
    public IChapterRepository ChapterRepository { get; }
    public ILectureRepository LectureRepository { get; }
}
