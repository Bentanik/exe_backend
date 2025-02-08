using exe_backend.Persistence.DepedencyInjection.Options;
using Microsoft.AspNetCore.Builder;

namespace exe_backend.Persistence.DepedencyInjection.Extensions;

public static class DatabaseExtention
{
    public static async Task InitialiseDatabaseAsync(this WebApplication app)
    {
        using var scope = app.Services.CreateScope();

        var context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
        var initialData = scope.ServiceProvider.GetRequiredService<InitialData>();
        context.Database.MigrateAsync().GetAwaiter().GetResult();

        await SeedAsync(context, initialData);
    }

    private static async Task SeedAsync(ApplicationDbContext context, InitialData initialData)
    {
        await SeedRolesAsync(context, initialData);
    }

    private static async Task SeedRolesAsync(ApplicationDbContext context, InitialData initialData)
    {
        if (!await context.Roles.AnyAsync())
        {
            var roles = InitialData.GetRoles();
            await context.Roles.AddRangeAsync(roles);
            await context.SaveChangesAsync();
        }
    }
}
