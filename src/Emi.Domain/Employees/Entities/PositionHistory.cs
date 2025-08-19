using Emi.Domain.Common.Primitives;

namespace Emi.Domain.Employees.Entities;

public sealed class PositionHistory : Entity<int>
{
    public int EmployeeId { get; private set; }
    public string Position { get; private set; } = default!;
    public DateTime StartDate { get; private set; }
    public DateTime? EndDate { get; private set; }

    private PositionHistory() { }
    public PositionHistory(int employeeId, string position, DateTime start, DateTime? end = null)
        => (EmployeeId, Position, StartDate, EndDate) = (employeeId, position, start, end);
}
