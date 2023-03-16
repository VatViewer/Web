using Newtonsoft.Json;

namespace VatViewer.Shared.Datafeed;

public class Prefile
{
    [JsonProperty("cid")]
    public int Cid { get; set; }

    [JsonProperty("name")]
    public required string Name { get; set; }

    [JsonProperty("callsign")]
    public required string Callsign { get; set; }

    [JsonProperty("flight_plan")]
    public Feed? FlightPlan { get; set; }

    [JsonProperty("last_updated")]
    public DateTimeOffset LastUpdated { get; set; }
}
