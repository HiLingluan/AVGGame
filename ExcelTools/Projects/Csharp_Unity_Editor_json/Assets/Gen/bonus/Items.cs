//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------
using Bright.Serialization;
using System.Collections.Generic;
using SimpleJSON;



namespace editor.cfg.bonus
{

public sealed partial class Items :  bonus.Bonus 
{
    public Items()
    {
            ItemList = System.Array.Empty<bonus.Item>();
    }

    public override void LoadJson(SimpleJSON.JSONObject _json)
    {
        { 
            var _fieldJson = _json["item_list"];
            if (_fieldJson != null)
            {
                if(!_fieldJson.IsArray) { throw new SerializationException(); } int _n = _fieldJson.Count; ItemList = new bonus.Item[_n]; int _index=0; foreach(SimpleJSON.JSONNode __e in _fieldJson.Children) { bonus.Item __v;  if(!__e.IsObject) { throw new SerializationException(); }  __v = bonus.Item.LoadJsonItem(__e);  ItemList[_index++] = __v; }  
            }
        }
        
    }

    public override void SaveJson(SimpleJSON.JSONObject _json)
    {
        _json["$type"] = "bonus.Items";
        {

            if (ItemList == null) { throw new System.ArgumentNullException(); }
            { var __cjson = new JSONArray(); foreach(var _e in ItemList) { { var __bjson = new JSONObject();  bonus.Item.SaveJsonItem(_e, __bjson); __cjson["null"] = __bjson; } } _json["item_list"] = __cjson; }
        }
    }

    public static Items LoadJsonItems(SimpleJSON.JSONNode _json)
    {
        Items obj = new bonus.Items();
        obj.LoadJson((SimpleJSON.JSONObject)_json);
        return obj;
    }
        
    public static void SaveJsonItems(Items _obj, SimpleJSON.JSONNode _json)
    {
        _obj.SaveJson((SimpleJSON.JSONObject)_json);
    }

    public bonus.Item[] ItemList { get; set; }

}
}
