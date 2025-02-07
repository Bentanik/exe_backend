using exe_backend.Domain.Abstractions;

namespace exe_backend.Domain.Models;

public class User : DomainEntity<Guid>
{
    public string Email { get; private set; } = default!;
    public string Password { get; private set; } = default!;
    public string FullName { get; private set; } = default!;
    public bool IsActive { get; private set; } = default!; // Active email
    public string? PublicAvatarId { get; private set; }
    public string? PublicAvatarUrl { get; private set; }
    public static User Create(Guid Id, string email, string password, string fullName, string? publicMediaId = null, string? publicMediaUrl = null)
    {
        return new User
        {
            Id = Id,
            Email = email,
            Password = password,
            FullName = fullName,
            IsActive = false,
            IsDeleted = false,
            PublicAvatarId = publicMediaId,
            PublicAvatarUrl = publicMediaUrl
        };
    }
}