using Emi.Domain.Projects.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Emi.Infrastructure.Data.Configurations;

public class EmployeeProjectConfiguration : IEntityTypeConfiguration<EmployeeProject>
{
    public void Configure(EntityTypeBuilder<EmployeeProject> b)
    {
        b.ToTable("EmployeeProjects");
        b.HasKey(ep => new { ep.EmployeeId, ep.ProjectId });

        b.HasOne<Project>()
         .WithMany(p => p.EmployeeProjects)
         .HasForeignKey(ep => ep.ProjectId)
         .OnDelete(DeleteBehavior.Cascade);
    }
}