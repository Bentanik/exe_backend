using System.Reflection;
using exe_backend.Infrastructure.Masstransit;
using exe_backend.Infrastructure.Services;
using FirebaseAdmin;
using Google.Apis.Auth.OAuth2;
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


    public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddMessageBroker(configuration, Assembly.GetExecutingAssembly());

        FirebaseApp.Create(new AppOptions
        {
            Credential = GoogleCredential.FromFile(configuration["FirebaseSetting:PrivateKey"])
        });

        services.AddScoped<IPasswordHashService, PasswordHashService>()
                .AddScoped<IEmailService, EmailService>()
                .AddScoped<ITokenGeneratorService, TokenGeneratorService>()
                .AddScoped<IMediaService, MediaService>()
                .AddScoped<IPaymentService, PaymentService>();

        return services;
    }
}