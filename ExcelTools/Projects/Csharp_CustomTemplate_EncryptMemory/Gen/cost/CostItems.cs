
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







namespace cfg.cost
{
    [Serializable]
    public partial class CostItems : AConfig
    {
        public cost.CostItem[]  item_list { get; set; }


        public override void EndInit() 
        {
            foreach(var _e in item_list) { _e.EndInit(); }
        }

        public override string ToString() => JsonConvert.SerializeObject(this);
    }
}