
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
    public partial class BehaviorTree : AConfig
    {

        public string  name { get; set; }

        public string  desc { get; set; }

        public string  blackboard_id { get; set; }

        [JsonProperty]
        public ai.ComposeNode  root { get; set; }


        public override void EndInit() 
        {
            root.EndInit();
        }

        public override string ToString() => JsonConvert.SerializeObject(this);
    }
}
