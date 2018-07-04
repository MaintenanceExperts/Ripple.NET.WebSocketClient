﻿using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;
using Ripple.WebSocketClient.Model.Ledger.Objects;

namespace Ripple.WebSocketClient.Model.Ledger
{
    public class LedgerData
    {
        [JsonProperty("ledger_index")]
        public uint LedgerIndex { get; set; }

        [JsonProperty("ledger_hash")]
        public string LedgerHash { get; set; }

        [JsonProperty("state")]
        public List<LedgerDataBinaryOrJson> State { get; set; }

        [JsonProperty("marker")]
        public object Marker { get; set; }
    }

    public class LedgerDataBinaryOrJson
    {
        [JsonProperty("marker")]
        public string Data { get; set; }

        public BaseRippleLedgerObject LedgerObject { get; set; }
    }
}
