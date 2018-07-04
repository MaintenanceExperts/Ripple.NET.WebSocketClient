using Newtonsoft.Json;

namespace Ripple.WebSocketClient.Responses.Transaction
{
    public class ChannelAuthorize
    {
        [JsonProperty("signature")]
        public string Signature { get; set; }
    }
}
