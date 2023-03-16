using Microsoft.EntityFrameworkCore;
using VatViewer.Shared.Datafeed;

namespace VatViewer.Shared.Models;

[Index(nameof(Cid))]
[Index(nameof(Callsign))]
[Index(nameof(LogonTime))]
public class Atis
{
    public int Id { get; set; }
    public int Cid { get; set; }
    public required string Name { get; set; }
    public required string Callsign { get; set; }
    public required string Frequency { get; set; }
    public Facility Facility { get; set; }
    public Rating Rating { get; set; }
    public required string TextAtisRaw { get; set; }
    public IList<string> TextAtis => TextAtisRaw.Split(",");
    public DateTimeOffset LogonTime { get; set; }
    public DateTimeOffset? LogoffTime { get; set; }
}
