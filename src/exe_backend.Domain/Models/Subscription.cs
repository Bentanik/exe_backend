using System.Text.Json.Serialization;
using exe_backend.Contract.Exceptions.BussinessExceptions;

namespace exe_backend.Domain.Models;

public class Subscription : DomainEntity<Guid>
{
    public Guid SubscriptionPackageId { get; private set; }
    [JsonIgnore]
    public SubscriptionPackage SubscriptionPackage { get; set; } = default!;

    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }

    public Guid? UserId { get; set; }
    [JsonIgnore]
    public User? User { get; set; }

    // Check vip
    public bool IsActive => EndDate > DateTime.UtcNow;

    public static Subscription Create(Guid id, SubscriptionPackage subscriptionPackage, Guid? userId = null)
    {
        var subscription = new Subscription
        {
            Id = id,
            SubscriptionPackageId = subscriptionPackage.Id,
            StartDate = DateTime.UtcNow,
            EndDate = DateTime.UtcNow.AddMonths(subscriptionPackage.ExpiredMonth),
            UserId = userId,
            IsDeleted = false
        };

        return subscription;
    }

    public void Renew()
    {
        // EndDate away UTC Now is 5 days
        if ((EndDate - DateTime.UtcNow).TotalDays <= 10)
        {
            if (IsActive)
            {
                EndDate = EndDate.AddMonths(SubscriptionPackage.ExpiredMonth);
            }
            else
            {
                StartDate = DateTime.UtcNow;
                EndDate = EndDate.AddMonths(SubscriptionPackage.ExpiredMonth);
            }
        }
        else
        {
            throw new UserException.SubscriptionNotRenewException();
        }
    }

}
