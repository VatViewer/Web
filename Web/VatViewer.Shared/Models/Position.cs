using Microsoft.EntityFrameworkCore;

namespace VatViewer.Shared.Models;

[Index(nameof(Latitude))]
[Index(nameof(Longitude))]
public class Position
{
    public int Id { get; set; }
    public required Pilot Pilot { get; set; }
    public double Latitude { get; set; }
    public double Longitude { get; set; }
    public int Altitude { get; set; }
    public int GroundSpeed { get; set; }
    public int Heading { get; set; }
    public DateTimeOffset Timestamp { get; set; } = DateTimeOffset.UtcNow;
}
