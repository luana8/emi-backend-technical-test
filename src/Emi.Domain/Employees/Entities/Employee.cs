using Emi.Domain.Common.Primitives;
using Emi.Domain.Employees.Enums;
using Emi.Domain.Employees.Interfaces;

namespace Emi.Domain.Employees.Entities;

public sealed class Employee : Entity<int>
{
    public string Name { get; private set; } = default!;
    public PositionKind CurrentPosition { get; private set; }
    public decimal Salary { get; private set; }
    public List<PositionHistory> History { get; } = new();

    private Employee() { } // EF/serialización
    public Employee(string name, PositionKind position, decimal salary)
        => (Name, CurrentPosition, Salary) = (name, position, salary);

    public void ChangeSalary(decimal newSalary) => Salary = newSalary;

    public decimal CalculateAnnualBonus(IBonusStrategyFactory factory)
        => factory.Resolve(CurrentPosition).Calculate(Salary);
}
