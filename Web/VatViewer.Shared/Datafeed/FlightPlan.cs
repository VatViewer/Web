using Newtonsoft.Json;

namespace VatViewer.Shared.Datafeed;

public class FlightPlan
{
    [JsonProperty("flight_rules")]
    public string? FlightRules { get; set; }

    [JsonProperty("aircraft_short")]
    public string? Aircraft { get; set; }

    [JsonProperty("departure")]
    public string? Departure { get; set; }

    [JsonProperty("arrival")]
    public string? Arrival { get; set; }

    [JsonProperty("route")]
    public string? Route { get; set; }
}
