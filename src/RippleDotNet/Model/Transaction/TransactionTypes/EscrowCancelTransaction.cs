
using Ripple.WebSocketClient.Model.Transaction.Interfaces;

namespace Ripple.WebSocketClient.Model.Transaction.TransactionTypes
{
    public class EscrowCancelTransaction : TransactionCommon, IEscrowCancelTransaction
    {
        public EscrowCancelTransaction()
        {
            TransactionType = TransactionType.EscrowCancel;
        }

        public string Owner { get; set; }

        public uint OfferSequence { get; set; }
    }
}
