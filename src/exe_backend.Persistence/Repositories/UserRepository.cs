namespace exe_backend.Persistence.Repositories;

public class UserRepository(ApplicationDbContext context) : RepositoryBase<User, Guid>(context), IUserRepository
{
}