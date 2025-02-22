using exe_backend.Contract.Common.Enums;
using exe_backend.Contract.Settings;
using exe_backend.Persistence.DepedencyInjection.Options;
using Microsoft.AspNetCore.Builder;

namespace exe_backend.Persistence.DepedencyInjection.Extensions;

public static class DatabaseExtention
{
    public static async Task InitialiseDatabaseAsync(this WebApplication app, IConfiguration configuration, IServiceProvider serviceProvider)
    {
        using var scope = app.Services.CreateScope();

        var context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
        var initialData = scope.ServiceProvider.GetRequiredService<InitialData>();
        context.Database.MigrateAsync().GetAwaiter().GetResult();

        await SeedAsync(context, initialData, configuration, serviceProvider);
    }

    private static async Task SeedAsync(ApplicationDbContext context, InitialData initialData, IConfiguration configuration, IServiceProvider serviceProvider)
    {
        await SeedRolesAsync(context, initialData);
        //await SeedAdminAccountAsync(context, configuration, serviceProvider);
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

    //private static async Task SeedAdminAccountAsync(ApplicationDbContext context, IConfiguration configuration, IServiceProvider serviceProvider)
    //{
    //    // Mapping property admin and user from appsetting
    //    var adminSetting = new AdminSetting();
    //    var userSetting = new UserSetting();
    //    configuration.GetSection(AdminSetting.SectionName).Bind(adminSetting);
    //    configuration.GetSection(UserSetting.SectionName).Bind(userSetting);
    //    // Find role Admin
    //    var role = await context.Roles.FirstOrDefaultAsync(r => r.Name == RoleEnum.Admin.ToString());
    //    if (!await context.Users.AnyAsync())
    //    {
    //        var adminId = Guid.NewGuid();
    //        // Get service password from DI and hash password
    //        var passwordHashService = serviceProvider.GetRequiredService<Contract.Infrastructure.Services.IPasswordHashService>();

    //        var newPasswordHashed = passwordHashService.HashPassword(adminSetting.Password);

    //        // Create admin
    //        var admin = User.Create(adminId, adminSetting.Email, newPasswordHashed, adminSetting.FullName, role.Id, userSetting.Avatar.AvatarId, userSetting.Avatar.AvatarUrl);

    //        // And and save in DB
    //        context.Users.Add(admin);
    //        await context.SaveChangesAsync();
    //    }
    //}
}
