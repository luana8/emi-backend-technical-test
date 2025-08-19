using Microsoft.EntityFrameworkCore;
using Emi.Domain.Employees.Entities;
using Emi.Domain.Org.Entities;
using Emi.Domain.Projects.Entities;

namespace Emi.Application.Abstractions.Persistence;

public interface IAppDbContext
{
    DbSet<Employee> Employees { get; }
    DbSet<PositionHistory> PositionHistories { get; }
    DbSet<Department> Departments { get; }
    DbSet<Project> Projects { get; }
    DbSet<EmployeeProject> EmployeeProjects { get; }

    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}
