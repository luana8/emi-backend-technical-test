using Emi.Domain.Employees.Enums;
using Emi.Domain.Employees.Interfaces;

namespace Emi.Domain.Employees.Services;

public sealed class BonusStrategyFactory : IBonusStrategyFactory
{
    private static readonly IBonusStrategy Regular = new RegularBonusStrategy();
    private static readonly IBonusStrategy Manager = new ManagerBonusStrategy();

    public IBonusStrategy Resolve(PositionKind position)
        => position == PositionKind.Regular ? Regular : Manager;
}
