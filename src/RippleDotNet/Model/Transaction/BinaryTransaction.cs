using Newtonsoft.Json;
using Ripple.WebSocketClient.Model.Transaction.TransactionTypes;

namespace Ripple.WebSocketClient.Model.Transaction
{
    public class BinaryTransaction
    {
        [JsonProperty("meta")]
        public string Meta { get; set; }

        [JsonProperty("tx_blob")]
        public string TransactionBlob { get; set; }
    }
}
