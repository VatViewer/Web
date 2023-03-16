using Newtonsoft.Json;

namespace VatViewer.Shared.Datafeed;

public class General
{
    [JsonProperty("connected_clients")]
    public int ConnectedClients { get; set; }

    [JsonProperty("unique_users")]
    public int UniqueClients { get; set; }
}
