using Microsoft.EntityFrameworkCore;

namespace VatViewer.Shared.Models;

[Index(nameof(Cid))]
[Index(nameof(Callsign))]
[Index(nameof(LogonTime))]
[Index(nameof(LogoffTime))]
public class Pilot
{
    public int Id { get; set; }
    public int Cid { get; set; }
    public required string Name { get; set; }
    public required string Callsign { get; set; }
    public required FlightPlan FlightPlan { get; set; }
    public DateTimeOffset LogonTime { get; set; }
    public DateTimeOffset? LogoffTime { get; set; }
    public TimeSpan? Length { get; set; }
}
