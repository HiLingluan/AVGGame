
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
    public partial class UpdateDailyBehaviorProps : AConfig
    {
        public string  satiety_key { get; set; }

        public string  energy_key { get; set; }

        public string  mood_key { get; set; }

        public string  satiety_lower_threshold_key { get; set; }

        public string  satiety_upper_threshold_key { get; set; }

        public string  energy_lower_threshold_key { get; set; }

        public string  energy_upper_threshold_key { get; set; }

        public string  mood_lower_threshold_key { get; set; }

        public string  mood_upper_threshold_key { get; set; }


        public override void EndInit() 
        {
        }

        public override string ToString() => JsonConvert.SerializeObject(this);
    }
}
