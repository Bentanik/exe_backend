namespace exe_backend.Persistence.Repositories;

public class RoleRepository(ApplicationDbContext context) : RepositoryBase<Role, Guid>(context), IRoleRepository
{
}