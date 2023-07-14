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



namespace cfg.condition
{

public sealed partial class MultiRoleCondition :  condition.RoleCondition 
{
    public MultiRoleCondition(ByteBuf _buf)  : base(_buf) 
    {
        {int n = System.Math.Min(_buf.ReadSize(), _buf.Size);Conditions = new condition.RoleCondition[n];for(var i = 0 ; i < n ; i++) { condition.RoleCondition _e;_e = condition.RoleCondition.DeserializeRoleCondition(_buf); Conditions[i] = _e;}}
        PostInit();
    }

    public static MultiRoleCondition DeserializeMultiRoleCondition(ByteBuf _buf)
    {
        return new condition.MultiRoleCondition(_buf);
    }

    public condition.RoleCondition[] Conditions { get; protected set; }

    public const int __ID__ = 934079583;
    public override int GetTypeId() => __ID__;

    public override void Resolve(Dictionary<string, object> _tables)
    {
        base.Resolve(_tables);
        foreach(var _e in Conditions) { _e?.Resolve(_tables); }
        PostResolve();
    }

    public override void TranslateText(System.Func<string, string, string> translator)
    {
        base.TranslateText(translator);
        foreach(var _e in Conditions) { _e?.TranslateText(translator); }
    }

    public void Reload(MultiRoleCondition reloadData)
    {
        if(Conditions==null)
        {
            Conditions = reloadData.Conditions;
        }else
        {
            if(Conditions.Length!=reloadData.Conditions.Length)
            {
                var newArray = new condition.RoleCondition[reloadData.Conditions.Length];
                for(int i = 0; i<newArray.Length; i++)
                {
                    if(i<Conditions.Length)
                    {
                        newArray[i] = Conditions[i];
                    }
                }
                typeof(MultiRoleCondition).GetProperty("Conditions").SetValue(this, newArray);
            }
                // array is_dynamic
                for(int i = 0; i<reloadData.Conditions.Length; i++)
                {
                    if(Conditions[i]!=null && Conditions[i].GetTypeId() == reloadData.Conditions[i].GetTypeId())
                    {
                        switch (reloadData.Conditions[i].GetTypeId())
                        {
                            case condition.MultiRoleCondition.__ID__:
                                (Conditions[i] as condition.MultiRoleCondition).Reload(reloadData.Conditions[i] as condition.MultiRoleCondition);
                                break;
                            case condition.GenderLimit.__ID__:
                                (Conditions[i] as condition.GenderLimit).Reload(reloadData.Conditions[i] as condition.GenderLimit);
                                break;
                            case condition.MinLevel.__ID__:
                                (Conditions[i] as condition.MinLevel).Reload(reloadData.Conditions[i] as condition.MinLevel);
                                break;
                            case condition.MaxLevel.__ID__:
                                (Conditions[i] as condition.MaxLevel).Reload(reloadData.Conditions[i] as condition.MaxLevel);
                                break;
                            case condition.MinMaxLevel.__ID__:
                                (Conditions[i] as condition.MinMaxLevel).Reload(reloadData.Conditions[i] as condition.MinMaxLevel);
                                break;
                            case condition.ClothesPropertyScoreGreaterThan.__ID__:
                                (Conditions[i] as condition.ClothesPropertyScoreGreaterThan).Reload(reloadData.Conditions[i] as condition.ClothesPropertyScoreGreaterThan);
                                break;
                            case condition.ContainsItem.__ID__:
                                (Conditions[i] as condition.ContainsItem).Reload(reloadData.Conditions[i] as condition.ContainsItem);
                                break;
                        }
                    }else
                    {
                        Conditions[i] = reloadData.Conditions[i];
                    }
                }
        }
    }

    public override string ToString()
    {
        return "{ "
        + "Conditions:" + Bright.Common.StringUtil.CollectionToString(Conditions) + ","
        + "}";
    }
    
    partial void PostInit();
    partial void PostResolve();
}

}