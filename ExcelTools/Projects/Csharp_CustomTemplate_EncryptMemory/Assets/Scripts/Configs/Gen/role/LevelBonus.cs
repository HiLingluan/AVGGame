
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
    public partial class LevelBonus : AConfig
    {

        public System.Collections.Generic.List<role.DistinctBonusInfos>  distinct_bonus_infos { get; set; }


        public override void EndInit() 
        {
            foreach(var _e in distinct_bonus_infos) { _e.EndInit(); }
        }

        public override string ToString() => JsonConvert.SerializeObject(this);
    }
}