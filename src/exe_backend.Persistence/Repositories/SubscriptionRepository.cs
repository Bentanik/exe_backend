namespace exe_backend.Persistence.Repositories;

public class SubcriptionRepository(ApplicationDbContext context) : RepositoryBase<Subscription, Guid>(context), ISubscriptionRepository
{
    public async Task<List<Subscription>> GetExpiredSubscriptionsAsync()
    {
        var result = await context.Subscriptions.Where(s => s.EndDate.CompareTo(DateTime.UtcNow) < 0).ToListAsync();

        return result;
    }
}