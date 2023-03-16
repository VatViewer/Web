using Microsoft.EntityFrameworkCore;

namespace VatViewer.Shared.Models;

[Index(nameof(Cid))]
[Index(nameof(Aircraft))]
[Index(nameof(Departure))]
[Index(nameof(Arrival))]
[Index(nameof(Timestamp))]
public class FlightPlan
{
    public int Id { get; set; }
    public int Cid { get; set; }
    public required string FlightRules { get; set; }
    public required string Aircraft { get; set; }
    public required string Departure { get; set; }
    public required string Arrival { get; set; }
    public required string Route { get; set; }
    public DateTimeOffset Timestamp { get; set; } = DateTimeOffset.UtcNow;
}
