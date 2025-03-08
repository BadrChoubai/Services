using Shifts.Extensions;
using Shifts.Service;

namespace Shifts;

internal static class Endpoints
{
    public static void MapShiftsApi(
        this IEndpointRouteBuilder routes)
    {
        var group = routes.MapGroup("/shifts");
        group.RequirePerUserRateLimit();

        group.WithTags("Shifts");
        group.WithOpenApi();

        group.MapGet("/", async (IApiService service) => await service.GetAll());
        group.MapGet("/{id:guid}", async (IApiService service, Guid id) => await service.GetShift(id));
    }
}