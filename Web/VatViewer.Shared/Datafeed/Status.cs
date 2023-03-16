using Newtonsoft.Json;

namespace VatViewer.Shared.Datafeed;

public class Status
{
    [JsonProperty("Data")]
    public Data? Data { get; set; }
}

public class Data
{
    [JsonProperty("v3")]
    public IList<string>? V3 { get; set; }
}

