using Ripple.WebSocketClient.Model.Transaction.Interfaces;

namespace Ripple.WebSocketClient.Model.Transaction.TransactionTypes
{
    public class SetRegularKeyTransaction : TransactionCommon, ISetRegularKeyTransaction
    {
        public SetRegularKeyTransaction()
        {
            TransactionType = TransactionType.SetRegularKey;
        }


        public string RegularKey { get; set; }
    }
}
