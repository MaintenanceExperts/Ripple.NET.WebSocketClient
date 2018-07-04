using System.Collections.Generic;
using Ripple.WebSocketClient.Model.Ledger.Objects;

namespace Ripple.WebSocketClient.Model.Transaction.Interfaces
{
    public interface ISignerListSetTransaction : ITransactionCommon
    {
        List<SignerEntry> SignerEntries { get; set; }
        uint SignerQuorum { get; set; }
    }
}