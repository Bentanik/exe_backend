using exe_backend.Application.Persistence;
using exe_backend.Application.Persistence.Repository;
using exe_backend.Persistence.Repositories;

namespace exe_backend.Persistence.DepedencyInjection.Extensions;

public static class ServiceCollectionExtensions
{
    public static void AddSqlConfiguration(this IServiceCollection services)
    {
        services.AddDbContextPool<DbContext, ApplicationDbContext>((provider, builder) =>
        {
            var configuration = provider.GetRequiredService<IConfiguration>();

            builder
            .EnableDetailedErrors(true)
            .EnableSensitiveDataLogging(true)
            .UseLazyLoadingProxies(false) // => If UseLazyLoadingProxies, all of the navigation fields should be VIRTUAL
            .UseSqlServer(
                connectionString: configuration.GetConnectionString("Database"),
                    sqlServerOptionsAction: optionsBuilder
                        => optionsBuilder
                        .MigrationsAssembly(typeof(ApplicationDbContext).Assembly.GetName().Name));
        });
    }

    public static IServiceCollection AddPersistenceService(this IServiceCollection services)
    {
        services.AddScoped<IUnitOfWork, UnitOfWork>()
            .AddScoped(typeof(IRepositoryBase<,>), typeof(RepositoryBase<,>))
            .AddScoped<IUserRepository, UserRepository>();
            
        return services;
    }
}
