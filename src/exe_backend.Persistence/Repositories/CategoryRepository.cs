namespace exe_backend.Persistence.Repositories;

public class CategoryRepository(ApplicationDbContext context) : RepositoryBase<Category, Guid>(context), ICategoryRepository
{
}