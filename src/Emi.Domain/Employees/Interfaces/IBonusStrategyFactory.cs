using Emi.Domain.Employees.Enums;

namespace Emi.Domain.Employees.Interfaces;

public interface IBonusStrategyFactory
{
    IBonusStrategy Resolve(PositionKind position);
}
