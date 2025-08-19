using Emi.Domain.Employees.Entities;
using Emi.Domain.Org.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Emi.Infrastructure.Data.Configurations;

public class EmployeeConfiguration : IEntityTypeConfiguration<Employee>
{
    public void Configure(EntityTypeBuilder<Employee> b)
    {
        b.ToTable("Employees");
        b.HasKey(e => e.Id);

        b.Property(e => e.Name).HasMaxLength(150).IsRequired();
        b.Property(e => e.Salary).HasPrecision(18, 2);

        // Relación con Department usando FK sombra (DepartmentId)
        b.Property<int>("DepartmentId");
        b.HasOne<Department>()
         .WithMany(d => d.Employees)
         .HasForeignKey("DepartmentId")
         .OnDelete(DeleteBehavior.Restrict);

        // 1..N con PositionHistory
        b.HasMany(e => e.History)
         .WithOne()
         .HasForeignKey(ph => ph.EmployeeId)
         .OnDelete(DeleteBehavior.Cascade);
    }
}
