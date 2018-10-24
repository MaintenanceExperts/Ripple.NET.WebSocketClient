using Newtonsoft.Json;
using Ripple.WebSocketClient.Requests.Transaction;
using System;
using System.Collections.Generic;
using System.Text;

namespace Ripple.WebSocketClient.Requests.Subscription
{
    public class SubscriptionRequest : RippleRequest
    {
        [JsonProperty("streams")]
        public List<string> Streams { get; set; }

        [JsonProperty("accounts")]
        public List<string> Accounts { get; set; }

        [JsonProperty("accounts_proposed")]
        public List<string> AccountsProposed { get; set; }

        [JsonProperty("books")]
        public List<BookOffersRequest> Books { get; set; }

        public SubscriptionRequest()
        {
            this.Command = "subscribe";
        }
    }
}
