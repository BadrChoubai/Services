using Shifts.Model;

namespace Shifts.Service;

public interface IApiService
{
    Task<List<Shift>> GetAll();
    Task<Shift> GetShift(Guid id);
}