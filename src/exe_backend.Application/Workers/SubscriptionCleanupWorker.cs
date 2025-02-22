using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace exe_backend.Application.Workers;

public class SubscriptionCleanupWorker : BackgroundService
{
    private readonly IServiceProvider _serviceProvider;
    private readonly ILogger<SubscriptionCleanupWorker> _logger;

    public SubscriptionCleanupWorker(IServiceProvider serviceProvider, ILogger<SubscriptionCleanupWorker> logger)
    {
        _serviceProvider = serviceProvider;
        _logger = logger;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        _logger.LogInformation("Subscription Cleanup Service is starting.");

        while (!stoppingToken.IsCancellationRequested)
        {
            await ProcessExpiredSubscriptions(stoppingToken);
            await Task.Delay(TimeSpan.FromDays(1), stoppingToken); // Run after 1 day
        }

        _logger.LogInformation("Subscription Cleanup Service is stopping.");
    }

    private async Task ProcessExpiredSubscriptions(CancellationToken stoppingToken)
    {
        using (var scope = _serviceProvider.CreateScope())
        {
            _logger.LogInformation("Start delete");
            var unitOfWork = scope.ServiceProvider.GetRequiredService<IUnitOfWork>();

            var expiredSubscriptions = await unitOfWork.SubscriptionRepository.GetExpiredSubscriptionsAsync();

            if (expiredSubscriptions != null && expiredSubscriptions.Count > 0)
            {
                unitOfWork.SubscriptionRepository.RemoveMultiple(expiredSubscriptions);

                await unitOfWork.SaveChangesAsync(stoppingToken);
            }
        }
    }
}