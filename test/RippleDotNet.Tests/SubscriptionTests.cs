using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;
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
        private static string mainNetUrl = "wss://s2.ripple.com:443";
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

        [TestMethod]
        public async Task SubscribeToAccountStream()
        {
            /*{
  "engine_result": "tesSUCCESS",
  "engine_result_code": 0,
  "engine_result_message": "The transaction was applied. Only final in a validated ledger.",
  "ledger_hash": "A4A35B9C23EFF806BAFAED1D4F63475E10002E40B0A409718F8507320024C0A3",
  "ledger_index": 42577394,
  "meta": {
    "AffectedNodes": [
      {
        "ModifiedNode": {
          "FinalFields": {
            "Account": "rK7D3QnTrYdkp1fGKKzHFNXZpqN8dUCfaf",
            "Balance": "1582570858693",
            "Flags": 1179648,
            "OwnerCount": 5,
            "Sequence": 682
          },
          "LedgerEntryType": "AccountRoot",
          "LedgerIndex": "56C6890B573E333E7CF18DFA1DAB581595F14C218FA33068D5379B33AC49BA2B",
          "PreviousFields": {
            "Balance": "1582470858693"
          },
          "PreviousTxnID": "FDDBFDE3091B64F3AEFCE522D4DFE55D67F68F168DFA574118DB7086B982EE4C",
          "PreviousTxnLgrSeq": 42575220
        }
      },
      {
        "ModifiedNode": {
          "FinalFields": {
            "Account": "rDsbeomae4FXwgQTJp9Rs64Qg9vDiTCdBv",
            "Balance": "2748465618040",
            "Flags": 131072,
            "OwnerCount": 0,
            "Sequence": 636135
          },
          "LedgerEntryType": "AccountRoot",
          "LedgerIndex": "C3B625B296E95A21D7BBBB7E3D343AF423B463B87B5D56EE7F79C8E16A47A6F5",
          "PreviousFields": {
            "Balance": "2748565678040",
            "Sequence": 636134
          },
          "PreviousTxnID": "48F4B2D79D1AE3B26D70DE6D26F4988FD9B4643410BDED41F49BCADB9D850875",
          "PreviousTxnLgrSeq": 42577122
        }
      }
    ],
    "TransactionIndex": 0,
    "TransactionResult": "tesSUCCESS"
  },
  "status": "closed",
  "transaction": {
    "Account": "rDsbeomae4FXwgQTJp9Rs64Qg9vDiTCdBv",
    "Amount": "100000000",
    "Destination": "rK7D3QnTrYdkp1fGKKzHFNXZpqN8dUCfaf",
    "DestinationTag": 153,
    "Fee": "60000",
    "Flags": 2147483648,
    "LastLedgerSequence": 42578393,
    "Sequence": 636134,
    "SigningPubKey": "03D847C2DBED3ABF0453F71DCD7641989136277218DF516AD49519C9693F32727E",
    "TransactionType": "Payment",
    "TxnSignature": "3045022100BFE470C6E3284808DEB5FE56097E9AD8EE616E3CE250524AEE3D3FC7811E33160220527FDAEAE5D09B2D949E0DF3A2DF2994A4F2FA43A0D17261C4029703CF885052",
    "date": 594087992,
    "hash": "5510C05D76FBC5259141222623C45D59F5DB50C46D8222516813A35403F65D97"
  },
  "type": "transaction",
  "validated": true
}
             */

            const string bitstampHotWallet = "rDsbeomae4FXwgQTJp9Rs64Qg9vDiTCdBv";

            this.client = new RippleClient(mainNetUrl);
            await this.client.Connect();

            SubscriptionStream stream = await this.client.Subscribe(new Requests.Subscription.SubscriptionRequest { Accounts = new List<string>() { bitstampHotWallet } });

            int i = 0;

            while(true)
            {
                if (i >= 3)
                {
                    await this.client.Unsubscribe();
                    break;
                }

                IDictionary<string, object> nextItem = await stream.Next();
                string json = JsonConvert.SerializeObject(nextItem);

                i++;
            }

            await this.client.Ping();
        }

    }
}
