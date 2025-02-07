using Microsoft.AspNetCore.Builder;

namespace exe_backend.Persistence.DepedencyInjection.Extensions;

public static class DatabaseExtention
{
    public static async Task InitialiseDatabaseAsync(this WebApplication app)
    {
        using var scope = app.Services.CreateScope();

        var context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
        context.Database.MigrateAsync().GetAwaiter().GetResult();

        await Task.CompletedTask;
    }
}
