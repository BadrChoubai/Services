using Shifts.Api.Model;

namespace Shifts.Api.Repository;

public interface IDataRepository
{
    Task<List<Shift>> GetShifts();
    Task<Shift> GetShift(Guid shiftId);
}