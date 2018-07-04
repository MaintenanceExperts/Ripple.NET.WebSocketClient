using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Ripple.WebSocketClient.Json.Converters;
using Ripple.WebSocketClient.Model;
using Ripple.WebSocketClient.Model.Transaction.Interfaces;
using Ripple.WebSocketClient.Model.Transaction.TransactionTypes;
using Ripple.WebSocketClient.Responses.Transaction.Interfaces;

namespace Ripple.WebSocketClient.Responses.Transaction.TransactionTypes
{
    [JsonConverter(typeof(TransactionConverter))]
    public abstract class TransactionResponseCommon : BaseTransactionResponse, ITransactionCommon, ITransactionResponseCommon
    {
        public string Account { get; set; }

        public string AccountTxnID { get; set; }

        [JsonConverter(typeof(CurrencyConverter))]
        public Currency Fee { get; set; }

        public TransactionFlags? Flags { get; set; }

        /// <summary>
        /// Although optional, the LastLedgerSequence is strongly recommended on every transaction to ensure it's validated or rejected promptly.
        /// </summary>
        public uint? LastLedgerSequence { get; set; }

        public List<Memo> Memos { get; set; }

        public uint? Sequence { get; set; }

        [JsonProperty("SigningPubKey")]
        public string SigningPublicKey { get; set; }

        public List<Signer> Signers { get; set; }

        [JsonConverter(typeof(StringEnumConverter))]
        public TransactionType TransactionType { get; set; }

        [JsonProperty("TxnSignature")]
        public string TransactionSignature { get; set; }

        [JsonProperty("meta")]
        public Meta Meta { get; set; }

        public string ToJson()
        {
            JsonSerializerSettings serializerSettings = new JsonSerializerSettings();
            serializerSettings.NullValueHandling = NullValueHandling.Ignore;
            serializerSettings.DateTimeZoneHandling = DateTimeZoneHandling.Utc;

            return JsonConvert.SerializeObject(this, serializerSettings);
        }
    }
}
