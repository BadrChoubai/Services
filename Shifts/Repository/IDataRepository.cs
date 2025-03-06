using Shifts.Model;

namespace Shifts.Repository;

public interface IDataRepository
{
    Task<List<Shift>> GetShifts();
    Task<Shift> GetShift(Guid shiftId);
}