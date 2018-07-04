using System.Collections.Generic;
using Ripple.WebSocketClient.Model.Ledger;
using Ripple.WebSocketClient.Model.Ledger.Objects;
using Ripple.WebSocketClient.Model.Transaction.Interfaces;

namespace Ripple.WebSocketClient.Model.Transaction.TransactionTypes
{
    public class SignerListSetTransaction : TransactionCommon, ISignerListSetTransaction
    {

        public SignerListSetTransaction()
        {
            TransactionType = TransactionType.SignerListSet;
        }

        public uint SignerQuorum { get; set; }

        public List<SignerEntry> SignerEntries { get; set; }
    }
}
