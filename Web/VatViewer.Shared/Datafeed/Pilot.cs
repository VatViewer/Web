using Newtonsoft.Json;

namespace VatViewer.Shared.Datafeed;

public class Pilot
{
    [JsonProperty("cid")]
    public int Cid { get; set; }

    [JsonProperty("name")]
    public required string Name { get; set; }

    [JsonProperty("callsign")]
    public required string Callsign { get; set; }

    [JsonProperty("latitude")]
    public double Latitude { get; set; }

    [JsonProperty("longitude")]
    public double Longitude { get; set; }

    [JsonProperty("altitude")]
    public int Altitude { get; set; }

    [JsonProperty("groundspeed")]
    public int GroundSpeed { get; set; }

    [JsonProperty("heading")]
    public int Heading { get; set; }

    [JsonProperty("flight_plan")]
    public FlightPlan? FlightPlan { get; set; }

    [JsonProperty("logon_time")]
    public DateTimeOffset LogonTime { get; set; }
}
