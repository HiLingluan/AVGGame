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

public sealed partial class CompositeJsonTable1 :  Bright.Config.EditorBeanBase 
{
    public CompositeJsonTable1()
    {
            X = "";
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
            var _fieldJson = _json["x"];
            if (_fieldJson != null)
            {
                if(!_fieldJson.IsString) { throw new SerializationException(); }  X = _fieldJson;
            }
        }
        
    }

    public override void SaveJson(SimpleJSON.JSONObject _json)
    {
        _json["$type"] = "test.CompositeJsonTable1";
        {
            _json["id"] = new JSONNumber(Id);
        }
        {

            if (X == null) { throw new System.ArgumentNullException(); }
            _json["x"] = new JSONString(X);
        }
    }

    public static CompositeJsonTable1 LoadJsonCompositeJsonTable1(SimpleJSON.JSONNode _json)
    {
        CompositeJsonTable1 obj = new test.CompositeJsonTable1();
        obj.LoadJson((SimpleJSON.JSONObject)_json);
        return obj;
    }
        
    public static void SaveJsonCompositeJsonTable1(CompositeJsonTable1 _obj, SimpleJSON.JSONNode _json)
    {
        _obj.SaveJson((SimpleJSON.JSONObject)_json);
    }

    public int Id { get; set; }

    public string X { get; set; }

}
}