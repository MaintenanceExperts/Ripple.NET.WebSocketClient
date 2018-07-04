using System;
using Newtonsoft.Json;
using Ripple.WebSocketClient.Json.Converters;
using Ripple.WebSocketClient.Model.Transaction.Interfaces;

namespace Ripple.WebSocketClient.Model.Transaction.TransactionTypes
{
    public class PaymentChannelFundTransaction : TransactionCommon, IPaymentChannelFundTransaction
    {
        public PaymentChannelFundTransaction()
        {
            TransactionType = TransactionType.PaymentChannelFund;
        }

        public string Channel { get; set; }

        public string Amount { get; set; }

        [JsonConverter(typeof(RippleDateTimeConverter))]
        public DateTime? Expiration { get; set; }
    }
}
