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

public sealed partial class TestUeType :  Bright.Config.EditorBeanBase 
{
    public TestUeType()
    {
            X10 = "";
            X12 = new test.DemoType1();
            X13 = test.ETestUeType.WHITE;
            T1 = "1970-01-01 00:00:00";
            K1 = System.Array.Empty<int>();
            K2 = new System.Collections.Generic.List<int>();
            K5 = new System.Collections.Generic.HashSet<int>();
            K8 = new System.Collections.Generic.Dictionary<int,int>();
            K9 = new System.Collections.Generic.List<test.DemoE2>();
    }

    public override void LoadJson(SimpleJSON.JSONObject _json)
    {
        { 
            var _fieldJson = _json["x1"];
            if (_fieldJson != null)
            {
                if(!_fieldJson.IsBoolean) { throw new SerializationException(); }  X1 = _fieldJson;
            }
        }
        
        { 
            var _fieldJson = _json["x2"];
            if (_fieldJson != null)
            {
                if(!_fieldJson.IsNumber) { throw new SerializationException(); }  X2 = _fieldJson;
            }
        }
        
        { 
            var _fieldJson = _json["x3"];
            if (_fieldJson != null)
            {
                if(!_fieldJson.IsNumber) { throw new SerializationException(); }  X3 = _fieldJson;
            }
        }
        
        { 
            var _fieldJson = _json["x4"];
            if (_fieldJson != null)
            {
                if(!_fieldJson.IsNumber) { throw new SerializationException(); }  X4 = _fieldJson;
            }
        }
        
        { 
            var _fieldJson = _json["x5"];
            if (_fieldJson != null)
            {
                if(!_fieldJson.IsNumber) { throw new SerializationException(); }  X5 = _fieldJson;
            }
        }
        
        { 
            var _fieldJson = _json["x6"];
            if (_fieldJson != null)
            {
                if(!_fieldJson.IsNumber) { throw new SerializationException(); }  X6 = _fieldJson;
            }
        }
        
        { 
            var _fieldJson = _json["x10"];
            if (_fieldJson != null)
            {
                if(!_fieldJson.IsString) { throw new SerializationException(); }  X10 = _fieldJson;
            }
        }
        
        { 
            var _fieldJson = _json["x12"];
            if (_fieldJson != null)
            {
                if(!_fieldJson.IsObject) { throw new SerializationException(); }  X12 = test.DemoType1.LoadJsonDemoType1(_fieldJson);
            }
        }
        
        { 
            var _fieldJson = _json["x13"];
            if (_fieldJson != null)
            {
                if(_fieldJson.IsString) { X13 = (test.ETestUeType)System.Enum.Parse(typeof(test.ETestUeType), _fieldJson); } else if(_fieldJson.IsNumber) { X13 = (test.ETestUeType)(int)_fieldJson; } else { throw new SerializationException(); }  
            }
        }
        
        { 
            var _fieldJson = _json["v2"];
            if (_fieldJson != null)
            {
                if(!_fieldJson.IsObject) { throw new SerializationException(); }  float __x; if(!_fieldJson["x"].IsNumber) { throw new SerializationException(); }  __x = _fieldJson["x"]; float __y; if(!_fieldJson["y"].IsNumber) { throw new SerializationException(); }  __y = _fieldJson["y"]; V2 = new UnityEngine.Vector2(__x, __y);
            }
        }
        
        { 
            var _fieldJson = _json["v3"];
            if (_fieldJson != null)
            {
                if(!_fieldJson.IsObject) { throw new SerializationException(); } float __x; if(!_fieldJson["x"].IsNumber) { throw new SerializationException(); }  __x = _fieldJson["x"]; float __y; if(!_fieldJson["y"].IsNumber) { throw new SerializationException(); }  __y = _fieldJson["y"]; float __z; if(!_fieldJson["z"].IsNumber) { throw new SerializationException(); }  __z = _fieldJson["z"];  V3 = new UnityEngine.Vector3(__x, __y,__z);
            }
        }
        
        { 
            var _fieldJson = _json["v4"];
            if (_fieldJson != null)
            {
                if(!_fieldJson.IsObject) { throw new SerializationException(); } float __x; if(!_fieldJson["x"].IsNumber) { throw new SerializationException(); }  __x = _fieldJson["x"]; float __y; if(!_fieldJson["y"].IsNumber) { throw new SerializationException(); }  __y = _fieldJson["y"]; float __z; if(!_fieldJson["z"].IsNumber) { throw new SerializationException(); }  __z = _fieldJson["z"];  float __w; if(!_fieldJson["w"].IsNumber) { throw new SerializationException(); }  __w = _fieldJson["w"]; V4 = new UnityEngine.Vector4(__x, __y, __z, __w);
            }
        }
        
        { 
            var _fieldJson = _json["t1"];
            if (_fieldJson != null)
            {
                if(!_fieldJson.IsString) { throw new SerializationException(); }  T1 = _fieldJson;
            }
        }
        
        { 
            var _fieldJson = _json["k1"];
            if (_fieldJson != null)
            {
                if(!_fieldJson.IsArray) { throw new SerializationException(); } int _n = _fieldJson.Count; K1 = new int[_n]; int _index=0; foreach(SimpleJSON.JSONNode __e in _fieldJson.Children) { int __v;  if(!__e.IsNumber) { throw new SerializationException(); }  __v = __e;  K1[_index++] = __v; }  
            }
        }
        
        { 
            var _fieldJson = _json["k2"];
            if (_fieldJson != null)
            {
                if(!_fieldJson.IsArray) { throw new SerializationException(); } K2 = new System.Collections.Generic.List<int>(); foreach(JSONNode __e in _fieldJson.Children) { int __v;  if(!__e.IsNumber) { throw new SerializationException(); }  __v = __e;  K2.Add(__v); }  
            }
        }
        
        { 
            var _fieldJson = _json["k5"];
            if (_fieldJson != null)
            {
                if(!_fieldJson.IsArray) { throw new SerializationException(); } K5 = new System.Collections.Generic.HashSet<int>(); foreach(JSONNode __e in _fieldJson.Children) { int __v;  if(!__e.IsNumber) { throw new SerializationException(); }  __v = __e;  K5.Add(__v); }  
            }
        }
        
        { 
            var _fieldJson = _json["k8"];
            if (_fieldJson != null)
            {
                if(!_fieldJson.IsArray) { throw new SerializationException(); } K8 = new System.Collections.Generic.Dictionary<int, int>(); foreach(JSONNode __e in _fieldJson.Children) { int __k;  if(!__e[0].IsNumber) { throw new SerializationException(); }  __k = __e[0]; int __v;  if(!__e[1].IsNumber) { throw new SerializationException(); }  __v = __e[1];  K8.Add(__k, __v); }  
            }
        }
        
        { 
            var _fieldJson = _json["k9"];
            if (_fieldJson != null)
            {
                if(!_fieldJson.IsArray) { throw new SerializationException(); } K9 = new System.Collections.Generic.List<test.DemoE2>(); foreach(JSONNode __e in _fieldJson.Children) { test.DemoE2 __v;  if(!__e.IsObject) { throw new SerializationException(); }  __v = test.DemoE2.LoadJsonDemoE2(__e);  K9.Add(__v); }  
            }
        }
        
    }

    public override void SaveJson(SimpleJSON.JSONObject _json)
    {
        _json["$type"] = "test.TestUeType";
        {
            _json["x1"] = new JSONBool(X1);
        }
        {
            _json["x2"] = new JSONNumber(X2);
        }
        {
            _json["x3"] = new JSONNumber(X3);
        }
        {
            _json["x4"] = new JSONNumber(X4);
        }
        {
            _json["x5"] = new JSONNumber(X5);
        }
        {
            _json["x6"] = new JSONNumber(X6);
        }
        {

            if (X10 == null) { throw new System.ArgumentNullException(); }
            _json["x10"] = new JSONString(X10);
        }
        {

            if (X12 == null) { throw new System.ArgumentNullException(); }
            { var __bjson = new JSONObject();  test.DemoType1.SaveJsonDemoType1(X12, __bjson); _json["x12"] = __bjson; }
        }
        {
            _json["x13"] = new JSONNumber((int)X13);
        }
        {
            { var __vjson = new JSONObject(); __vjson["x"] = V2.x;  __vjson["y"] = V2.y; _json["v2"] = __vjson; }
        }
        {
            { var __vjson = new JSONObject(); __vjson["x"] = V3.x;  __vjson["y"] = V3.y; __vjson["z"] = V3.z; _json["v3"] = __vjson; }
        }
        {
            { var __vjson = new JSONObject(); __vjson["x"] = V4.x;  __vjson["y"] = V4.y; __vjson["z"] = V4.z; __vjson["w"] = V4.w; _json["v4"] = __vjson; }
        }
        {
            _json["t1"] = new JSONString(T1);
        }
        {

            if (K1 == null) { throw new System.ArgumentNullException(); }
            { var __cjson = new JSONArray(); foreach(var _e in K1) { __cjson["null"] = new JSONNumber(_e); } _json["k1"] = __cjson; }
        }
        {

            if (K2 == null) { throw new System.ArgumentNullException(); }
            { var __cjson = new JSONArray(); foreach(var _e in K2) { __cjson["null"] = new JSONNumber(_e); } _json["k2"] = __cjson; }
        }
        {

            if (K5 == null) { throw new System.ArgumentNullException(); }
            { var __cjson = new JSONArray(); foreach(var _e in K5) { __cjson["null"] = new JSONNumber(_e); } _json["k5"] = __cjson; }
        }
        {

            if (K8 == null) { throw new System.ArgumentNullException(); }
            { var __cjson = new JSONArray(); foreach(var _e in K8) { var __entry = new JSONArray(); __cjson[null] = __entry; __entry["null"] = new JSONNumber(_e.Key); __entry["null"] = new JSONNumber(_e.Value); } _json["k8"] = __cjson; }
        }
        {

            if (K9 == null) { throw new System.ArgumentNullException(); }
            { var __cjson = new JSONArray(); foreach(var _e in K9) { { var __bjson = new JSONObject();  test.DemoE2.SaveJsonDemoE2(_e, __bjson); __cjson["null"] = __bjson; } } _json["k9"] = __cjson; }
        }
    }

    public static TestUeType LoadJsonTestUeType(SimpleJSON.JSONNode _json)
    {
        TestUeType obj = new test.TestUeType();
        obj.LoadJson((SimpleJSON.JSONObject)_json);
        return obj;
    }
        
    public static void SaveJsonTestUeType(TestUeType _obj, SimpleJSON.JSONNode _json)
    {
        _obj.SaveJson((SimpleJSON.JSONObject)_json);
    }

    public bool X1 { get; set; }

    public byte X2 { get; set; }

    public short X3 { get; set; }

    public int X4 { get; set; }

    public long X5 { get; set; }

    public float X6 { get; set; }

    public string X10 { get; set; }

    public test.DemoType1 X12 { get; set; }

    public test.ETestUeType X13 { get; set; }

    public UnityEngine.Vector2 V2 { get; set; }

    public UnityEngine.Vector3 V3 { get; set; }

    public UnityEngine.Vector4 V4 { get; set; }

    public string T1 { get; set; }

    public int[] K1 { get; set; }

    public System.Collections.Generic.List<int> K2 { get; set; }

    public System.Collections.Generic.HashSet<int> K5 { get; set; }

    public System.Collections.Generic.Dictionary<int, int> K8 { get; set; }

    public System.Collections.Generic.List<test.DemoE2> K9 { get; set; }

}
}
