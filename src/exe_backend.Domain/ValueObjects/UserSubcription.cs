using exe_backend.Domain.Enums;

namespace exe_backend.Domain.ValueObjects;

public record UserSubscription
{
    public SubscriptionType Type { get; private set; }
    public DateTime StartDate { get; private set; }
    public DateTime EndDate { get; private set; }

    private UserSubscription() { }

    public UserSubscription(SubscriptionType type)
    {
        Type = type;
        StartDate = DateTime.Now;
        EndDate = CalculateEndDate(type);
    }

    // If the registration time has expired, it returns false, otherwise it returns true
    public bool IsActive() => EndDate > DateTime.Now;

    private static DateTime CalculateEndDate(SubscriptionType type) => type switch
    {
        SubscriptionType.Premium1Month => DateTime.Now.AddMonths(1),
        SubscriptionType.Premium3Months => DateTime.Now.AddMonths(3),
        SubscriptionType.Premium6Months => DateTime.Now.AddMonths(6),
        _ => DateTime.Now
    };

    // 1. Extend the subscription by a given number of months
    public UserSubscription ExtendSubscription(int months)
    {
        return this with { EndDate = EndDate.AddMonths(months) };
    }

    // 2. Cancel the subscription by setting EndDate to the current time
    public UserSubscription CancelSubscription()
    {
        return this with { EndDate = DateTime.Now };
    }

    // 3. Get the remaining days before expiration
    public int RemainingDays()
    {
        return (EndDate - DateTime.Now).Days;
    }

    // 4. Renew the subscription if it has expired
    public UserSubscription RenewSubscription()
    {
        if (IsActive()) return this; // If still active, no need to renew

        return new UserSubscription(Type); // Create a new subscription with the same type
    }
}
