using Shifts.Api.Model;

namespace Shifts.Api.Service;

public interface IApiService
{
    Task<List<Shift>> GetAll();
    Task<Shift> GetShift(Guid id);
}