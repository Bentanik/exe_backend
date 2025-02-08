using exe_backend.Application.Persistence.Repository;

namespace exe_backend.Application.Persistence;

public interface IUnitOfWork : IAsyncDisposable
{
    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
    IUserRepository UserRepository { get; }
    IRoleRepository RoleRepository { get; }
}