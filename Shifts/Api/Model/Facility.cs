using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Shifts.Api.Model;

public class Facility
{
    public Guid Id { get; set; } = Guid.NewGuid();
    [MaxLength(8)]
    public string Alias { get; set; }
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public FacilityType Type { get; set; }
}