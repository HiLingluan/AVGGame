
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







namespace cfg.ai
{
    [Serializable]
    public partial class UeCooldown : AConfig
    {
        [JsonProperty("cooldown_time")]
        private float _cooldown_time { get; set; }
        [JsonIgnore]
        public EncryptFloat cooldown_time { get; private set; } = new();


        public override void EndInit() 
        {
            cooldown_time = _cooldown_time;
        }

        public override string ToString() => JsonConvert.SerializeObject(this);
    }
}
