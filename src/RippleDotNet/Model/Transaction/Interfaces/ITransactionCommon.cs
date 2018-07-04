using System.Collections.Generic;
using Ripple.WebSocketClient.Model.Transaction.TransactionTypes;

namespace Ripple.WebSocketClient.Model.Transaction.Interfaces
{
    public interface ITransactionCommon
    {
        string Account { get; set; }

        string AccountTxnID { get; set; }

        Currency Fee { get; set; }

        TransactionFlags? Flags { get; set; }

        uint? LastLedgerSequence { get; set; }

        List<Memo> Memos { get; set; }

        Meta Meta { get; set; }

        uint? Sequence { get; set; }

        List<Signer> Signers { get; set; }

        string SigningPublicKey { get; set; }

        string TransactionSignature { get; set; }

        TransactionType TransactionType { get; set; }

        string ToJson();
    }
}