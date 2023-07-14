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

public sealed partial class TestMap :  Bright.Config.BeanBase 
{
    public TestMap(JSONNode _json) 
    {
        { if(!_json["id"].IsNumber) { throw new SerializationException(); }  Id = _json["id"]; }
        { var _json1 = _json["x1"]; if(!_json1.IsArray) { throw new SerializationException(); } X1 = new System.Collections.Generic.Dictionary<int, int>(_json1.Count); foreach(JSONNode __e in _json1.Children) { int __k;  { if(!__e[0].IsNumber) { throw new SerializationException(); }  __k = __e[0]; } int __v;  { if(!__e[1].IsNumber) { throw new SerializationException(); }  __v = __e[1]; }  X1.Add(__k, __v); }   }
        { var _json1 = _json["x2"]; if(!_json1.IsArray) { throw new SerializationException(); } X2 = new System.Collections.Generic.Dictionary<long, int>(_json1.Count); foreach(JSONNode __e in _json1.Children) { long __k;  { if(!__e[0].IsNumber) { throw new SerializationException(); }  __k = __e[0]; } int __v;  { if(!__e[1].IsNumber) { throw new SerializationException(); }  __v = __e[1]; }  X2.Add(__k, __v); }   }
        { var _json1 = _json["x3"]; if(!_json1.IsArray) { throw new SerializationException(); } X3 = new System.Collections.Generic.Dictionary<string, int>(_json1.Count); foreach(JSONNode __e in _json1.Children) { string __k;  { if(!__e[0].IsString) { throw new SerializationException(); }  __k = __e[0]; } int __v;  { if(!__e[1].IsNumber) { throw new SerializationException(); }  __v = __e[1]; }  X3.Add(__k, __v); }   }
        { var _json1 = _json["x4"]; if(!_json1.IsArray) { throw new SerializationException(); } X4 = new System.Collections.Generic.Dictionary<test.DemoEnum, int>(_json1.Count); foreach(JSONNode __e in _json1.Children) { test.DemoEnum __k;  { if(!__e[0].IsNumber) { throw new SerializationException(); }  __k = (test.DemoEnum)__e[0].AsInt; } int __v;  { if(!__e[1].IsNumber) { throw new SerializationException(); }  __v = __e[1]; }  X4.Add(__k, __v); }   }
        PostInit();
    }

    public TestMap(int id, System.Collections.Generic.Dictionary<int, int> x1, System.Collections.Generic.Dictionary<long, int> x2, System.Collections.Generic.Dictionary<string, int> x3, System.Collections.Generic.Dictionary<test.DemoEnum, int> x4 ) 
    {
        this.Id = id;
        this.X1 = x1;
        this.X2 = x2;
        this.X3 = x3;
        this.X4 = x4;
        PostInit();
    }

    public static TestMap DeserializeTestMap(JSONNode _json)
    {
        return new test.TestMap(_json);
    }

    public int Id { get; private set; }
    public test.TestIndex Id_Ref { get; private set; }
    public System.Collections.Generic.Dictionary<int, int> X1 { get; private set; }
    public System.Collections.Generic.Dictionary<long, int> X2 { get; private set; }
    public System.Collections.Generic.Dictionary<string, int> X3 { get; private set; }
    public System.Collections.Generic.Dictionary<test.DemoEnum, int> X4 { get; private set; }

    public const int __ID__ = -543227410;
    public override int GetTypeId() => __ID__;

    public  void Resolve(Dictionary<string, object> _tables)
    {
        this.Id_Ref = (_tables["test.TbTestIndex"] as test.TbTestIndex).GetOrDefault(Id);
        PostResolve();
    }

    public  void TranslateText(System.Func<string, string, string> translator)
    {
    }

    public override string ToString()
    {
        return "{ "
        + "Id:" + Id + ","
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
