
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
    public partial class ExcelFromJsonMultiRow : AConfig
    {

        [JsonProperty("x")]
        private int _x { get; set; }
        [JsonIgnore]
        public EncryptInt x { get; private set; } = new();

        public System.Collections.Generic.List<test.TestRow>  items { get; set; }


        public override void EndInit() 
        {
            x = _x;
            foreach(var _e in items) { _e.EndInit(); }
        }

        public override string ToString() => JsonConvert.SerializeObject(this);
    }
}
