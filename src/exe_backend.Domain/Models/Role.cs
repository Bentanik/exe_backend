using System.Text.Json.Serialization;

namespace exe_backend.Domain.Models;

public class Role : DomainEntity<Guid>
{
    public string Name { get; private set; } = default!;
    
    [JsonIgnore]
    public List<User> Users { get; private set; } = [];
    public static Role Create(Guid id, string Name)
    {
        var role = new Role
        {
            Id = id,
            Name = Name
        };

        return role;
    }
}