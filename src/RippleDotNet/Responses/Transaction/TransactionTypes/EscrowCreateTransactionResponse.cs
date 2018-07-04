using System;
using Newtonsoft.Json;
using Ripple.WebSocketClient.Json.Converters;
using Ripple.WebSocketClient.Model;
using Ripple.WebSocketClient.Model.Transaction.Interfaces;


namespace Ripple.WebSocketClient.Responses.Transaction.TransactionTypes
{
    public class EscrowCreateTransactionResponse : TransactionResponseCommon, IEscrowCreateTransaction
    {
        [JsonConverter(typeof(CurrencyConverter))]
        public Currency Amount { get; set; }

        public string Destination { get; set; }

        [JsonConverter(typeof(RippleDateTimeConverter))]
        public DateTime? CancelAfter { get; set; }

        [JsonConverter(typeof(RippleDateTimeConverter))]
        public DateTime? FinishAfter { get; set; }

        public string Condition { get; set; }

        public uint? DestinationTag { get; set; }

        public uint? SourceTag { get; set; }
    }
}
