using exe_backend.Domain.Models;

namespace exe_backend.Application.Persistence.Repository;

public interface IUserRepository : IRepositoryBase<User, Guid>
{
}
