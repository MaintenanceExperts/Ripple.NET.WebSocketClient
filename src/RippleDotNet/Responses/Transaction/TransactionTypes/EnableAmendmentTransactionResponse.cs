using Ripple.WebSocketClient.Model.Transaction.Interfaces;
using Ripple.WebSocketClient.Responses.Transaction.Interfaces;

namespace Ripple.WebSocketClient.Responses.Transaction.TransactionTypes
{
    public class EnableAmendmentTransactionResponse : TransactionResponseCommon, IEnableAmendmentTransaction
    {
        public string Amendment { get; set; }
        public uint LedgerSequence { get; set; }
    }
}
