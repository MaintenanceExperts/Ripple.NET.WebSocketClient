using Ripple.WebSocketClient.Model.Transaction.Interfaces;
using Ripple.WebSocketClient.Responses.Transaction.Interfaces;

namespace Ripple.WebSocketClient.Responses.Transaction.TransactionTypes
{
    public class EscrowFinishTransactionResponse : TransactionResponseCommon, IEscrowFinishTransaction
    {
        public string Condition { get; set; }
        public string Fulfillment { get; set; }
        public uint OfferSequence { get; set; }
        public string Owner { get; set; }
    }
}
