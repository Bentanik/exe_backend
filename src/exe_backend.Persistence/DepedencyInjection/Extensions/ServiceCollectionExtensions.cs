using exe_backend.Application.Persistence;
using exe_backend.Persistence.DepedencyInjection.Options;
using exe_backend.Persistence.Interceptors;
using exe_backend.Persistence.Repositories;
using Microsoft.EntityFrameworkCore.Diagnostics;

namespace exe_backend.Persistence.DepedencyInjection.Extensions;

public static class ServiceCollectionExtensions
{
    public static void AddSqlConfiguration(this IServiceCollection services)
    {
        services.AddSingleton<ISaveChangesInterceptor, AuditableEntityInterceptor>();

        // Regist InitialData
        services.AddSingleton<InitialData>();

        services.AddDbContextPool<DbContext, ApplicationDbContext>((provider, builder) =>
        {
            var configuration = provider.GetRequiredService<IConfiguration>();

            builder
            .EnableDetailedErrors(true)
            .EnableSensitiveDataLogging(false)
            .UseLazyLoadingProxies(false) // => If UseLazyLoadingProxies, all of the navigation fields should be VIRTUAL
            .UseSqlServer(
                connectionString: configuration.GetConnectionString("Database"),
                    sqlServerOptionsAction: optionsBuilder
                        => optionsBuilder
                        .MigrationsAssembly(typeof(ApplicationDbContext).Assembly.GetName().Name));

            var interceptor = provider.GetService<ISaveChangesInterceptor>();
            if (interceptor is not null)
            {
                builder.AddInterceptors(interceptor);
            }
        });
    }

    public static IServiceCollection AddPersistenceServices(this IServiceCollection services)
    {
        services.AddScoped<IUnitOfWork, UnitOfWork>()
            .AddScoped(typeof(IRepositoryBase<,>), typeof(RepositoryBase<,>))
            .AddScoped<IUserRepository, UserRepository>()
            .AddScoped<IRoleRepository, RoleRepository>()
            .AddScoped<ICourseRepository, CourseRepository>()
            .AddScoped<IChapterRepository, ChapterRepository>()
            .AddScoped<ILectureRepository, LectureRepository>()
            .AddScoped<ICategoryRepository, CategoryRepository>()
            .AddScoped<ILevelRepository, LevelRepository>()
            .AddScoped<ISubscriptionRepository, SubcriptionRepository>()
            .AddScoped<ISubscriptionRepositoryPackage, SubscriptionRepositoryPackage>()
            .AddScoped<IDonateRepository, DonateRepository>();

        return services;
    }
}
