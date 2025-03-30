using System.Text.Json.Serialization;

namespace exe_backend.Domain.Models;

public class Donate : DomainEntity<Guid>
{
    public int Amount { get; private set; }
    public string Description { get; private set; } = string.Empty;
    public long OrderId { get; private set; }

    public Guid? UserId { get; set; }
    [JsonIgnore]
    public User? User { get; set; }

    public static Donate CreateDonate(int amount, string description, long orderId)
    {
        var donate = new Donate
        {
            Amount = amount,
            Description = description,
            OrderId = orderId
        };
        return donate;
    }

    public void AssignUser(Guid userId)
    {
        UserId = userId;
    }
}
