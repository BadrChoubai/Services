namespace Shifts.Api.Model;

public class Shift
{
    public Guid Id { init; get; } = Guid.NewGuid();
    public TimeSpan Duration { get; init; }
    public Location Location { get; init; }
}