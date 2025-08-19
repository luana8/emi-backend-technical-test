using Emi.Domain.Org.Entities;
using Emi.Domain.Projects.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Emi.Infrastructure.Data.Seeds;

public static class SeedData
{
    public static async Task EnsureSeededAsync(IServiceProvider sp)
    {
        using var scope = sp.CreateScope();
        var db = scope.ServiceProvider.GetRequiredService<EmiDbContext>();
        await db.Database.MigrateAsync();

        if (!await db.Departments.AnyAsync())
        {
            db.Departments.AddRange(
                new Department("Engineering"),
                new Department("Operations")
            );
        }

        if (!await db.Projects.AnyAsync())
        {
            db.Projects.AddRange(
                new Project("Falck Core"),
                new Project("Falck Mobile")
            );
        }

        await db.SaveChangesAsync();
    }
}