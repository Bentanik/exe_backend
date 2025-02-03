using exe_backend.Infrastructure.Services;
using Notification.API.Infrastructure.Services;

namespace exe_backend.Infrastructure.DepedencyInjection.Extensions;

public static class ServiceCollectionExtensions
{
    public static void AddConfigurationRedis
       (this IServiceCollection services, IConfiguration configuration)
    {
        services.AddSingleton<IConnectionMultiplexer>
            (_ => ConnectionMultiplexer.Connect(configuration.GetConnectionString("Redis")!));

        services.AddStackExchangeRedisCache(options =>
        {
            options.Configuration = configuration.GetConnectionString("Redis");
        });

        services.AddSingleton<IResponseCacheService, ResponseCacheService>();
    }

    public static IServiceCollection AddInfrastructureServices(this IServiceCollection services)
    {
        services.AddScoped<IPasswordHashService, PasswordHashService>()
                .AddScoped<IEmailService, EmailService>();
        return services;
    }
}