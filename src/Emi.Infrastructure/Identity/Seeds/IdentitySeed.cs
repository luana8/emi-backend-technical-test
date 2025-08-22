using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using Emi.Infrastructure.Identity.Models;
using Emi.Infrastructure.Data;

namespace Emi.Infrastructure.Identity.Seeds;

public static class IdentitySeed
{
    public static async Task EnsureSeededAsync(IServiceProvider sp)
    {
        using var scope = sp.CreateScope();
        var roleMgr = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();
        var userMgr = scope.ServiceProvider.GetRequiredService<UserManager<AppUser>>();
        var db      = scope.ServiceProvider.GetRequiredService<EmiDbContext>();

        await db.Database.MigrateAsync();


        foreach (var role in new[] { "Admin", "User" })
        {
            if (!await roleMgr.RoleExistsAsync(role))
                await roleMgr.CreateAsync(new IdentityRole(role));
        }

        var adminEmail = "admin@emi.local";
        var admin = await userMgr.FindByEmailAsync(adminEmail);
        if (admin is null)
        {
            admin = new AppUser { UserName = adminEmail, Email = adminEmail, EmailConfirmed = true };
            await userMgr.CreateAsync(admin, "Admin123$"); // solo dev
            await userMgr.AddToRoleAsync(admin, "Admin");
        }
    }
}