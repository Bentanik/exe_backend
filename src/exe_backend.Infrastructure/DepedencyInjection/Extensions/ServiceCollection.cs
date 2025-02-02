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
}