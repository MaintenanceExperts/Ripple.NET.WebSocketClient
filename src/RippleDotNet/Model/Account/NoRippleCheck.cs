using System.Collections.Generic;
using Newtonsoft.Json;
using Ripple.WebSocketClient.Model.Transaction;
using Ripple.WebSocketClient.Model.Transaction.TransactionTypes;

namespace Ripple.WebSocketClient.Model.Account
{
    public class NoRippleCheck
    {
        [JsonProperty("ledger_current_index")]
        public uint LedgerCurrentIndex { get; set; }

        [JsonProperty("problems")]
        public List<string> Problems { get; set; }

        [JsonProperty("transactions")]
        public List<TransactionCommon> Transactions { get; set; }

        [JsonProperty("validated")]
        public bool Validated { get; set; }
    }
}
