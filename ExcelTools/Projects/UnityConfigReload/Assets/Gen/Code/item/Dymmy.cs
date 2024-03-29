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

public sealed partial class Dymmy :  item.ItemExtra 
{
    public Dymmy(ByteBuf _buf)  : base(_buf) 
    {
        Cost = cost.Cost.DeserializeCost(_buf);
        PostInit();
    }

    public static Dymmy DeserializeDymmy(ByteBuf _buf)
    {
        return new item.Dymmy(_buf);
    }

    public cost.Cost Cost { get; protected set; }

    public const int __ID__ = 896889705;
    public override int GetTypeId() => __ID__;

    public override void Resolve(Dictionary<string, object> _tables)
    {
        base.Resolve(_tables);
        Cost?.Resolve(_tables);
        PostResolve();
    }

    public override void TranslateText(System.Func<string, string, string> translator)
    {
        base.TranslateText(translator);
        Cost?.TranslateText(translator);
    }

    public void Reload(Dymmy reloadData)
    {
        Id = reloadData.Id;
        if(Cost==null)
        {
            Cost = reloadData.Cost;
        }else
        {
            if(Cost.GetTypeId() == reloadData.Cost.GetTypeId())
            {
                switch (reloadData.Cost.GetTypeId())
                {
                    case cost.CostCurrency.__ID__:
                        (Cost as cost.CostCurrency).Reload(reloadData.Cost as cost.CostCurrency);
                        break;
                    case cost.CostCurrencies.__ID__:
                        (Cost as cost.CostCurrencies).Reload(reloadData.Cost as cost.CostCurrencies);
                        break;
                    case cost.CostOneItem.__ID__:
                        (Cost as cost.CostOneItem).Reload(reloadData.Cost as cost.CostOneItem);
                        break;
                    case cost.CostItem.__ID__:
                        (Cost as cost.CostItem).Reload(reloadData.Cost as cost.CostItem);
                        break;
                    case cost.CostItems.__ID__:
                        (Cost as cost.CostItems).Reload(reloadData.Cost as cost.CostItems);
                        break;
                }
            }else
            {
                typeof(Dymmy).GetProperty("Cost").SetValue(this,reloadData.Cost);
            }
        }
    }

    public override string ToString()
    {
        return "{ "
        + "Id:" + Id + ","
        + "Cost:" + Cost + ","
        + "}";
    }
    
    partial void PostInit();
    partial void PostResolve();
}

}
