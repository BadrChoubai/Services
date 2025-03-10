using Shifts.Api.Model;
using Shifts.Api.Repository;

namespace Shifts.Api.Service;

public class ShiftsService : IApiService
{
    private readonly IDataRepository _repository;
    private readonly ILogger<ShiftsService> _logger;

    public ShiftsService(IDataRepository repository, ILogger<ShiftsService> logger)
    {
        _repository = repository;
        _logger = logger;
    }

    public async Task<List<Shift>> GetAll()
    {
        _logger.LogInformation("Getting all shifts");
        return await _repository.GetShifts();
    }

    public async Task<Shift> GetShift(Guid id)
    {
        _logger.LogInformation("Getting a shift");
        return await _repository.GetShift(id);
    }
}