using Microsoft.VisualStudio.TestTools.UnitTesting;
using Ripple.WebSocketClient.Model.Admin;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Ripple.WebSocketClient.Tests
{
    [TestClass]
    public class AdminTests
    {
        private static IRippleClient client;

        //this is an altnet account
        private static string account = "rwEHFU98CjH59UX2VqAgeCzRFU9KVvV71V";

        //these are mainnet accounts
        //private static string account = "rPGKpTsgSaQiwLpEekVj1t5sgYJiqf2HDC";
        //private static string account = "rho3u4kXc5q3chQFKfn9S1ZqUCya1xT3t4";

        //private static string serverUrl = "ws://localhost:1337";
        private static string serverUrl = "wss://s1.ripple.com:443";
        //private static string serverUrl = "wss://s2.ripple.com:443";


        [ClassInitialize]
        public static void MyClassInitialize(TestContext testContext)
        {
            client = new RippleClient(serverUrl);
            client.Connect().Wait();
        }

        [TestMethod]
        public async Task CanWalletPropse()
        {
            WalletPropose response = await client.WalletPropose(new Requests.Admin.WalletProposeRequest());

            Assert.IsTrue(response.MasterSeed.StartsWith("s"));
            Assert.IsTrue(response.AccountId.StartsWith("r"));
        }
    }
}
