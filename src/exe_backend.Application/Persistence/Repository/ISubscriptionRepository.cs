namespace exe_backend.Application.Persistence.Repository;

public interface ISubscriptionRepository : IRepositoryBase<Subscription, Guid>
{
    Task<List<Subscription>> GetExpiredSubscriptionsAsync();
}