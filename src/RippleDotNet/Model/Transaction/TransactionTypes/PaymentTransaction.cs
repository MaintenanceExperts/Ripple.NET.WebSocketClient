using System.Collections.Generic;
using Newtonsoft.Json;
using Ripple.WebSocketClient.Json.Converters;
using Ripple.WebSocketClient.Model.Transaction.Interfaces;

namespace Ripple.WebSocketClient.Model.Transaction.TransactionTypes
{
    public class PaymentTransaction : TransactionCommon, IPaymentTransaction
    {
        public PaymentTransaction()
        {
            TransactionType = TransactionType.Payment;
            Flags = PaymentFlags.tfFullyCanonicalSig;
        }

        [JsonConverter(typeof(CurrencyConverter))]
        public Currency Amount { get; set; }

        public string Destination { get; set; }

        public uint? DestinationTag { get; set; }

        public new PaymentFlags? Flags { get; set; }

        public string InvoiceId { get; set; }

        public List<List<Path>> Paths { get; set; }

        [JsonConverter(typeof(CurrencyConverter))]
        public Currency SendMax { get; set; }

        [JsonConverter(typeof(CurrencyConverter))]
        public Currency DeliverMin { get; set; }
    }
}
