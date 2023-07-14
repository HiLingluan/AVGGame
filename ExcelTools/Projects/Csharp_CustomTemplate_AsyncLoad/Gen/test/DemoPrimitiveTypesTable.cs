
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

public sealed class DemoPrimitiveTypesTable :  Bright.Config.BeanBase 
{
    public DemoPrimitiveTypesTable(JsonElement _json) 
    {
        X1 = _json.GetProperty("x1").GetBoolean();
        X2 = _json.GetProperty("x2").GetByte();
        X3 = _json.GetProperty("x3").GetInt16();
        X4 = _json.GetProperty("x4").GetInt32();
        X5 = _json.GetProperty("x5").GetInt64();
        X6 = _json.GetProperty("x6").GetSingle();
        X7 = _json.GetProperty("x7").GetDouble();
        S1 = _json.GetProperty("s1").GetString();
        S2_l10n_key = _json.GetProperty("s2").GetProperty("key").GetString();S2 = _json.GetProperty("s2").GetProperty("text").GetString();
        { var _json0 = _json.GetProperty("v2"); float __x; __x = _json0.GetProperty("x").GetSingle(); float __y; __y = _json0.GetProperty("y").GetSingle(); V2 = new System.Numerics.Vector2(__x, __y); }
        { var _json0 = _json.GetProperty("v3"); float __x; __x = _json0.GetProperty("x").GetSingle(); float __y; __y = _json0.GetProperty("y").GetSingle(); float __z; __z = _json0.GetProperty("z").GetSingle();  V3 = new System.Numerics.Vector3(__x, __y,__z); }
        { var _json0 = _json.GetProperty("v4"); float __x; __x = _json0.GetProperty("x").GetSingle(); float __y; __y = _json0.GetProperty("y").GetSingle(); float __z; __z = _json0.GetProperty("z").GetSingle();  float __w; __w = _json0.GetProperty("w").GetSingle(); V4 = new System.Numerics.Vector4(__x, __y, __z, __w); }
        T1 = _json.GetProperty("t1").GetInt32();
    }

    public DemoPrimitiveTypesTable(bool x1, byte x2, short x3, int x4, long x5, float x6, double x7, string s1, string s2, System.Numerics.Vector2 v2, System.Numerics.Vector3 v3, System.Numerics.Vector4 v4, int t1 ) 
    {
        this.X1 = x1;
        this.X2 = x2;
        this.X3 = x3;
        this.X4 = x4;
        this.X5 = x5;
        this.X6 = x6;
        this.X7 = x7;
        this.S1 = s1;
        this.S2 = s2;
        this.V2 = v2;
        this.V3 = v3;
        this.V4 = v4;
        this.T1 = t1;
    }

    public static DemoPrimitiveTypesTable DeserializeDemoPrimitiveTypesTable(JsonElement _json)
    {
        return new test.DemoPrimitiveTypesTable(_json);
    }

    public bool X1 { get; private set; }
    public byte X2 { get; private set; }
    public short X3 { get; private set; }
    public int X4 { get; private set; }
    public long X5 { get; private set; }
    public float X6 { get; private set; }
    public double X7 { get; private set; }
    public string S1 { get; private set; }
    public string S2 { get; private set; }
    public string S2_l10n_key { get; }
    public System.Numerics.Vector2 V2 { get; private set; }
    public System.Numerics.Vector3 V3 { get; private set; }
    public System.Numerics.Vector4 V4 { get; private set; }
    public int T1 { get; private set; }
    public long T1_Millis => T1 * 1000L;

    public const int __ID__ = -370934083;
    public override int GetTypeId() => __ID__;

    public  void Resolve(Dictionary<string, object> _tables)
    {
    }

    public  void TranslateText(System.Func<string, string, string> translator)
    {
        S2 = translator(S2_l10n_key, S2);
    }

    public override string ToString()
    {
        return "{ "
        + "X1:" + X1 + ","
        + "X2:" + X2 + ","
        + "X3:" + X3 + ","
        + "X4:" + X4 + ","
        + "X5:" + X5 + ","
        + "X6:" + X6 + ","
        + "X7:" + X7 + ","
        + "S1:" + S1 + ","
        + "S2:" + S2 + ","
        + "V2:" + V2 + ","
        + "V3:" + V3 + ","
        + "V4:" + V4 + ","
        + "T1:" + T1 + ","
        + "}";
    }
    }
}
