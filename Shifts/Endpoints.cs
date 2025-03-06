using Shifts.Repository;

namespace Shifts;

internal static class Endpoints
{
    public static void MapShiftsApi(
        this IEndpointRouteBuilder routes)
    {
        var group = routes.MapGroup("/shifts");

        group.WithTags("Shifts");
        group.WithOpenApi();

        group.MapGet("/", async (IDataRepository repository) => await repository.GetShifts());
        group.MapGet("/{id:guid}", async (IDataRepository repository, Guid id) => await repository.GetShift(id));
    }
}