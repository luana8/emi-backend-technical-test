using System.Reflection;
using Emi.Application.Abstractions.Persistence;
using Emi.Domain.Employees.Entities;
using Emi.Domain.Org.Entities;
using Emi.Domain.Projects.Entities;
using Microsoft.EntityFrameworkCore;

namespace Emi.Infrastructure.Data;

public class EmiDbContext : DbContext, IAppDbContext
{
    public EmiDbContext(DbContextOptions<EmiDbContext> options) : base(options) { }

    public DbSet<Employee> Employees => Set<Employee>();
    public DbSet<PositionHistory> PositionHistories => Set<PositionHistory>();
    public DbSet<Department> Departments => Set<Department>();
    public DbSet<Project> Projects => Set<Project>();
    public DbSet<EmployeeProject> EmployeeProjects => Set<EmployeeProject>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // Aplica todas las IEntityTypeConfiguration<T> en esta asamblea
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        base.OnModelCreating(modelBuilder);
    }
}
