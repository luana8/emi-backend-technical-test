using Emi.Domain.Common.Primitives;
using Emi.Domain.Employees.Entities;

namespace Emi.Domain.Org.Entities;

public sealed class Department : Entity<int>
{
    public string Name { get; private set; } = default!;
    public List<Employee> Employees { get; } = new();

    private Department() { }
    public Department(string name) => Name = name;
}
