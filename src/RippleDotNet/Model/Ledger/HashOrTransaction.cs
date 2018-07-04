using Newtonsoft.Json;
using Ripple.WebSocketClient.Json.Converters;
using Ripple.WebSocketClient.Model.Transaction.TransactionTypes;

namespace Ripple.WebSocketClient.Model.Ledger
{
    [JsonConverter(typeof(TransactionOrHashConverter))]
    public class HashOrTransaction
    {
        public string TransactionHash { get; set; }

        public TransactionCommon Transaction { get; set; }
    }
}
