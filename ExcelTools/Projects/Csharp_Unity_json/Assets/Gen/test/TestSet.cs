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



namespace cfg.test
{

public sealed partial class TestSet :  Bright.Config.BeanBase 
{
    public TestSet(JSONNode _json) 
    {
        { if(!_json["id"].IsNumber) { throw new SerializationException(); }  Id = _json["id"]; }
        { if(!_json["x0"].IsString) { throw new SerializationException(); }  X0 = _json["x0"]; }
        { var _json1 = _json["x1"]; if(!_json1.IsArray) { throw new SerializationException(); } X1 = new System.Collections.Generic.List<int>(_json1.Count); foreach(JSONNode __e in _json1.Children) { int __v;  { if(!__e.IsNumber) { throw new SerializationException(); }  __v = __e; }  X1.Add(__v); }   }
        { var _json1 = _json["x2"]; if(!_json1.IsArray) { throw new SerializationException(); } X2 = new System.Collections.Generic.List<long>(_json1.Count); foreach(JSONNode __e in _json1.Children) { long __v;  { if(!__e.IsNumber) { throw new SerializationException(); }  __v = __e; }  X2.Add(__v); }   }
        { var _json1 = _json["x3"]; if(!_json1.IsArray) { throw new SerializationException(); } X3 = new System.Collections.Generic.List<string>(_json1.Count); foreach(JSONNode __e in _json1.Children) { string __v;  { if(!__e.IsString) { throw new SerializationException(); }  __v = __e; }  X3.Add(__v); }   }
        { var _json1 = _json["x4"]; if(!_json1.IsArray) { throw new SerializationException(); } X4 = new System.Collections.Generic.List<test.DemoEnum>(_json1.Count); foreach(JSONNode __e in _json1.Children) { test.DemoEnum __v;  { if(!__e.IsNumber) { throw new SerializationException(); }  __v = (test.DemoEnum)__e.AsInt; }  X4.Add(__v); }   }
        PostInit();
    }

    public TestSet(int id, string x0, System.Collections.Generic.List<int> x1, System.Collections.Generic.List<long> x2, System.Collections.Generic.List<string> x3, System.Collections.Generic.List<test.DemoEnum> x4 ) 
    {
        this.Id = id;
        this.X0 = x0;
        this.X1 = x1;
        this.X2 = x2;
        this.X3 = x3;
        this.X4 = x4;
        PostInit();
    }

    public static TestSet DeserializeTestSet(JSONNode _json)
    {
        return new test.TestSet(_json);
    }

    public int Id { get; private set; }
    public string X0 { get; private set; }
    public System.Collections.Generic.List<int> X1 { get; private set; }
    public System.Collections.Generic.List<long> X2 { get; private set; }
    public System.Collections.Generic.List<string> X3 { get; private set; }
    public System.Collections.Generic.List<test.DemoEnum> X4 { get; private set; }

    public const int __ID__ = -543221516;
    public override int GetTypeId() => __ID__;

    public  void Resolve(Dictionary<string, object> _tables)
    {
        PostResolve();
    }

    public  void TranslateText(System.Func<string, string, string> translator)
    {
    }

    public override string ToString()
    {
        return "{ "
        + "Id:" + Id + ","
        + "X0:" + X0 + ","
        + "X1:" + Bright.Common.StringUtil.CollectionToString(X1) + ","
        + "X2:" + Bright.Common.StringUtil.CollectionToString(X2) + ","
        + "X3:" + Bright.Common.StringUtil.CollectionToString(X3) + ","
        + "X4:" + Bright.Common.StringUtil.CollectionToString(X4) + ","
        + "}";
    }
    
    partial void PostInit();
    partial void PostResolve();
}
}