using Newtonsoft.Json;
using Ripple.WebSocketClient.Json.Converters;
using Ripple.WebSocketClient.Model;
using Ripple.WebSocketClient.Model.Transaction.Interfaces;
using Ripple.WebSocketClient.Responses.Transaction.Interfaces;

namespace Ripple.WebSocketClient.Responses.Transaction.TransactionTypes
{
    public class TrustSetTransactionResponse : TransactionResponseCommon, ITrustSetTransaction
    {
        public new TrustSetFlags? Flags { get; set; }

        [JsonConverter(typeof(CurrencyConverter))]
        public Currency LimitAmount { get; set; }
        public uint? QualityIn { get; set; }
        public uint? QualityOut { get; set; }
    }
}
