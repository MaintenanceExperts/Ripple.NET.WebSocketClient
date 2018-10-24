using Microsoft.VisualStudio.TestTools.UnitTesting;
using Ripple.WebSocketClient.Model.Subscription;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Ripple.WebSocketClient.Tests
{
    [TestClass]
    public class SubscriptionTests
    {
        private static string serverUrl = "wss://s.altnet.rippletest.net:51233";
        private IRippleClient client;

        public SubscriptionTests()
        {
            this.client = new RippleClient(serverUrl);
            this.client.Connect().Wait();
        }

        [TestMethod]
        public async Task SubscribeToLedgerStream()
        {
            SubscriptionStream stream = await this.client.Subscribe(new Requests.Subscription.SubscriptionRequest { Streams = new List<string>() { "ledger" } });

            int i = 0;

            while (true)
            {
                if (i >= 3)
                {
                    await this.client.Unsubscribe();
                    break;
                }

                IDictionary<string, object> nextItem = await stream.Next();
                string json = nextItem.ToString();

                i++;
            }

            await this.client.Ping();
        }

    }
}
