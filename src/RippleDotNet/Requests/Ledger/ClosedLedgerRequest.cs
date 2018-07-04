namespace Ripple.WebSocketClient.Requests.Ledger
{
    public class ClosedLedgerRequest : RippleRequest
    {
        public ClosedLedgerRequest()
        {
            Command = "ledger_closed";
        }
    }
}
