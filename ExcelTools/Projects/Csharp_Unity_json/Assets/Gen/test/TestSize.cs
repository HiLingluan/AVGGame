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

public sealed partial class TestSize :  Bright.Config.BeanBase 
{
    public TestSize(JSONNode _json) 
    {
        { if(!_json["id"].IsNumber) { throw new SerializationException(); }  Id = _json["id"]; }
        { var _json1 = _json["x1"]; if(!_json1.IsArray) { throw new SerializationException(); } int _n = _json1.Count; X1 = new int[_n]; int _index=0; foreach(JSONNode __e in _json1.Children) { int __v;  { if(!__e.IsNumber) { throw new SerializationException(); }  __v = __e; }  X1[_index++] = __v; }   }
        { var _json1 = _json["x2"]; if(!_json1.IsArray) { throw new SerializationException(); } X2 = new System.Collections.Generic.List<int>(_json1.Count); foreach(JSONNode __e in _json1.Children) { int __v;  { if(!__e.IsNumber) { throw new SerializationException(); }  __v = __e; }  X2.Add(__v); }   }
        { var _json1 = _json["x3"]; if(!_json1.IsArray) { throw new SerializationException(); } X3 = new System.Collections.Generic.HashSet<int>(/*_json1.Count*/); foreach(JSONNode __e in _json1.Children) { int __v;  { if(!__e.IsNumber) { throw new SerializationException(); }  __v = __e; }  X3.Add(__v); }   }
        { var _json1 = _json["x4"]; if(!_json1.IsArray) { throw new SerializationException(); } X4 = new System.Collections.Generic.Dictionary<int, int>(_json1.Count); foreach(JSONNode __e in _json1.Children) { int __k;  { if(!__e[0].IsNumber) { throw new SerializationException(); }  __k = __e[0]; } int __v;  { if(!__e[1].IsNumber) { throw new SerializationException(); }  __v = __e[1]; }  X4.Add(__k, __v); }   }
        PostInit();
    }

    public TestSize(int id, int[] x1, System.Collections.Generic.List<int> x2, System.Collections.Generic.HashSet<int> x3, System.Collections.Generic.Dictionary<int, int> x4 ) 
    {
        this.Id = id;
        this.X1 = x1;
        this.X2 = x2;
        this.X3 = x3;
        this.X4 = x4;
        PostInit();
    }

    public static TestSize DeserializeTestSize(JSONNode _json)
    {
        return new test.TestSize(_json);
    }

    public int Id { get; private set; }
    public int[] X1 { get; private set; }
    public System.Collections.Generic.List<int> X2 { get; private set; }
    public System.Collections.Generic.HashSet<int> X3 { get; private set; }
    public System.Collections.Generic.Dictionary<int, int> X4 { get; private set; }

    public const int __ID__ = 340006319;
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
