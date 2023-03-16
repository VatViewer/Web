using Newtonsoft.Json;

namespace VatViewer.Shared.Datafeed;

public class Controller
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
    public IList<string>? ControllerInfoRaw { get; set; }
    public string ControllerInfo => string.Join(",", ControllerInfoRaw ?? new List<string>());

    [JsonProperty("logon_time")]
    public DateTimeOffset LogonTime { get; set; }
}

public enum Facility
{
    OBS,
    FSS,
    DEL,
    GND,
    TWR,
    APP,
    CTR
}

public enum Rating
{
    INAC = -1,
    SUS,
    OBS,
    S1,
    S2,
    S3,
    C1,
    C2,
    C3,
    I1,
    I2,
    I3,
    SUP,
    ADM
}
