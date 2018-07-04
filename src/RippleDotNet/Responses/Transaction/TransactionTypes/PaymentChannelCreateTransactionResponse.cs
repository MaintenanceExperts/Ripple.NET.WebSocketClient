using System;
using Newtonsoft.Json;
using Ripple.WebSocketClient.Json.Converters;
using Ripple.WebSocketClient.Model.Transaction.Interfaces;

namespace Ripple.WebSocketClient.Responses.Transaction.TransactionTypes
{
    public class PaymentChannelCreateTransactionResponse : TransactionResponseCommon, IPaymentChannelCreateTransaction
    {
        public string Amount { get; set; }

        public string Destination { get; set; }

        public uint SettleDelay { get; set; }

        public string PublicKey { get; set; }

        [JsonConverter(typeof(RippleDateTimeConverter))]
        public DateTime? CancelAfter { get; set; }

        public uint? DestinationTag { get; set; }

        public uint? SourceTag { get; set; }
    }
}
