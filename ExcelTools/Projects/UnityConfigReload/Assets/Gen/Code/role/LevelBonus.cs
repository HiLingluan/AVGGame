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



namespace cfg.role
{

public sealed partial class LevelBonus :  Bright.Config.BeanBase 
{
    public LevelBonus(ByteBuf _buf) 
    {
        Id = _buf.ReadInt();
        {int n = System.Math.Min(_buf.ReadSize(), _buf.Size);DistinctBonusInfos = new System.Collections.Generic.List<role.DistinctBonusInfos>(n);for(var i = 0 ; i < n ; i++) { role.DistinctBonusInfos _e;  _e = role.DistinctBonusInfos.DeserializeDistinctBonusInfos(_buf); DistinctBonusInfos.Add(_e);}}
        PostInit();
    }

    public static LevelBonus DeserializeLevelBonus(ByteBuf _buf)
    {
        return new role.LevelBonus(_buf);
    }

    public int Id { get; protected set; }
    public System.Collections.Generic.List<role.DistinctBonusInfos> DistinctBonusInfos { get; protected set; }

    public const int __ID__ = -572269677;
    public override int GetTypeId() => __ID__;

    public  void Resolve(Dictionary<string, object> _tables)
    {
        foreach(var _e in DistinctBonusInfos) { _e?.Resolve(_tables); }
        PostResolve();
    }

    public  void TranslateText(System.Func<string, string, string> translator)
    {
        foreach(var _e in DistinctBonusInfos) { _e?.TranslateText(translator); }
    }

    public void Reload(LevelBonus reloadData)
    {
        Id = reloadData.Id;
        if(DistinctBonusInfos==null)
        {
            DistinctBonusInfos = reloadData.DistinctBonusInfos;
        }else
        {
            DistinctBonusInfos.Capacity = reloadData.DistinctBonusInfos.Count;
            for (int i = 0; i < reloadData.DistinctBonusInfos.Count; i++)
            {
                if(DistinctBonusInfos[i]!=null)
                {
                    DistinctBonusInfos[i].Reload(reloadData.DistinctBonusInfos[i]);
                }else
                {
                    DistinctBonusInfos[i] = reloadData.DistinctBonusInfos[i];
                }
            }
        }
    }

    public override string ToString()
    {
        return "{ "
        + "Id:" + Id + ","
        + "DistinctBonusInfos:" + Bright.Common.StringUtil.CollectionToString(DistinctBonusInfos) + ","
        + "}";
    }
    
    partial void PostInit();
    partial void PostResolve();
}

}
