namespace exe_backend.Domain.Models;

public class SubscriptionPackage : DomainEntity<Guid>
{
    public string Name { get; private set; } = default!;
    public long Price { get; private set; } = default!;
    public int ExpiredMonth { get; private set; } = default!;
    public string? Description { get; private set; }

    public static SubscriptionPackage Create(Guid id, string name, long price, int expiredMonth, string? description = null)
    {
        var subcriptionPackage = new SubscriptionPackage
        {
            Id = id,
            Name = name,
            Price = price,
            ExpiredMonth = expiredMonth,
            Description = description,
            IsDeleted = false
        };

        return subcriptionPackage;
    }
}