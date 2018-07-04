using Ripple.WebSocketClient.Model.Transaction.Interfaces;

namespace Ripple.WebSocketClient.Model.Transaction.TransactionTypes
{
    public class OfferCancelTransaction : TransactionCommon, IOfferCancelTransaction
    {
        public OfferCancelTransaction()
        {
            TransactionType = TransactionType.OfferCancel;
        }

        public uint OfferSequence { get; set; }
    }
}
