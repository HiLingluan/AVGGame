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

public sealed partial class UeBlackboard :  ai.Decorator 
{
    public UeBlackboard()
    {
            NotifyObserver = ai.ENotifyObserverMode.ON_VALUE_CHANGE;
            BlackboardKey = "";
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
            var _fieldJson = _json["notify_observer"];
            if (_fieldJson != null)
            {
                if(_fieldJson.IsString) { NotifyObserver = (ai.ENotifyObserverMode)System.Enum.Parse(typeof(ai.ENotifyObserverMode), _fieldJson); } else if(_fieldJson.IsNumber) { NotifyObserver = (ai.ENotifyObserverMode)(int)_fieldJson; } else { throw new SerializationException(); }  
            }
        }
        
        { 
            var _fieldJson = _json["blackboard_key"];
            if (_fieldJson != null)
            {
                if(!_fieldJson.IsString) { throw new SerializationException(); }  BlackboardKey = _fieldJson;
            }
        }
        
        { 
            var _fieldJson = _json["key_query"];
            if (_fieldJson != null)
            {
                if(!_fieldJson.IsObject) { throw new SerializationException(); }  KeyQuery = ai.KeyQueryOperator.LoadJsonKeyQueryOperator(_fieldJson);
            }
        }
        
    }

    public override void SaveJson(SimpleJSON.JSONObject _json)
    {
        _json["$type"] = "ai.UeBlackboard";
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
            _json["notify_observer"] = new JSONNumber((int)NotifyObserver);
        }
        {

            if (BlackboardKey == null) { throw new System.ArgumentNullException(); }
            _json["blackboard_key"] = new JSONString(BlackboardKey);
        }
        {

            if (KeyQuery == null) { throw new System.ArgumentNullException(); }
            { var __bjson = new JSONObject();  ai.KeyQueryOperator.SaveJsonKeyQueryOperator(KeyQuery, __bjson); _json["key_query"] = __bjson; }
        }
    }

    public static UeBlackboard LoadJsonUeBlackboard(SimpleJSON.JSONNode _json)
    {
        UeBlackboard obj = new ai.UeBlackboard();
        obj.LoadJson((SimpleJSON.JSONObject)_json);
        return obj;
    }
        
    public static void SaveJsonUeBlackboard(UeBlackboard _obj, SimpleJSON.JSONNode _json)
    {
        _obj.SaveJson((SimpleJSON.JSONObject)_json);
    }

    public ai.ENotifyObserverMode NotifyObserver { get; set; }

    public string BlackboardKey { get; set; }

    public ai.KeyQueryOperator KeyQuery { get; set; }

}
}