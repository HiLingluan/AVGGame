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

public sealed partial class TestDesc :  Bright.Config.EditorBeanBase 
{
    public TestDesc()
    {
            Name = "";
            X1 = new test.H1();
            X2 = new System.Collections.Generic.List<test.H2>();
            X3 = System.Array.Empty<test.H2>();
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
            var _fieldJson = _json["name"];
            if (_fieldJson != null)
            {
                if(!_fieldJson.IsString) { throw new SerializationException(); }  Name = _fieldJson;
            }
        }
        
        { 
            var _fieldJson = _json["a1"];
            if (_fieldJson != null)
            {
                if(!_fieldJson.IsNumber) { throw new SerializationException(); }  A1 = _fieldJson;
            }
        }
        
        { 
            var _fieldJson = _json["a2"];
            if (_fieldJson != null)
            {
                if(!_fieldJson.IsNumber) { throw new SerializationException(); }  A2 = _fieldJson;
            }
        }
        
        { 
            var _fieldJson = _json["x1"];
            if (_fieldJson != null)
            {
                if(!_fieldJson.IsObject) { throw new SerializationException(); }  X1 = test.H1.LoadJsonH1(_fieldJson);
            }
        }
        
        { 
            var _fieldJson = _json["x2"];
            if (_fieldJson != null)
            {
                if(!_fieldJson.IsArray) { throw new SerializationException(); } X2 = new System.Collections.Generic.List<test.H2>(); foreach(JSONNode __e in _fieldJson.Children) { test.H2 __v;  if(!__e.IsObject) { throw new SerializationException(); }  __v = test.H2.LoadJsonH2(__e);  X2.Add(__v); }  
            }
        }
        
        { 
            var _fieldJson = _json["x3"];
            if (_fieldJson != null)
            {
                if(!_fieldJson.IsArray) { throw new SerializationException(); } int _n = _fieldJson.Count; X3 = new test.H2[_n]; int _index=0; foreach(SimpleJSON.JSONNode __e in _fieldJson.Children) { test.H2 __v;  if(!__e.IsObject) { throw new SerializationException(); }  __v = test.H2.LoadJsonH2(__e);  X3[_index++] = __v; }  
            }
        }
        
    }

    public override void SaveJson(SimpleJSON.JSONObject _json)
    {
        {
            _json["id"] = new JSONNumber(Id);
        }
        {

            if (Name == null) { throw new System.ArgumentNullException(); }
            _json["name"] = new JSONString(Name);
        }
        {
            _json["a1"] = new JSONNumber(A1);
        }
        {
            _json["a2"] = new JSONNumber(A2);
        }
        {

            if (X1 == null) { throw new System.ArgumentNullException(); }
            { var __bjson = new JSONObject();  test.H1.SaveJsonH1(X1, __bjson); _json["x1"] = __bjson; }
        }
        {

            if (X2 == null) { throw new System.ArgumentNullException(); }
            { var __cjson = new JSONArray(); foreach(var _e in X2) { { var __bjson = new JSONObject();  test.H2.SaveJsonH2(_e, __bjson); __cjson["null"] = __bjson; } } _json["x2"] = __cjson; }
        }
        {

            if (X3 == null) { throw new System.ArgumentNullException(); }
            { var __cjson = new JSONArray(); foreach(var _e in X3) { { var __bjson = new JSONObject();  test.H2.SaveJsonH2(_e, __bjson); __cjson["null"] = __bjson; } } _json["x3"] = __cjson; }
        }
    }

    public static TestDesc LoadJsonTestDesc(SimpleJSON.JSONNode _json)
    {
        TestDesc obj = new test.TestDesc();
        obj.LoadJson((SimpleJSON.JSONObject)_json);
        return obj;
    }
        
    public static void SaveJsonTestDesc(TestDesc _obj, SimpleJSON.JSONNode _json)
    {
        _obj.SaveJson((SimpleJSON.JSONObject)_json);
    }

    public int Id { get; set; }

    /// <summary>
    /// 禁止
    /// </summary>
    public string Name { get; set; }

    /// <summary>
    /// 测试换行<br/>第2行<br/>第3层
    /// </summary>
    public int A1 { get; set; }

    /// <summary>
    /// 测试转义 &lt; &amp; % / # &gt;
    /// </summary>
    public int A2 { get; set; }

    public test.H1 X1 { get; set; }

    /// <summary>
    /// 这是x2
    /// </summary>
    public System.Collections.Generic.List<test.H2> X2 { get; set; }

    public test.H2[] X3 { get; set; }

}
}
