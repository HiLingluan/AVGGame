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



namespace editor.cfg.error
{

public sealed partial class CodeInfo :  Bright.Config.EditorBeanBase 
{
    public CodeInfo()
    {
            Code = error.EErrorCode.OK;
            Key = "";
    }

    public override void LoadJson(SimpleJSON.JSONObject _json)
    {
        { 
            var _fieldJson = _json["code"];
            if (_fieldJson != null)
            {
                if(_fieldJson.IsString) { Code = (error.EErrorCode)System.Enum.Parse(typeof(error.EErrorCode), _fieldJson); } else if(_fieldJson.IsNumber) { Code = (error.EErrorCode)(int)_fieldJson; } else { throw new SerializationException(); }  
            }
        }
        
        { 
            var _fieldJson = _json["key"];
            if (_fieldJson != null)
            {
                if(!_fieldJson.IsString) { throw new SerializationException(); }  Key = _fieldJson;
            }
        }
        
    }

    public override void SaveJson(SimpleJSON.JSONObject _json)
    {
        _json["$type"] = "error.CodeInfo";
        {
            _json["code"] = new JSONNumber((int)Code);
        }
        {

            if (Key == null) { throw new System.ArgumentNullException(); }
            _json["key"] = new JSONString(Key);
        }
    }

    public static CodeInfo LoadJsonCodeInfo(SimpleJSON.JSONNode _json)
    {
        CodeInfo obj = new error.CodeInfo();
        obj.LoadJson((SimpleJSON.JSONObject)_json);
        return obj;
    }
        
    public static void SaveJsonCodeInfo(CodeInfo _obj, SimpleJSON.JSONNode _json)
    {
        _obj.SaveJson((SimpleJSON.JSONObject)_json);
    }

    public error.EErrorCode Code { get; set; }

    public string Key { get; set; }

}
}
