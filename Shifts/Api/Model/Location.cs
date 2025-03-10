using System.ComponentModel.DataAnnotations;

namespace Shifts.Api.Model;

public class Location
{
    public Guid Id { get; set; } = Guid.NewGuid();

    [MaxLength(100)] public string Address { get; set; }

    [MaxLength(40)] public string City { get; set; }
    [MaxLength(2)] public string StateCode { get; set; }

    [MaxLength(10)] public string PostalCode { get; set; }
    [MaxLength(4)] public string CountryCode { get; set; }
    public Facility Facility { get; set; }
}