
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------
using System;
using Newtonsoft.Json;
using Scripts;







namespace cfg.test
{
    [Serializable]
    public partial class ExcelFromJson : AConfig
    {
        [JsonProperty("x4")]
        private int _x4 { get; set; }
        [JsonIgnore]
        public EncryptInt x4 { get; private set; } = new();

        public bool  x1 { get; set; }

        [JsonProperty("x5")]
        private long _x5 { get; set; }
        [JsonIgnore]
        public EncryptLong x5 { get; private set; } = new();

        [JsonProperty("x6")]
        private float _x6 { get; set; }
        [JsonIgnore]
        public EncryptFloat x6 { get; private set; } = new();

        public string  s1 { get; set; }

        public string  s2 { get; set; }

        public string S2_l10n_key { get; }
        public System.Numerics.Vector2  v2 { get; set; }

        public System.Numerics.Vector3  v3 { get; set; }

        public System.Numerics.Vector4  v4 { get; set; }

        public int  t1 { get; set; }

        [JsonProperty]
        public test.DemoType1  x12 { get; set; }

        public test.DemoEnum  x13 { get; set; }

        [JsonProperty]
        public test.DemoDynamic  x14 { get; set; }

        public int[]  k1 { get; set; }

        public System.Collections.Generic.Dictionary<int, int>  k8 { get; set; }

        public System.Collections.Generic.List<test.DemoE2>  k9 { get; set; }

        public test.DemoDynamic[]  k15 { get; set; }


        public override void EndInit() 
        {
            x4 = _x4;
            x5 = _x5;
            x6 = _x6;
            x12.EndInit();
            x14.EndInit();
            foreach(var _e in k9) { _e.EndInit(); }
            foreach(var _e in k15) { _e.EndInit(); }
        }

        public override string ToString() => JsonConvert.SerializeObject(this);
    }
}
