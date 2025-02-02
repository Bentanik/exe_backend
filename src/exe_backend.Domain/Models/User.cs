using exe_backend.Domain.Abstractions;

namespace exe_backend.Domain.Models;

public class User : DomainEntity<Guid>
{
    public string Email { get; private set; } = default!;
    public string Password { get; private set; } = default!;

    public static User Create(Guid Id, string email, string password)
    {
        return new User
        {
            Id = Id,
            Email = email,
            Password = password
        };
    }
}