
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







namespace cfg.item
{
    [Serializable]
    public partial class ChooseOneBonus : AConfig
    {
        [JsonProperty("drop_id")]
        private int _drop_id { get; set; }
        [JsonIgnore]
        public EncryptInt drop_id { get; private set; } = new();

        public bool  is_unique { get; set; }


        public override void EndInit() 
        {
            drop_id = _drop_id;
        }

        public override string ToString() => JsonConvert.SerializeObject(this);
    }
}
