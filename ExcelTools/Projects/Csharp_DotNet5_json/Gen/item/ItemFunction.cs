//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------
using Bright.Serialization;
using System.Collections.Generic;
using System.Text.Json;



namespace cfg.item
{

public sealed partial class ItemFunction :  Bright.Config.BeanBase 
{
    public ItemFunction(JsonElement _json) 
    {
        MinorType = (item.EMinorType)_json.GetProperty("minor_type").GetInt32();
        FuncType = (item.EItemFunctionType)_json.GetProperty("func_type").GetInt32();
        Method = _json.GetProperty("method").GetString();
        CloseBagUi = _json.GetProperty("close_bag_ui").GetBoolean();
        PostInit();
    }

    public ItemFunction(item.EMinorType minor_type, item.EItemFunctionType func_type, string method, bool close_bag_ui ) 
    {
        this.MinorType = minor_type;
        this.FuncType = func_type;
        this.Method = method;
        this.CloseBagUi = close_bag_ui;
        PostInit();
    }

    public static ItemFunction DeserializeItemFunction(JsonElement _json)
    {
        return new item.ItemFunction(_json);
    }

    public item.EMinorType MinorType { get; private set; }
    public item.EItemFunctionType FuncType { get; private set; }
    public string Method { get; private set; }
    public bool CloseBagUi { get; private set; }

    public const int __ID__ = 1205824294;
    public override int GetTypeId() => __ID__;

    public  void Resolve(Dictionary<string, object> _tables)
    {
        PostResolve();
    }

    public  void TranslateText(System.Func<string, string, string> translator)
    {
    }

    public override string ToString()
    {
        return "{ "
        + "MinorType:" + MinorType + ","
        + "FuncType:" + FuncType + ","
        + "Method:" + Method + ","
        + "CloseBagUi:" + CloseBagUi + ","
        + "}";
    }

    partial void PostInit();
    partial void PostResolve();
}
}
