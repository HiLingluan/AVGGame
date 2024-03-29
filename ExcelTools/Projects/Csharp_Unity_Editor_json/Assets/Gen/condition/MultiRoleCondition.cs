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



namespace editor.cfg.condition
{

public sealed partial class MultiRoleCondition :  condition.RoleCondition 
{
    public MultiRoleCondition()
    {
            Conditions = System.Array.Empty<condition.RoleCondition>();
    }

    public override void LoadJson(SimpleJSON.JSONObject _json)
    {
        { 
            var _fieldJson = _json["conditions"];
            if (_fieldJson != null)
            {
                if(!_fieldJson.IsArray) { throw new SerializationException(); } int _n = _fieldJson.Count; Conditions = new condition.RoleCondition[_n]; int _index=0; foreach(SimpleJSON.JSONNode __e in _fieldJson.Children) { condition.RoleCondition __v;  if(!__e.IsObject) { throw new SerializationException(); }  __v = condition.RoleCondition.LoadJsonRoleCondition(__e);  Conditions[_index++] = __v; }  
            }
        }
        
    }

    public override void SaveJson(SimpleJSON.JSONObject _json)
    {
        _json["$type"] = "condition.MultiRoleCondition";
        {

            if (Conditions == null) { throw new System.ArgumentNullException(); }
            { var __cjson = new JSONArray(); foreach(var _e in Conditions) { { var __bjson = new JSONObject();  condition.RoleCondition.SaveJsonRoleCondition(_e, __bjson); __cjson["null"] = __bjson; } } _json["conditions"] = __cjson; }
        }
    }

    public static MultiRoleCondition LoadJsonMultiRoleCondition(SimpleJSON.JSONNode _json)
    {
        MultiRoleCondition obj = new condition.MultiRoleCondition();
        obj.LoadJson((SimpleJSON.JSONObject)_json);
        return obj;
    }
        
    public static void SaveJsonMultiRoleCondition(MultiRoleCondition _obj, SimpleJSON.JSONNode _json)
    {
        _obj.SaveJson((SimpleJSON.JSONObject)_json);
    }

    public condition.RoleCondition[] Conditions { get; set; }

}
}
