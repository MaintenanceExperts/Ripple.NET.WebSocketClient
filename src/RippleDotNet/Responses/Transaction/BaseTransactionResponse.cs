using System;
using Newtonsoft.Json;
using Ripple.WebSocketClient.Json.Converters;
using Ripple.WebSocketClient.Responses.Transaction.Interfaces;

namespace Ripple.WebSocketClient.Responses.Transaction
{
    public class BaseTransactionResponse : IBaseTransactionResponse
    {
        [JsonConverter(typeof(RippleDateTimeConverter))]
        [JsonProperty("date")]
        public DateTime? Date { get; set; }

        [JsonProperty("hash")]
        public string Hash { get; set; }

        [JsonProperty("inLedger")]
        public uint? InLedger { get; set; }

        [JsonProperty("ledger_index")]
        public uint? LedgerIndex { get; set; }

        [JsonProperty("validated")]
        public bool? Validated { get; set; }
    }
}
