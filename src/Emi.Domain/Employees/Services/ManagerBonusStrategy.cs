using Emi.Domain.Employees.Interfaces;

namespace Emi.Domain.Employees.Services;

public sealed class ManagerBonusStrategy : IBonusStrategy
{
    public decimal Calculate(decimal salary) => Math.Round(salary * 0.20m, 2);
}
