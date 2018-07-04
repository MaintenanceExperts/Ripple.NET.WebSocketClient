using Newtonsoft.Json;
using Ripple.WebSocketClient.Json.Converters;
using Ripple.WebSocketClient.Model.Transaction.Interfaces;

namespace Ripple.WebSocketClient.Model.Transaction.TransactionTypes
{
    public class TrustSetTransaction : TransactionCommon, ITrustSetTransaction
    {
        public TrustSetTransaction()
        {
            TransactionType = TransactionType.TrustSet;
            Flags = TrustSetFlags.tfFullyCanonicalSig | TrustSetFlags.tfSetNoRipple;
        }

        public new TrustSetFlags? Flags { get; set; }

        [JsonConverter(typeof(CurrencyConverter))]
        public Currency LimitAmount {get; set; }

        public uint? QualityIn { get; set; }

        public uint? QualityOut { get; set; }
    }
}
