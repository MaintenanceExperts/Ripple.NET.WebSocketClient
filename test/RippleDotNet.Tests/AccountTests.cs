using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Ripple.Core.Types;
using Ripple.WebSocketClient.Model;
using Ripple.WebSocketClient.Model.Account;
using Ripple.WebSocketClient.Requests.Account;


namespace Ripple.WebSocketClient.Tests
{
    [TestClass]
    public class AccountTests
    {
        private static IRippleClient client;
        
        //this is an altnet account
        private static string account = "rwEHFU98CjH59UX2VqAgeCzRFU9KVvV71V";
        
        //these are mainnet accounts
        //private static string account = "rPGKpTsgSaQiwLpEekVj1t5sgYJiqf2HDC";
        //private static string account = "rho3u4kXc5q3chQFKfn9S1ZqUCya1xT3t4";

        private static string serverUrl = "wss://s.altnet.rippletest.net:51233";
        //private static string serverUrl = "wss://s1.ripple.com:443";
        //private static string serverUrl = "wss://s2.ripple.com:443";


        [ClassInitialize]
        public static void MyClassInitialize(TestContext testContext)
        {
            client = new RippleClient(serverUrl);
            client.Connect();            
        }

        [TestMethod]
        public async Task CanPerformPing()
        {
            await client.Ping();            
        }

        [TestMethod]
        public async Task CanGetAccountInfo()
        {
            AccountInfo accountInfo = await client.AccountInfo(account);
            Assert.IsNotNull(accountInfo);
        }

        [TestMethod]
        public async Task CanGetAccountChannels()
        {
            var accountChannels = await client.AccountChannels(account);
            Assert.IsNotNull(accountChannels);
        }

        [TestMethod]
        public async Task CanGetAccountCurrencies()
        {
            var currencies = await client.AccountCurrencies(account);
            Assert.IsNotNull(currencies);
        }

        [TestMethod]
        public async Task CanGetAccountLines()
        {
            var accountLines = await client.AccountLines(account);
            Assert.IsNotNull(accountLines);           
        }

        [TestMethod]
        public async Task CanGetAccountOffers()
        {
            var accountOffers = await client.AccountOffers(account);
            Assert.IsNotNull(accountOffers);
        }

        [TestMethod]
        public async Task CanGetAccountObjects()
        {
            var accountObjects = await client.AccountObjects(account);
            Assert.IsNotNull(accountObjects);
        }

        [TestMethod]
        public async Task CanGetAccountTransactions()
        {
            AccountTransactions accountTransactions = await client.AccountTransactions(account);
            Assert.IsNotNull(accountTransactions);
        }

        [TestMethod]
        public async Task CanGetAccountTransactionsAsBinary()
        {
            AccountTransactionsRequest transactionsRequest = new AccountTransactionsRequest(account);
            transactionsRequest.Binary = true;
            var accountTransactions = await client.AccountTransactions(transactionsRequest);
            Assert.IsNotNull(accountTransactions);
        }

        [TestMethod]
        public void CanCreateNoRippleCheckRequest()
        {
            NoRippleCheckRequest request = new NoRippleCheckRequest(account);
            request.Role = RoleType.User;

            string json = JsonConvert.SerializeObject(request);
            JObject jObject = JObject.Parse(json);
            JToken role = jObject["role"];
            Assert.AreEqual("user", role.Value<string>());
        }

        [TestMethod]
        public async Task CanPerformNoRippleCheck()
        {
            NoRippleCheck noRippleCheck = await client.NoRippleCheck(account);
            Assert.IsNotNull(noRippleCheck);
        }

        [TestMethod]
        public async Task CanPerformNoRippleCheckWithRequest()
        {
            NoRippleCheckRequest request = new NoRippleCheckRequest(account);
            request.Role = RoleType.Gateway;
            NoRippleCheck noRippleCheck = await client.NoRippleCheck(request);            
            Assert.IsNotNull(noRippleCheck);
        }

        [TestMethod]
        public async Task CanGetGatewayBalances()
        {
            var balances = await client.GatewayBalances(account);
            Assert.IsNotNull(balances);
        }

        [TestMethod]
        public void CanDecodeMetaBinary()
        {
            var meta = "201C00000000F8E511006125004D1B5655AB8953C6EFD2EBDD5D6314CE0DAFF4CA5B180AE1792B47468ECA5EC5AED28CA45698430F87624433A745D429635D1B2B982EF22AC7E4112ACF43DA97AEF258DC76E6624000000254C5E530E1E7220000000024000000022D00000000624000000254D527708114A2D0815DD52160FF1979A60C50B00C09ECD669D4E1E1E511006125004D1B5655AB8953C6EFD2EBDD5D6314CE0DAFF4CA5B180AE1792B47468ECA5EC5AED28CA456CECECAD86B8E831AA8DB7C8AE259D7D5C1B85C6A5FB9A4341CF560DFB8242F10E624000000126240000002534F23B0E1E7220000000024000000132D000000016240000002533FE1668114656CFDA8B366CAFE7EDC195A6DE87921FB70C231E1E1F1031000";
            StObject obj = StObject.FromHex(meta);
            Assert.IsNotNull(obj);
        }
    }
}
