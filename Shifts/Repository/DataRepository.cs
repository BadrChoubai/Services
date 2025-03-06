using Microsoft.EntityFrameworkCore;

using Shifts.Model;


namespace Shifts.Repository
{
    public class DataRepository(ShiftsDbContext dbContext) : IDataRepository
    {
        public async Task<List<Shift>> GetShifts()
        {
            var result = await dbContext.Shifts
                .Include(s => s.Location)
                .ThenInclude(l => l.Facility)
                .ToListAsync();

            return result;
        }

        public async Task<Shift> GetShift(Guid id)
        {
            var result = await dbContext.Shifts
                .Include(s => s.Location)
                .ThenInclude(l => l.Facility)
                .FirstOrDefaultAsync(s => s.Id == id);


            return result;

        }
    }
}