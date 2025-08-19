using Emi.Domain.Common.Primitives;

namespace Emi.Domain.Projects.Entities;

public sealed class Project : Entity<int>
{
    public string Name { get; private set; } = default!;
    public List<EmployeeProject> EmployeeProjects { get; } = new();

    private Project() { }
    public Project(string name) => Name = name;
}

public sealed class EmployeeProject
{
    public int EmployeeId { get; private set; }
    public int ProjectId  { get; private set; }

    // navegación se asignará desde Infra/EF; aquí mantenemos dominio limpio.
    private EmployeeProject() { }
    public EmployeeProject(int employeeId, int projectId)
        => (EmployeeId, ProjectId) = (employeeId, projectId);
}
