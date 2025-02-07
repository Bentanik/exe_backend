namespace exe_backend.Api.DepedencyInjection.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddConfigurationAppSetting
     (this IServiceCollection services, IConfiguration configuration)
    {
        services
            .Configure<EmailSetting>(configuration.GetSection(EmailSetting.SectionName))
            .Configure<AuthSetting>(configuration.GetSection(AuthSetting.SectionName))
            .Configure<CloudinarySetting>(configuration.GetSection(CloudinarySetting.SectionName))
            .Configure<UserSetting>(configuration.GetSection(UserSetting.SectionName));

        return services;
    }
}