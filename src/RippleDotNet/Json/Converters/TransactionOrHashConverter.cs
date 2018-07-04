using System;
using Newtonsoft.Json;
using Ripple.WebSocketClient.Model.Ledger;
using Ripple.WebSocketClient.Model.Transaction.TransactionTypes;

namespace Ripple.WebSocketClient.Json.Converters
{
    public class TransactionOrHashConverter : JsonConverter
    {
        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            throw new NotImplementedException();
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            HashOrTransaction hashOrTransaction = new HashOrTransaction();

            if (reader.TokenType == JsonToken.String)
            {
                hashOrTransaction.TransactionHash = reader.Value.ToString();                
            }
            else
            {
                hashOrTransaction.Transaction = serializer.Deserialize<TransactionCommon>(reader);
            }
            
            return hashOrTransaction;
        }

        public override bool CanConvert(Type objectType)
        {
            return true;
        }
    }
}
