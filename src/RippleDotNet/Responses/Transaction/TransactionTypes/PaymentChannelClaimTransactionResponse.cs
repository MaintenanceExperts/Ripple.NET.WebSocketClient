using Ripple.WebSocketClient.Model;
using Ripple.WebSocketClient.Model.Transaction.Interfaces;
using Ripple.WebSocketClient.Responses.Transaction.Interfaces;

namespace Ripple.WebSocketClient.Responses.Transaction.TransactionTypes
{
    public class PaymentChannelClaimTransactionResponse : TransactionResponseCommon, IPaymentChannelClaimTransaction
    {
        public string Amount { get; set; }
        public string Balance { get; set; }
        public string Channel { get; set; }
        public PaymentChannelClaimFlags? Flags { get; set; }
        public string PublicKey { get; set; }
        public string Signature { get; set; }
    }
}
