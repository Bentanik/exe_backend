using System.Security.Claims;
using System.Text;
using exe_backend.Contract.Common.Enums;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;

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
            .Configure<PayOSSetting>(configuration.GetSection(PayOSSetting.SectionName));

        return services;
    }


    public static IServiceCollection AddAuthenticationAndAuthorization(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddSignalR()
          .AddJsonProtocol(options =>
          {
              options.PayloadSerializerOptions.PropertyNamingPolicy = null;
          });

        var authSetting = new AuthSetting();
        configuration.GetSection(AuthSetting.SectionName).Bind(authSetting);


        services.AddAuthentication(options =>
        {
            options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
        })
       .AddJwtBearer(options =>
       {
           options.SaveToken = true;
           options.TokenValidationParameters = new TokenValidationParameters
           {
               ValidateIssuer = false,
               ValidateAudience = false,
               ValidateLifetime = true,
               ValidateIssuerSigningKey = true,
               ValidIssuer = authSetting.Issuer,
               ValidAudience = authSetting.Audience,
               IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(authSetting.AccessSecretToken)),
               ClockSkew = TimeSpan.Zero
           };

           options.Events = new JwtBearerEvents
           {
               OnAuthenticationFailed = context =>
               {
                   if (context.Exception.GetType() == typeof(SecurityTokenExpiredException))
                   {
                       context.Response.Headers.Add("IS-TOKEN-EXPIRED", "true");
                   }
                   return Task.CompletedTask;
               },
           };
       });

        services.AddAuthorization(options =>
        {
            // Admin Policy
            options.AddPolicy(RoleEnum.Admin.ToString(), policy =>
                policy.RequireClaim(ClaimTypes.Role, RoleEnum.Admin.ToString()).ToString());

            // Member Policy
            options.AddPolicy(RoleEnum.Member.ToString(), policy =>
                policy.RequireClaim(ClaimTypes.Role, RoleEnum.Member.ToString()).ToString());
        });

        return services;
    }

    public static IServiceCollection AddApiService(this IServiceCollection services, IConfiguration configuration)
    {
        return services;
    }
}