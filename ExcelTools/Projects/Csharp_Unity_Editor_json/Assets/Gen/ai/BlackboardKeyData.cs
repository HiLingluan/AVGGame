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

public sealed partial class BlackboardKeyData :  ai.KeyData 
{
    public BlackboardKeyData()
    {
            Value = "";
    }

    public override void LoadJson(SimpleJSON.JSONObject _json)
    {
        { 
            var _fieldJson = _json["value"];
            if (_fieldJson != null)
            {
                if(!_fieldJson.IsString) { throw new SerializationException(); }  Value = _fieldJson;
            }
        }
        
    }

    public override void SaveJson(SimpleJSON.JSONObject _json)
    {
        _json["$type"] = "ai.BlackboardKeyData";
        {

            if (Value == null) { throw new System.ArgumentNullException(); }
            _json["value"] = new JSONString(Value);
        }
    }

    public static BlackboardKeyData LoadJsonBlackboardKeyData(SimpleJSON.JSONNode _json)
    {
        BlackboardKeyData obj = new ai.BlackboardKeyData();
        obj.LoadJson((SimpleJSON.JSONObject)_json);
        return obj;
    }
        
    public static void SaveJsonBlackboardKeyData(BlackboardKeyData _obj, SimpleJSON.JSONNode _json)
    {
        _obj.SaveJson((SimpleJSON.JSONObject)_json);
    }

    public string Value { get; set; }

}
}
