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



namespace cfg.item
{

/// <summary>
/// 道具
/// </summary>
public sealed partial class Item :  Bright.Config.BeanBase 
{
    public Item(ByteBuf _buf) 
    {
        Id = _buf.ReadInt();
        Name = _buf.ReadString();
        MajorType = (item.EMajorType)_buf.ReadInt();
        MinorType = (item.EMinorType)_buf.ReadInt();
        MaxPileNum = _buf.ReadInt();
        Quality = (item.EItemQuality)_buf.ReadInt();
        Icon = _buf.ReadString();
        IconBackgroud = _buf.ReadString();
        IconMask = _buf.ReadString();
        Desc = _buf.ReadString();
        ShowOrder = _buf.ReadInt();
        Quantifier = _buf.ReadString();
        ShowInBag = _buf.ReadBool();
        MinShowLevel = _buf.ReadInt();
        BatchUsable = _buf.ReadBool();
        ProgressTimeWhenUse = _buf.ReadFloat();
        ShowHintWhenUse = _buf.ReadBool();
        Droppable = _buf.ReadBool();
        if(_buf.ReadBool()){ Price = _buf.ReadInt(); } else { Price = null; }
        UseType = (item.EUseType)_buf.ReadInt();
        if(_buf.ReadBool()){ LevelUpId = _buf.ReadInt(); } else { LevelUpId = null; }
        PostInit();
    }

    public static Item DeserializeItem(ByteBuf _buf)
    {
        return new item.Item(_buf);
    }

    /// <summary>
    /// 道具id
    /// </summary>
    public int Id { get; protected set; }
    public string Name { get; protected set; }
    public item.EMajorType MajorType { get; protected set; }
    public item.EMinorType MinorType { get; protected set; }
    public int MaxPileNum { get; protected set; }
    public item.EItemQuality Quality { get; protected set; }
    public string Icon { get; protected set; }
    public string IconBackgroud { get; protected set; }
    public string IconMask { get; protected set; }
    public string Desc { get; protected set; }
    public int ShowOrder { get; protected set; }
    public string Quantifier { get; protected set; }
    public bool ShowInBag { get; protected set; }
    public int MinShowLevel { get; protected set; }
    public bool BatchUsable { get; protected set; }
    public float ProgressTimeWhenUse { get; protected set; }
    public bool ShowHintWhenUse { get; protected set; }
    public bool Droppable { get; protected set; }
    public int? Price { get; protected set; }
    public item.EUseType UseType { get; protected set; }
    public int? LevelUpId { get; protected set; }

    public const int __ID__ = 2107285806;
    public override int GetTypeId() => __ID__;

    public  void Resolve(Dictionary<string, object> _tables)
    {
        PostResolve();
    }

    public  void TranslateText(System.Func<string, string, string> translator)
    {
    }

    public void Reload(Item reloadData)
    {
        Id = reloadData.Id;
        Name = reloadData.Name;
        MajorType = reloadData.MajorType;
        MinorType = reloadData.MinorType;
        MaxPileNum = reloadData.MaxPileNum;
        Quality = reloadData.Quality;
        Icon = reloadData.Icon;
        IconBackgroud = reloadData.IconBackgroud;
        IconMask = reloadData.IconMask;
        Desc = reloadData.Desc;
        ShowOrder = reloadData.ShowOrder;
        Quantifier = reloadData.Quantifier;
        ShowInBag = reloadData.ShowInBag;
        MinShowLevel = reloadData.MinShowLevel;
        BatchUsable = reloadData.BatchUsable;
        ProgressTimeWhenUse = reloadData.ProgressTimeWhenUse;
        ShowHintWhenUse = reloadData.ShowHintWhenUse;
        Droppable = reloadData.Droppable;
        Price = reloadData.Price;
        UseType = reloadData.UseType;
        LevelUpId = reloadData.LevelUpId;
    }

    public override string ToString()
    {
        return "{ "
        + "Id:" + Id + ","
        + "Name:" + Name + ","
        + "MajorType:" + MajorType + ","
        + "MinorType:" + MinorType + ","
        + "MaxPileNum:" + MaxPileNum + ","
        + "Quality:" + Quality + ","
        + "Icon:" + Icon + ","
        + "IconBackgroud:" + IconBackgroud + ","
        + "IconMask:" + IconMask + ","
        + "Desc:" + Desc + ","
        + "ShowOrder:" + ShowOrder + ","
        + "Quantifier:" + Quantifier + ","
        + "ShowInBag:" + ShowInBag + ","
        + "MinShowLevel:" + MinShowLevel + ","
        + "BatchUsable:" + BatchUsable + ","
        + "ProgressTimeWhenUse:" + ProgressTimeWhenUse + ","
        + "ShowHintWhenUse:" + ShowHintWhenUse + ","
        + "Droppable:" + Droppable + ","
        + "Price:" + Price + ","
        + "UseType:" + UseType + ","
        + "LevelUpId:" + LevelUpId + ","
        + "}";
    }
    
    partial void PostInit();
    partial void PostResolve();
}

}
