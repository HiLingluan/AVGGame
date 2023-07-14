
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------
using Bright.Serialization;
using System.Collections.Generic;
using System.Text.Json;



namespace cfg.test
{

public sealed class TestRef :  Bright.Config.BeanBase 
{
    public TestRef(JsonElement _json) 
    {
        Id = _json.GetProperty("id").GetInt32();
        X1 = _json.GetProperty("x1").GetInt32();
        X12 = _json.GetProperty("x1_2").GetInt32();
        X2 = _json.GetProperty("x2").GetInt32();
        { var _json0 = _json.GetProperty("a1"); int _n = _json0.GetArrayLength(); A1 = new int[_n]; int _index=0; foreach(JsonElement __e in _json0.EnumerateArray()) { int __v;  __v = __e.GetInt32();  A1[_index++] = __v; }   }
        { var _json0 = _json.GetProperty("a2"); int _n = _json0.GetArrayLength(); A2 = new int[_n]; int _index=0; foreach(JsonElement __e in _json0.EnumerateArray()) { int __v;  __v = __e.GetInt32();  A2[_index++] = __v; }   }
        { var _json0 = _json.GetProperty("b1"); B1 = new System.Collections.Generic.List<int>(_json0.GetArrayLength()); foreach(JsonElement __e in _json0.EnumerateArray()) { int __v;  __v = __e.GetInt32();  B1.Add(__v); }   }
        { var _json0 = _json.GetProperty("b2"); B2 = new System.Collections.Generic.List<int>(_json0.GetArrayLength()); foreach(JsonElement __e in _json0.EnumerateArray()) { int __v;  __v = __e.GetInt32();  B2.Add(__v); }   }
        { var _json0 = _json.GetProperty("c1"); C1 = new System.Collections.Generic.HashSet<int>(_json0.GetArrayLength()); foreach(JsonElement __e in _json0.EnumerateArray()) { int __v;  __v = __e.GetInt32();  C1.Add(__v); }   }
        { var _json0 = _json.GetProperty("c2"); C2 = new System.Collections.Generic.HashSet<int>(_json0.GetArrayLength()); foreach(JsonElement __e in _json0.EnumerateArray()) { int __v;  __v = __e.GetInt32();  C2.Add(__v); }   }
        { var _json0 = _json.GetProperty("d1"); D1 = new System.Collections.Generic.Dictionary<int, int>(_json0.GetArrayLength()); foreach(JsonElement __e in _json0.EnumerateArray()) { int __k;  __k = __e[0].GetInt32(); int __v;  __v = __e[1].GetInt32();  D1.Add(__k, __v); }   }
        { var _json0 = _json.GetProperty("d2"); D2 = new System.Collections.Generic.Dictionary<int, int>(_json0.GetArrayLength()); foreach(JsonElement __e in _json0.EnumerateArray()) { int __k;  __k = __e[0].GetInt32(); int __v;  __v = __e[1].GetInt32();  D2.Add(__k, __v); }   }
    }

    public TestRef(int id, int x1, int x1_2, int x2, int[] a1, int[] a2, System.Collections.Generic.List<int> b1, System.Collections.Generic.List<int> b2, System.Collections.Generic.HashSet<int> c1, System.Collections.Generic.HashSet<int> c2, System.Collections.Generic.Dictionary<int, int> d1, System.Collections.Generic.Dictionary<int, int> d2 ) 
    {
        this.Id = id;
        this.X1 = x1;
        this.X12 = x1_2;
        this.X2 = x2;
        this.A1 = a1;
        this.A2 = a2;
        this.B1 = b1;
        this.B2 = b2;
        this.C1 = c1;
        this.C2 = c2;
        this.D1 = d1;
        this.D2 = d2;
    }

    public static TestRef DeserializeTestRef(JsonElement _json)
    {
        return new test.TestRef(_json);
    }

    public int Id { get; private set; }
    public int X1 { get; private set; }
    public test.TestBeRef X1_Ref { get; private set; }
    public int X12 { get; private set; }
    public int X2 { get; private set; }
    public test.TestBeRef X2_Ref { get; private set; }
    public int[] A1 { get; private set; }
    public int[] A2 { get; private set; }
    public System.Collections.Generic.List<int> B1 { get; private set; }
    public System.Collections.Generic.List<int> B2 { get; private set; }
    public System.Collections.Generic.HashSet<int> C1 { get; private set; }
    public System.Collections.Generic.HashSet<int> C2 { get; private set; }
    public System.Collections.Generic.Dictionary<int, int> D1 { get; private set; }
    public System.Collections.Generic.Dictionary<int, int> D2 { get; private set; }

    public const int __ID__ = -543222491;
    public override int GetTypeId() => __ID__;

    public  void Resolve(Dictionary<string, object> _tables)
    {
        this.X1_Ref = (_tables["test.TbTestBeRef"] as test.TbTestBeRef).GetOrDefault(X1);
        this.X2_Ref = (_tables["test.TbTestBeRef"] as test.TbTestBeRef).GetOrDefault(X2);
    }

    public  void TranslateText(System.Func<string, string, string> translator)
    {
    }

    public override string ToString()
    {
        return "{ "
        + "Id:" + Id + ","
        + "X1:" + X1 + ","
        + "X12:" + X12 + ","
        + "X2:" + X2 + ","
        + "A1:" + Bright.Common.StringUtil.CollectionToString(A1) + ","
        + "A2:" + Bright.Common.StringUtil.CollectionToString(A2) + ","
        + "B1:" + Bright.Common.StringUtil.CollectionToString(B1) + ","
        + "B2:" + Bright.Common.StringUtil.CollectionToString(B2) + ","
        + "C1:" + Bright.Common.StringUtil.CollectionToString(C1) + ","
        + "C2:" + Bright.Common.StringUtil.CollectionToString(C2) + ","
        + "D1:" + Bright.Common.StringUtil.CollectionToString(D1) + ","
        + "D2:" + Bright.Common.StringUtil.CollectionToString(D2) + ","
        + "}";
    }
    }
}
