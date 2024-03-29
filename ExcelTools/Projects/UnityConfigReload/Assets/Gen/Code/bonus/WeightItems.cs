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

public sealed partial class WeightItems :  bonus.Bonus 
{
    public WeightItems(ByteBuf _buf)  : base(_buf) 
    {
        {int n = System.Math.Min(_buf.ReadSize(), _buf.Size);ItemList = new bonus.WeightItemInfo[n];for(var i = 0 ; i < n ; i++) { bonus.WeightItemInfo _e;_e = bonus.WeightItemInfo.DeserializeWeightItemInfo(_buf); ItemList[i] = _e;}}
        PostInit();
    }

    public static WeightItems DeserializeWeightItems(ByteBuf _buf)
    {
        return new bonus.WeightItems(_buf);
    }

    public bonus.WeightItemInfo[] ItemList { get; protected set; }

    public const int __ID__ = -356202311;
    public override int GetTypeId() => __ID__;

    public override void Resolve(Dictionary<string, object> _tables)
    {
        base.Resolve(_tables);
        foreach(var _e in ItemList) { _e?.Resolve(_tables); }
        PostResolve();
    }

    public override void TranslateText(System.Func<string, string, string> translator)
    {
        base.TranslateText(translator);
        foreach(var _e in ItemList) { _e?.TranslateText(translator); }
    }

    public void Reload(WeightItems reloadData)
    {
        if(ItemList==null)
        {
            ItemList = reloadData.ItemList;
        }else
        {
            if(ItemList.Length!=reloadData.ItemList.Length)
            {
                var newArray = new bonus.WeightItemInfo[reloadData.ItemList.Length];
                for(int i = 0; i<newArray.Length; i++)
                {
                    if(i<ItemList.Length)
                    {
                        newArray[i] = ItemList[i];
                    }
                }
                typeof(WeightItems).GetProperty("ItemList").SetValue(this, newArray);
            }
                for(int i = 0; i<reloadData.ItemList.Length; i++)
                {
                    if(ItemList[i]!=null){
                        ItemList[i].Reload(reloadData.ItemList[i]);
                    }else{
                        ItemList[i] = reloadData.ItemList[i];
                    }
                }
        }
    }

    public override string ToString()
    {
        return "{ "
        + "ItemList:" + Bright.Common.StringUtil.CollectionToString(ItemList) + ","
        + "}";
    }
    
    partial void PostInit();
    partial void PostResolve();
}

}
