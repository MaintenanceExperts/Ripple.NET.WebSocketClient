using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace RippleDotNet.Requests.Admin
{
    public class WalletProposeRequest : RippleRequest
    {
        public WalletProposeRequest()
        {
            this.Command = "wallet_propose";
            this.KeyType = "secp256k1";
        }

        [JsonProperty("key_type")]
        public string KeyType { get; set; }

        [JsonProperty("passphrase")]
        public string Passphrase { get; set; }

        [JsonProperty("seed")]
        public string Seed { get; set; }

        [JsonProperty("seed_hex")]
        public string SeedHex { get; set; }
    }
}
