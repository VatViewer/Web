using Newtonsoft.Json;

namespace VatViewer.Shared.Datafeed;

public class Atis
{
    [JsonProperty("cid")]
    public int Cid { get; set; }

    [JsonProperty("name")]
    public required string Name { get; set; }

    [JsonProperty("callsign")]
    public required string Callsign { get; set; }

    [JsonProperty("frequency")]
    public required string Frequency { get; set; }

    [JsonProperty("facility")]
    public Facility Facility { get; set; }

    [JsonProperty("rating")]
    public Rating Rating { get; set; }

    [JsonProperty("text_atis")]
    public IList<string>? TextAtisRaw { get; set; }
    public string TextAtis => string.Join(",", TextAtisRaw ?? new List<string>());

    [JsonProperty("logon_time")]
    public DateTimeOffset LogonTime { get; set; }
}
