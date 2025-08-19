using Emi.Domain.Employees.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Emi.Infrastructure.Data.Configurations;

public class PositionHistoryConfiguration : IEntityTypeConfiguration<PositionHistory>
{
    public void Configure(EntityTypeBuilder<PositionHistory> b)
    {
        b.ToTable("PositionHistory");
        b.HasKey(ph => ph.Id);
        b.Property(ph => ph.Position).HasMaxLength(120).IsRequired();
        b.Property(ph => ph.StartDate).IsRequired();
        // EmployeeId ya está definido como FK desde EmployeeConfiguration
    }
}
