using Emi.Domain.Employees.Interfaces;

namespace Emi.Domain.Employees.Services;

public sealed class RegularBonusStrategy : IBonusStrategy
{
    public decimal Calculate(decimal salary) => Math.Round(salary * 0.10m, 2);
}
