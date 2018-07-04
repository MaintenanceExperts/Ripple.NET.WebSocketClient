using System.Collections.Generic;
using Ripple.WebSocketClient.Model.Ledger.Objects;
using Ripple.WebSocketClient.Model.Transaction.Interfaces;
using Ripple.WebSocketClient.Responses.Transaction.Interfaces;

namespace Ripple.WebSocketClient.Responses.Transaction.TransactionTypes
{
    public class SignerListSetTransactionResponse : TransactionResponseCommon, ISignerListSetTransaction
    {
        public List<SignerEntry> SignerEntries { get; set; }
        public uint SignerQuorum { get; set; }
    }
}
