using Ripple.WebSocketClient.Model.Transaction.Interfaces;
using Ripple.WebSocketClient.Responses.Transaction.Interfaces;

namespace Ripple.WebSocketClient.Responses.Transaction.TransactionTypes
{
    public class AccountSetTransactionResponse : TransactionResponseCommon, IAccountSetTransaction
    {
        public uint? ClearFlag { get; set; }
        public string Domain { get; set; }
        public string EmailHash { get; set; }
        public string MessageKey { get; set; }
        public uint? SetFlag { get; set; }
        public uint? TransferRate { get; set; }
        public uint? TickSize { get; set; }        
    }
}
