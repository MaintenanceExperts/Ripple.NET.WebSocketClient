﻿using Newtonsoft.Json;
using Ripple.WebSocketClient.Responses.Transaction.Interfaces;
using Ripple.WebSocketClient.Responses.Transaction.TransactionTypes;


namespace Ripple.WebSocketClient.Responses.Transaction
{
    public class Submit
    {
        [JsonProperty("engine_result")]
        public string EngineResult { get; set; }

        [JsonProperty("engine_result_code")]
        public int EngineResultCode { get; set; }

        [JsonProperty("engine_result_message")]
        public string EngineResultMessage { get; set; }

        [JsonProperty("tx_blob")]
        public string TransactionBlob { get; set; }

        [JsonProperty("tx_json")]
        public dynamic TransactionJson { get; set; }

        [JsonIgnore]
        public ITransactionResponseCommon Transaction => JsonConvert.DeserializeObject<TransactionResponseCommon>(TransactionJson.ToString());
    }
}
