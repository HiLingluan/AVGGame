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



namespace editor.cfg.test
{

public sealed partial class H2 :  Bright.Config.EditorBeanBase 
{
    public H2()
    {
    }

    public override void LoadJson(SimpleJSON.JSONObject _json)
    {
        { 
            var _fieldJson = _json["z2"];
            if (_fieldJson != null)
            {
                if(!_fieldJson.IsNumber) { throw new SerializationException(); }  Z2 = _fieldJson;
            }
        }
        
        { 
            var _fieldJson = _json["z3"];
            if (_fieldJson != null)
            {
                if(!_fieldJson.IsNumber) { throw new SerializationException(); }  Z3 = _fieldJson;
            }
        }
        
    }

    public override void SaveJson(SimpleJSON.JSONObject _json)
    {
        _json["$type"] = "test.H2";
        {
            _json["z2"] = new JSONNumber(Z2);
        }
        {
            _json["z3"] = new JSONNumber(Z3);
        }
    }

    public static H2 LoadJsonH2(SimpleJSON.JSONNode _json)
    {
        H2 obj = new test.H2();
        obj.LoadJson((SimpleJSON.JSONObject)_json);
        return obj;
    }
        
    public static void SaveJsonH2(H2 _obj, SimpleJSON.JSONNode _json)
    {
        _obj.SaveJson((SimpleJSON.JSONObject)_json);
    }

    public int Z2 { get; set; }

    public int Z3 { get; set; }

}
}
