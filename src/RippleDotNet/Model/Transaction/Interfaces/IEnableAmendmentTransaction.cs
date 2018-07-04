namespace Ripple.WebSocketClient.Model.Transaction.Interfaces
{
    public interface IEnableAmendmentTransaction : ITransactionCommon
    {
        string Amendment { get; set; }
        uint LedgerSequence { get; set; }
    }
}