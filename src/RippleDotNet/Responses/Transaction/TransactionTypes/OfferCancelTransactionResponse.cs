using Ripple.WebSocketClient.Model.Transaction.Interfaces;
using Ripple.WebSocketClient.Responses.Transaction.Interfaces;

namespace Ripple.WebSocketClient.Responses.Transaction.TransactionTypes
{
    public class OfferCancelTransactionResponse : TransactionResponseCommon, IOfferCancelTransaction
    {
        public uint OfferSequence { get; set; }
    }
}
