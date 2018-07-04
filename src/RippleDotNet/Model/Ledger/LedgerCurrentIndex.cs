
using Newtonsoft.Json;

namespace Ripple.WebSocketClient.Model.Ledger
{
    public class LedgerCurrentIndex
    {
        [JsonProperty("ledger_current_index")]
        public uint CurrentIndex { get; set; }
    }
}
