namespace Ripple.WebSocketClient.Model.Transaction.Interfaces
{
    public interface IOfferCancelTransaction : ITransactionCommon
    {
        uint OfferSequence { get; set; }
    }
}