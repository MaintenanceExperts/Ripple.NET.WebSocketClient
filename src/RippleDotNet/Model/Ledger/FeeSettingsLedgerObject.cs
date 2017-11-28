﻿namespace RippleDotNet.Model.Ledger
{
    public class FeeSettingsLedgerObject : BaseRippleLedgerObject
    {
        public uint Flags { get; set; }

        //Transaction fee in drops of XRP as hexidecimal
        public string BaseFee { get; set; }

        public uint ReferenceFeeUnits { get; set; }

        public uint ReserveBase { get; set; }

        public uint ReserveIncrement { get; set; }
    }
}
