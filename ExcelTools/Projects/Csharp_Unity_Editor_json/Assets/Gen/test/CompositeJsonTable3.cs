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

public sealed partial class CompositeJsonTable3 :  Bright.Config.EditorBeanBase 
{
    public CompositeJsonTable3()
    {
    }

    public override void LoadJson(SimpleJSON.JSONObject _json)
    {
        { 
            var _fieldJson = _json["a"];
            if (_fieldJson != null)
            {
                if(!_fieldJson.IsNumber) { throw new SerializationException(); }  A = _fieldJson;
            }
        }
        
        { 
            var _fieldJson = _json["b"];
            if (_fieldJson != null)
            {
                if(!_fieldJson.IsNumber) { throw new SerializationException(); }  B = _fieldJson;
            }
        }
        
    }

    public override void SaveJson(SimpleJSON.JSONObject _json)
    {
        _json["$type"] = "test.CompositeJsonTable3";
        {
            _json["a"] = new JSONNumber(A);
        }
        {
            _json["b"] = new JSONNumber(B);
        }
    }

    public static CompositeJsonTable3 LoadJsonCompositeJsonTable3(SimpleJSON.JSONNode _json)
    {
        CompositeJsonTable3 obj = new test.CompositeJsonTable3();
        obj.LoadJson((SimpleJSON.JSONObject)_json);
        return obj;
    }
        
    public static void SaveJsonCompositeJsonTable3(CompositeJsonTable3 _obj, SimpleJSON.JSONNode _json)
    {
        _obj.SaveJson((SimpleJSON.JSONObject)_json);
    }

    public int A { get; set; }

    public int B { get; set; }

}
}
