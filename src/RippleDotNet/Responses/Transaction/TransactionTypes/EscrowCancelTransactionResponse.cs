using Ripple.WebSocketClient.Model.Transaction.Interfaces;
using Ripple.WebSocketClient.Responses.Transaction.Interfaces;

namespace Ripple.WebSocketClient.Responses.Transaction.TransactionTypes
{
    public class EscrowCancelTransactionResponse : TransactionResponseCommon, IEscrowCancelTransaction
    {
        public uint OfferSequence { get; set; }
        public string Owner { get; set; }
    }
}
