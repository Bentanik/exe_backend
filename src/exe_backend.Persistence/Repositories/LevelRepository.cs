namespace exe_backend.Persistence.Repositories;

public class LevelRepository(ApplicationDbContext context) : RepositoryBase<Level, Guid>(context), ILevelRepository
{
}