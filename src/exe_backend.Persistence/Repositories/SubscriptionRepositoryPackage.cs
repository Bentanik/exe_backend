namespace exe_backend.Persistence.Repositories;

public class SubscriptionRepositoryPackage(ApplicationDbContext context) : RepositoryBase<SubscriptionPackage, Guid>(context), ISubscriptionRepositoryPackage
{
}