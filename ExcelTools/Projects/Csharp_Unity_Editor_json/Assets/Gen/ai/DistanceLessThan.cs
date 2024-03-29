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



namespace editor.cfg.ai
{

public sealed partial class DistanceLessThan :  ai.Decorator 
{
    public DistanceLessThan()
    {
            Actor1Key = "";
            Actor2Key = "";
    }

    public override void LoadJson(SimpleJSON.JSONObject _json)
    {
        { 
            var _fieldJson = _json["id"];
            if (_fieldJson != null)
            {
                if(!_fieldJson.IsNumber) { throw new SerializationException(); }  Id = _fieldJson;
            }
        }
        
        { 
            var _fieldJson = _json["node_name"];
            if (_fieldJson != null)
            {
                if(!_fieldJson.IsString) { throw new SerializationException(); }  NodeName = _fieldJson;
            }
        }
        
        { 
            var _fieldJson = _json["flow_abort_mode"];
            if (_fieldJson != null)
            {
                if(_fieldJson.IsString) { FlowAbortMode = (ai.EFlowAbortMode)System.Enum.Parse(typeof(ai.EFlowAbortMode), _fieldJson); } else if(_fieldJson.IsNumber) { FlowAbortMode = (ai.EFlowAbortMode)(int)_fieldJson; } else { throw new SerializationException(); }  
            }
        }
        
        { 
            var _fieldJson = _json["actor1_key"];
            if (_fieldJson != null)
            {
                if(!_fieldJson.IsString) { throw new SerializationException(); }  Actor1Key = _fieldJson;
            }
        }
        
        { 
            var _fieldJson = _json["actor2_key"];
            if (_fieldJson != null)
            {
                if(!_fieldJson.IsString) { throw new SerializationException(); }  Actor2Key = _fieldJson;
            }
        }
        
        { 
            var _fieldJson = _json["distance"];
            if (_fieldJson != null)
            {
                if(!_fieldJson.IsNumber) { throw new SerializationException(); }  Distance = _fieldJson;
            }
        }
        
        { 
            var _fieldJson = _json["reverse_result"];
            if (_fieldJson != null)
            {
                if(!_fieldJson.IsBoolean) { throw new SerializationException(); }  ReverseResult = _fieldJson;
            }
        }
        
    }

    public override void SaveJson(SimpleJSON.JSONObject _json)
    {
        _json["$type"] = "ai.DistanceLessThan";
        {
            _json["id"] = new JSONNumber(Id);
        }
        {

            if (NodeName == null) { throw new System.ArgumentNullException(); }
            _json["node_name"] = new JSONString(NodeName);
        }
        {
            _json["flow_abort_mode"] = new JSONNumber((int)FlowAbortMode);
        }
        {

            if (Actor1Key == null) { throw new System.ArgumentNullException(); }
            _json["actor1_key"] = new JSONString(Actor1Key);
        }
        {

            if (Actor2Key == null) { throw new System.ArgumentNullException(); }
            _json["actor2_key"] = new JSONString(Actor2Key);
        }
        {
            _json["distance"] = new JSONNumber(Distance);
        }
        {
            _json["reverse_result"] = new JSONBool(ReverseResult);
        }
    }

    public static DistanceLessThan LoadJsonDistanceLessThan(SimpleJSON.JSONNode _json)
    {
        DistanceLessThan obj = new ai.DistanceLessThan();
        obj.LoadJson((SimpleJSON.JSONObject)_json);
        return obj;
    }
        
    public static void SaveJsonDistanceLessThan(DistanceLessThan _obj, SimpleJSON.JSONNode _json)
    {
        _obj.SaveJson((SimpleJSON.JSONObject)_json);
    }

    public string Actor1Key { get; set; }

    public string Actor2Key { get; set; }

    public float Distance { get; set; }

    public bool ReverseResult { get; set; }

}
}
