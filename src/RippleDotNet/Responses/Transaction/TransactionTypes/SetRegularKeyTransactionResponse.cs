using Ripple.WebSocketClient.Model.Transaction.Interfaces;
using Ripple.WebSocketClient.Responses.Transaction.Interfaces;

namespace Ripple.WebSocketClient.Responses.Transaction.TransactionTypes
{
    public class SetRegularKeyTransactionResponse : TransactionResponseCommon, ISetRegularKeyTransaction
    {
        public string RegularKey { get; set; }
    }
}
