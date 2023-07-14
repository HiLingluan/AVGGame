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

public sealed partial class TreasureBox :  item.ItemExtra 
{
    public TreasureBox(JsonElement _json)  : base(_json) 
    {
        { if (_json.TryGetProperty("key_item_id", out var _j) && _j.ValueKind != JsonValueKind.Null) { KeyItemId = _j.GetInt32(); } else { KeyItemId = null; } }
        OpenLevel =  condition.MinLevel.DeserializeMinLevel(_json.GetProperty("open_level"));
        UseOnObtain = _json.GetProperty("use_on_obtain").GetBoolean();
        { var _json0 = _json.GetProperty("drop_ids"); DropIds = new System.Collections.Generic.List<int>(_json0.GetArrayLength()); foreach(JsonElement __e in _json0.EnumerateArray()) { int __v;  __v = __e.GetInt32();  DropIds.Add(__v); }   }
        { var _json0 = _json.GetProperty("choose_list"); ChooseList = new System.Collections.Generic.List<item.ChooseOneBonus>(_json0.GetArrayLength()); foreach(JsonElement __e in _json0.EnumerateArray()) { item.ChooseOneBonus __v;  __v =  item.ChooseOneBonus.DeserializeChooseOneBonus(__e);  ChooseList.Add(__v); }   }
        PostInit();
    }

    public TreasureBox(int id, int? key_item_id, condition.MinLevel open_level, bool use_on_obtain, System.Collections.Generic.List<int> drop_ids, System.Collections.Generic.List<item.ChooseOneBonus> choose_list )  : base(id) 
    {
        this.KeyItemId = key_item_id;
        this.OpenLevel = open_level;
        this.UseOnObtain = use_on_obtain;
        this.DropIds = drop_ids;
        this.ChooseList = choose_list;
        PostInit();
    }

    public static TreasureBox DeserializeTreasureBox(JsonElement _json)
    {
        return new item.TreasureBox(_json);
    }

    public int? KeyItemId { get; private set; }
    public condition.MinLevel OpenLevel { get; private set; }
    public bool UseOnObtain { get; private set; }
    public System.Collections.Generic.List<int> DropIds { get; private set; }
    public System.Collections.Generic.List<bonus.DropInfo> DropIds_Ref { get; private set; }
    public System.Collections.Generic.List<item.ChooseOneBonus> ChooseList { get; private set; }

    public const int __ID__ = 1494222369;
    public override int GetTypeId() => __ID__;

    public override void Resolve(Dictionary<string, object> _tables)
    {
        base.Resolve(_tables);
        OpenLevel?.Resolve(_tables);
        { bonus.TbDrop __table = (bonus.TbDrop)_tables["bonus.TbDrop"]; this.DropIds_Ref = new System.Collections.Generic.List<bonus.DropInfo>(); foreach(var __e in DropIds) { this.DropIds_Ref.Add(__table.GetOrDefault(__e)); } }
        foreach(var _e in ChooseList) { _e?.Resolve(_tables); }
        PostResolve();
    }

    public override void TranslateText(System.Func<string, string, string> translator)
    {
        base.TranslateText(translator);
        OpenLevel?.TranslateText(translator);
        foreach(var _e in ChooseList) { _e?.TranslateText(translator); }
    }

    public override string ToString()
    {
        return "{ "
        + "Id:" + Id + ","
        + "KeyItemId:" + KeyItemId + ","
        + "OpenLevel:" + OpenLevel + ","
        + "UseOnObtain:" + UseOnObtain + ","
        + "DropIds:" + Bright.Common.StringUtil.CollectionToString(DropIds) + ","
        + "ChooseList:" + Bright.Common.StringUtil.CollectionToString(ChooseList) + ","
        + "}";
    }

    partial void PostInit();
    partial void PostResolve();
}
}