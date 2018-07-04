using Ripple.WebSocketClient.Model.Transaction.Interfaces;
using Ripple.WebSocketClient.Responses.Transaction.Interfaces;

namespace Ripple.WebSocketClient.Responses.Transaction.TransactionTypes
{
    public class SetFeeTransactionResponse : TransactionResponseCommon, ISetFeeTransaction
    {
        public string BaseFee { get; set; }
        public uint LedgerSequence { get; set; }
        public uint ReferenceFeeUnits { get; set; }
        public uint ReserveBase { get; set; }
        public uint ReserveIncrement { get; set; }
    }
}
