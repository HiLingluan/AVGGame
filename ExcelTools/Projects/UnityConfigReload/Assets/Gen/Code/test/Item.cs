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



namespace cfg.test
{

public sealed partial class Item :  test.ItemBase 
{
    public Item(ByteBuf _buf)  : base(_buf) 
    {
        Num = _buf.ReadInt();
        Price = _buf.ReadInt();
        PostInit();
    }

    public static Item DeserializeItem(ByteBuf _buf)
    {
        return new test.Item(_buf);
    }

    public int Num { get; protected set; }
    public int Price { get; protected set; }

    public const int __ID__ = -1226641649;
    public override int GetTypeId() => __ID__;

    public override void Resolve(Dictionary<string, object> _tables)
    {
        base.Resolve(_tables);
        PostResolve();
    }

    public override void TranslateText(System.Func<string, string, string> translator)
    {
        base.TranslateText(translator);
    }

    public void Reload(Item reloadData)
    {
        Id = reloadData.Id;
        Name = reloadData.Name;
        Desc = reloadData.Desc;
        Num = reloadData.Num;
        Price = reloadData.Price;
    }

    public override string ToString()
    {
        return "{ "
        + "Id:" + Id + ","
        + "Name:" + Name + ","
        + "Desc:" + Desc + ","
        + "Num:" + Num + ","
        + "Price:" + Price + ","
        + "}";
    }
    
    partial void PostInit();
    partial void PostResolve();
}

}