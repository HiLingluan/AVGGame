
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







namespace cfg.role
{
    [Serializable]
    public partial class DistinctBonusInfos : AConfig
    {
        [JsonProperty("effective_level")]
        private int _effective_level { get; set; }
        [JsonIgnore]
        public EncryptInt effective_level { get; private set; } = new();

        public System.Collections.Generic.List<role.BonusInfo>  bonus_info { get; set; }


        public override void EndInit() 
        {
            effective_level = _effective_level;
            foreach(var _e in bonus_info) { _e.EndInit(); }
        }

        public override string ToString() => JsonConvert.SerializeObject(this);
    }
}
