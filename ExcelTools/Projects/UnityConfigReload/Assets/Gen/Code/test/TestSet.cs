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

public sealed partial class TestSet :  Bright.Config.BeanBase 
{
    public TestSet(ByteBuf _buf) 
    {
        Id = _buf.ReadInt();
        X0 = _buf.ReadString();
        {int n = System.Math.Min(_buf.ReadSize(), _buf.Size);X1 = new System.Collections.Generic.List<int>(n);for(var i = 0 ; i < n ; i++) { int _e;  _e = _buf.ReadInt(); X1.Add(_e);}}
        {int n = System.Math.Min(_buf.ReadSize(), _buf.Size);X2 = new System.Collections.Generic.List<long>(n);for(var i = 0 ; i < n ; i++) { long _e;  _e = _buf.ReadLong(); X2.Add(_e);}}
        {int n = System.Math.Min(_buf.ReadSize(), _buf.Size);X3 = new System.Collections.Generic.List<string>(n);for(var i = 0 ; i < n ; i++) { string _e;  _e = _buf.ReadString(); X3.Add(_e);}}
        {int n = System.Math.Min(_buf.ReadSize(), _buf.Size);X4 = new System.Collections.Generic.List<test.DemoEnum>(n);for(var i = 0 ; i < n ; i++) { test.DemoEnum _e;  _e = (test.DemoEnum)_buf.ReadInt(); X4.Add(_e);}}
        PostInit();
    }

    public static TestSet DeserializeTestSet(ByteBuf _buf)
    {
        return new test.TestSet(_buf);
    }

    public int Id { get; protected set; }
    public string X0 { get; protected set; }
    public System.Collections.Generic.List<int> X1 { get; protected set; }
    public System.Collections.Generic.List<long> X2 { get; protected set; }
    public System.Collections.Generic.List<string> X3 { get; protected set; }
    public System.Collections.Generic.List<test.DemoEnum> X4 { get; protected set; }

    public const int __ID__ = -543221516;
    public override int GetTypeId() => __ID__;

    public  void Resolve(Dictionary<string, object> _tables)
    {
        PostResolve();
    }

    public  void TranslateText(System.Func<string, string, string> translator)
    {
    }

    public void Reload(TestSet reloadData)
    {
        Id = reloadData.Id;
        X0 = reloadData.X0;
        if(X1==null)
        {
            X1 = reloadData.X1;
        }else
        {
            X1.Capacity = reloadData.X1.Count;
            for (int i = 0; i < reloadData.X1.Count; i++)
            {
                X1[i] = reloadData.X1[i];
            }
        }
        if(X2==null)
        {
            X2 = reloadData.X2;
        }else
        {
            X2.Capacity = reloadData.X2.Count;
            for (int i = 0; i < reloadData.X2.Count; i++)
            {
                X2[i] = reloadData.X2[i];
            }
        }
        if(X3==null)
        {
            X3 = reloadData.X3;
        }else
        {
            X3.Capacity = reloadData.X3.Count;
            for (int i = 0; i < reloadData.X3.Count; i++)
            {
                X3[i] = reloadData.X3[i];
            }
        }
        if(X4==null)
        {
            X4 = reloadData.X4;
        }else
        {
            X4.Capacity = reloadData.X4.Count;
            for (int i = 0; i < reloadData.X4.Count; i++)
            {
                X4[i] = reloadData.X4[i];
            }
        }
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
