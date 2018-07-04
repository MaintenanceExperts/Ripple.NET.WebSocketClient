using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Ripple.WebSocketClient.Json.Converters;

namespace Ripple.WebSocketClient.Model.Ledger.Objects
{
    [JsonConverter(typeof(LedgerObjectConverter))]
    public class BaseRippleLedgerObject
    {
        [JsonConverter(typeof(StringEnumConverter))]
        public LedgerEntryType LedgerEntryType { get; set; }

        [JsonProperty("index")]
        public string Index { get; set; }
    }
}
