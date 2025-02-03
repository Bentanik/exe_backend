namespace exe_backend.Api.DepedencyInjection.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddConfigurationAppSetting
     (this IServiceCollection services, IConfiguration configuration)
    {
        services
            .Configure<EmailSetting>(configuration.GetSection(EmailSetting.SectionName));

        return services;
    }
}