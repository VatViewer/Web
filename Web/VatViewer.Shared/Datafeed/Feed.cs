using Newtonsoft.Json;

namespace VatViewer.Shared.Datafeed;

public class Feed
{
    [JsonProperty("general")]
    public General? General { get; set; }

    [JsonProperty("pilots")]
    public IList<Pilot>? Pilots { get; set; }

    [JsonProperty("controllers")]
    public IList<Controller>? Controllers { get; set; }

    [JsonProperty("atis")]
    public IList<Atis>? Atis { get; set; }

    [JsonProperty("prefiles")]
    public IList<Prefile>? Prefiles { get; set; }
}
