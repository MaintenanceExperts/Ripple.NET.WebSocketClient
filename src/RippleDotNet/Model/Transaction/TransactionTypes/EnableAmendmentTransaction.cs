using Ripple.WebSocketClient.Model.Transaction.Interfaces;

namespace Ripple.WebSocketClient.Model.Transaction.TransactionTypes
{
    public class EnableAmendmentTransaction : TransactionCommon, IEnableAmendmentTransaction
    {
        public EnableAmendmentTransaction()
        {
            TransactionType = TransactionType.EnableAmendment;
        }

        public string Amendment { get; set; }

        public uint LedgerSequence { get; set; }
    }
}
