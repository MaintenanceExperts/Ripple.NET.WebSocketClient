using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Ripple.WebSocketClient.Model.Admin
{
    public class WalletPropose
    {
        [JsonProperty("master_seed")]
        public string MasterSeed { get; set; }

        [JsonProperty("master_seed_hex")]
        public string MasterSeedHex { get; set; }

        [JsonProperty("master_key")]
        public string MasterKey { get; set; }

        [JsonProperty("account_id")]
        public string AccountId { get; set; }

        [JsonProperty("public_key")]
        public string PublicKey { get; set; }

        [JsonProperty("public_key_hex")]
        public string PublicKeyHex { get; set; }

        [JsonProperty("warning")]
        public string Warning { get; set; }
    }
}
