using System.Text.Json.Serialization;

namespace exe_backend.Domain.Models;

public class User : DomainEntity<Guid>
{
    public string Email { get; private set; } = default!;
    public string FullName { get; private set; } = default!;
    public string IdentityId { get; private set; } = default!;
    public string? PublicAvatarId { get; private set; }
    public string? PublicAvatarUrl { get; private set; }

    // Role Foreign key
    public Guid RoleId { get; private set; } = default;
    [JsonIgnore]
    public Role Role { get; set; } = default!;

    // Subcription
    public Subscription? Subscription { get; private set; }

    public static User Create(Guid id, string email, string fullName, string identityId, Guid roleId, string? publicMediaId = null, string? publicMediaUrl = null)
    {
        return new User
        {
            Id = id,
            Email = email,
            FullName = fullName,
            IdentityId = identityId,
            RoleId = roleId,
            IsDeleted = false,
            PublicAvatarId = publicMediaId,
            PublicAvatarUrl = publicMediaUrl,
        };
    }

    public void Update()
    {
    }

    public void AddSubscription(Subscription subscription)
    {
        Subscription = subscription;
    }

    public void RemoveSubscription()
    {
        Subscription = null;
    }
}