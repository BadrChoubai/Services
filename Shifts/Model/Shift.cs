using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Shifts.Model;

public enum FacilityType
{
    SortCenter,
    DistributionCenter,
}

public class Facility
{
    public Guid Id { get; set; } = Guid.NewGuid();
    [MaxLength(8)]
    public string Alias { get; set; }
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public FacilityType Type { get; set; }
}

public class Location
{
    public Guid Id { get; set; } = Guid.NewGuid();

    [MaxLength(100)]
    public string Address { get; set; }

    [MaxLength(40)]
    public string City { get; set; }
    [MaxLength(2)]
    public string StateCode { get; set; }

    [MaxLength(10)]
    public string PostalCode { get; set; }
    [MaxLength(4)]
    public string CountryCode { get; set; }
    public Facility Facility { get; set; }
}

public class Shift
{
    public Guid Id { init; get; } = Guid.NewGuid();
    public TimeSpan Duration { get; init; }
    public Location Location { get; init; }
}