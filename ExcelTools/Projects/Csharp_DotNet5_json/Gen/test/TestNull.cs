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

public sealed partial class TestNull :  Bright.Config.BeanBase 
{
    public TestNull(JsonElement _json) 
    {
        Id = _json.GetProperty("id").GetInt32();
        { if (_json.TryGetProperty("x1", out var _j) && _j.ValueKind != JsonValueKind.Null) { X1 = _j.GetInt32(); } else { X1 = null; } }
        { if (_json.TryGetProperty("x2", out var _j) && _j.ValueKind != JsonValueKind.Null) { X2 = (test.DemoEnum)_j.GetInt32(); } else { X2 = null; } }
        { if (_json.TryGetProperty("x3", out var _j) && _j.ValueKind != JsonValueKind.Null) { X3 =  test.DemoType1.DeserializeDemoType1(_j); } else { X3 = null; } }
        { if (_json.TryGetProperty("x4", out var _j) && _j.ValueKind != JsonValueKind.Null) { X4 =  test.DemoDynamic.DeserializeDemoDynamic(_j); } else { X4 = null; } }
        { if (_json.TryGetProperty("s1", out var _j) && _j.ValueKind != JsonValueKind.Null) { S1 = _j.GetString(); } else { S1 = null; } }
        { if (_json.TryGetProperty("s2", out var _j) && _j.ValueKind != JsonValueKind.Null) { S2_l10n_key = _j.GetProperty("key").GetString();S2 = _j.GetProperty("text").GetString(); } else { S2 = null; } }
        PostInit();
    }

    public TestNull(int id, int? x1, test.DemoEnum? x2, test.DemoType1 x3, test.DemoDynamic x4, string s1, string s2 ) 
    {
        this.Id = id;
        this.X1 = x1;
        this.X2 = x2;
        this.X3 = x3;
        this.X4 = x4;
        this.S1 = s1;
        this.S2 = s2;
        PostInit();
    }

    public static TestNull DeserializeTestNull(JsonElement _json)
    {
        return new test.TestNull(_json);
    }

    public int Id { get; private set; }
    public int? X1 { get; private set; }
    public test.DemoEnum? X2 { get; private set; }
    public test.DemoType1 X3 { get; private set; }
    public test.DemoDynamic X4 { get; private set; }
    public string S1 { get; private set; }
    public string S2 { get; private set; }
    public string S2_l10n_key { get; }

    public const int __ID__ = 339868469;
    public override int GetTypeId() => __ID__;

    public  void Resolve(Dictionary<string, object> _tables)
    {
        X3?.Resolve(_tables);
        X4?.Resolve(_tables);
        PostResolve();
    }

    public  void TranslateText(System.Func<string, string, string> translator)
    {
        X3?.TranslateText(translator);
        X4?.TranslateText(translator);
        S2 = translator(S2_l10n_key, S2);
    }

    public override string ToString()
    {
        return "{ "
        + "Id:" + Id + ","
        + "X1:" + X1 + ","
        + "X2:" + X2 + ","
        + "X3:" + X3 + ","
        + "X4:" + X4 + ","
        + "S1:" + S1 + ","
        + "S2:" + S2 + ","
        + "}";
    }

    partial void PostInit();
    partial void PostResolve();
}
}
