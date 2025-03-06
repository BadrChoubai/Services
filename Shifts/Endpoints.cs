using Microsoft.EntityFrameworkCore;

using Shifts.Model;

namespace Shifts;

internal static class Endpoints
{
    public static void MapShiftsApi(this IEndpointRouteBuilder routes)
    {
        var group = routes.MapGroup("/shifts");

        group.WithTags("Shifts");
        group.WithOpenApi();

        group.MapGet("/", async (ShiftsDbContext db) =>
        {
            var result = await db.Shifts
                .Include(s => s.Location)
                .ThenInclude(l => l.Facility)
                .ToListAsync();

            return result;
        });

        group.MapGet("/{id:guid}", async (Guid id, ShiftsDbContext db) =>
        {
            var result = await db.Shifts
                .Include(s => s.Location)
                .ThenInclude(l => l.Facility)
                .FirstOrDefaultAsync(s => s.Id == id);


            return result;
        });
    }
}