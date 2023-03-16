using Microsoft.EntityFrameworkCore;

namespace VatViewer.Shared.Models;

[Index(nameof(Cid))]
public class Prefile
{
    public int Id { get; set; }
    public int Cid { get; set; }
    public required string Name { get; set; }
    public required string Callsign { get; set; }
    public FlightPlan? FlightPlan { get; set; }
    public DateTimeOffset Timestamp { get; set; }
}
