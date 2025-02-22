using System.Security.Claims;
using exe_backend.Contract.Common.Enums;
using FirebaseAdmin;
using Google.Apis.Auth.OAuth2;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;

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
            .Configure<UserSetting>(configuration.GetSection(UserSetting.SectionName))
            .Configure<AdminSetting>(configuration.GetSection(AdminSetting.SectionName))
            .Configure<ClientSetting>(configuration.GetSection(ClientSetting.SectionName))
            .Configure<PayOSSetting>(configuration.GetSection(PayOSSetting.SectionName))
            .Configure<FirebaseSetting>(configuration.GetSection(FirebaseSetting.SectionName));

        return services;
    }


    public static IServiceCollection AddAuthenticationAndAuthorization(this IServiceCollection services, IConfiguration configuration)
    {
        var authSetting = new AuthSetting();
        configuration.GetSection(AuthSetting.SectionName).Bind(authSetting);

        services.AddSingleton(FirebaseApp.Create(new AppOptions
        {
            Credential = GoogleCredential.FromFile(configuration["FirebaseSetting:PrivateKey"])
        }));

        services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddScheme<AuthenticationSchemeOptions, FirebaseAuthenticationHandler>(JwtBearerDefaults.AuthenticationScheme, (o) => { });

        services.AddAuthorization(options =>
        {
            // Admin Policy
            options.AddPolicy(RoleEnum.Admin.ToString(), policy =>
                policy.RequireClaim(ClaimTypes.Role, RoleEnum.Admin.ToString()).ToString());

            // Member Policy
            options.AddPolicy(RoleEnum.Member.ToString(), policy =>
                policy.RequireClaim(ClaimTypes.Role, RoleEnum.Member.ToString()).ToString());

            options.AddPolicy(RoleEnum.AdminAndMember.ToString(), policy =>
               policy.RequireAssertion(context =>
                   context.User.HasClaim(c => c.Type == ClaimTypes.Role &&
                       (c.Value == RoleEnum.Member.ToString() ||
                        c.Value == RoleEnum.Admin.ToString()))
               ));
        });

        return services;
    }

    public static IServiceCollection AddApiService(this IServiceCollection services, IConfiguration configuration)
    {
        return services;
    }
}