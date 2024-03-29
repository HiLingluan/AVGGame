//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------
using Bright.Serialization;
using System.Collections.Generic;
using System.Linq;



namespace cfg.test
{

public sealed partial class DemoPrimitiveTypesTable :  Bright.Config.BeanBase 
{
    public DemoPrimitiveTypesTable(ByteBuf _buf) 
    {
        X1 = _buf.ReadBool();
        X2 = _buf.ReadByte();
        X3 = _buf.ReadShort();
        X4 = _buf.ReadInt();
        X5 = _buf.ReadLong();
        X6 = _buf.ReadFloat();
        X7 = _buf.ReadDouble();
        S1 = _buf.ReadString();
        S2_l10n_key = _buf.ReadString(); S2 = _buf.ReadString();
        V2 = _buf.ReadUnityVector2();
        V3 = _buf.ReadUnityVector3();
        V4 = _buf.ReadUnityVector4();
        T1 = _buf.ReadInt();
        PostInit();
    }

    public static DemoPrimitiveTypesTable DeserializeDemoPrimitiveTypesTable(ByteBuf _buf)
    {
        return new test.DemoPrimitiveTypesTable(_buf);
    }

    public bool X1 { get; protected set; }
    public byte X2 { get; protected set; }
    public short X3 { get; protected set; }
    public int X4 { get; protected set; }
    public long X5 { get; protected set; }
    public float X6 { get; protected set; }
    public double X7 { get; protected set; }
    public string S1 { get; protected set; }
    public string S2 { get; protected set; }
    //field.gen_text_key
    public string S2_l10n_key { get; protected set; }
    public UnityEngine.Vector2 V2 { get; protected set; }
    public UnityEngine.Vector3 V3 { get; protected set; }
    public UnityEngine.Vector4 V4 { get; protected set; }
    public int T1 { get; protected set; }
    public long T1_Millis => T1 * 1000L;

    public const int __ID__ = -370934083;
    public override int GetTypeId() => __ID__;

    public  void Resolve(Dictionary<string, object> _tables)
    {
        PostResolve();
    }

    public  void TranslateText(System.Func<string, string, string> translator)
    {
        S2 = translator(S2_l10n_key, S2);
    }

    public void Reload(DemoPrimitiveTypesTable reloadData)
    {
        X1 = reloadData.X1;
        X2 = reloadData.X2;
        X3 = reloadData.X3;
        X4 = reloadData.X4;
        X5 = reloadData.X5;
        X6 = reloadData.X6;
        X7 = reloadData.X7;
        S1 = reloadData.S1;
        S2 = reloadData.S2;
        V2 = reloadData.V2;
        V3 = reloadData.V3;
        V4 = reloadData.V4;
        T1 = reloadData.T1;
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
    
    partial void PostInit();
    partial void PostResolve();
}

}
