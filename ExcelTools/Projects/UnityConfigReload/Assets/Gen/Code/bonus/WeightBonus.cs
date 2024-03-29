//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------
using Bright.Serialization;
using System.Collections.Generic;
using System.Linq;



namespace cfg.bonus
{

public sealed partial class WeightBonus :  bonus.Bonus 
{
    public WeightBonus(ByteBuf _buf)  : base(_buf) 
    {
        {int n = System.Math.Min(_buf.ReadSize(), _buf.Size);Bonuses = new bonus.WeightBonusInfo[n];for(var i = 0 ; i < n ; i++) { bonus.WeightBonusInfo _e;_e = bonus.WeightBonusInfo.DeserializeWeightBonusInfo(_buf); Bonuses[i] = _e;}}
        PostInit();
    }

    public static WeightBonus DeserializeWeightBonus(ByteBuf _buf)
    {
        return new bonus.WeightBonus(_buf);
    }

    public bonus.WeightBonusInfo[] Bonuses { get; protected set; }

    public const int __ID__ = -362807016;
    public override int GetTypeId() => __ID__;

    public override void Resolve(Dictionary<string, object> _tables)
    {
        base.Resolve(_tables);
        foreach(var _e in Bonuses) { _e?.Resolve(_tables); }
        PostResolve();
    }

    public override void TranslateText(System.Func<string, string, string> translator)
    {
        base.TranslateText(translator);
        foreach(var _e in Bonuses) { _e?.TranslateText(translator); }
    }

    public void Reload(WeightBonus reloadData)
    {
        if(Bonuses==null)
        {
            Bonuses = reloadData.Bonuses;
        }else
        {
            if(Bonuses.Length!=reloadData.Bonuses.Length)
            {
                var newArray = new bonus.WeightBonusInfo[reloadData.Bonuses.Length];
                for(int i = 0; i<newArray.Length; i++)
                {
                    if(i<Bonuses.Length)
                    {
                        newArray[i] = Bonuses[i];
                    }
                }
                typeof(WeightBonus).GetProperty("Bonuses").SetValue(this, newArray);
            }
                for(int i = 0; i<reloadData.Bonuses.Length; i++)
                {
                    if(Bonuses[i]!=null){
                        Bonuses[i].Reload(reloadData.Bonuses[i]);
                    }else{
                        Bonuses[i] = reloadData.Bonuses[i];
                    }
                }
        }
    }

    public override string ToString()
    {
        return "{ "
        + "Bonuses:" + Bright.Common.StringUtil.CollectionToString(Bonuses) + ","
        + "}";
    }
    
    partial void PostInit();
    partial void PostResolve();
}

}
