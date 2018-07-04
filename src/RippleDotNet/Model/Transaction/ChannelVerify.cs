using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;

namespace Ripple.WebSocketClient.Model.Transaction
{
    public class ChannelVerify
    {
        [JsonProperty("signature_verified")]
        public bool SignatureVerified { get; set; }
    }
}
