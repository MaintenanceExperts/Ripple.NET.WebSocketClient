using Newtonsoft.Json;

namespace Ripple.WebSocketClient.Model.Transaction
{
    public class ChannelAuthorize
    {
        [JsonProperty("signature")]
        public string Signature { get; set; }
    }
}
