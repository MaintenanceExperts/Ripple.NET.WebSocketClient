using System;
using Newtonsoft.Json;
using Ripple.WebSocketClient.Json.Converters;
using Ripple.WebSocketClient.Model;

namespace Ripple.WebSocketClient.Requests
{
    public class BaseLedgerRequest : RippleRequest
    {
        public BaseLedgerRequest() { }

        public BaseLedgerRequest(Guid requestId) : base(requestId){ }

        [JsonProperty("ledger_hash")]
        public string LedgerHash { get; set; }

        [JsonProperty("ledger_index")]
        [JsonConverter(typeof(LedgerIndexConverter))]
        public LedgerIndex LedgerIndex { get; set; }
    }
}
