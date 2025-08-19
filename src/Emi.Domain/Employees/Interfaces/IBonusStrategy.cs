namespace Emi.Domain.Employees.Interfaces;

public interface IBonusStrategy
{
    decimal Calculate(decimal salary);
}
