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

public sealed partial class MultiRowTitle :  Bright.Config.BeanBase 
{
    public MultiRowTitle(ByteBuf _buf) 
    {
        Id = _buf.ReadInt();
        Name = _buf.ReadString();
        X1 = test.H1.DeserializeH1(_buf);
        if(_buf.ReadBool()){ X20 = test.H2.DeserializeH2(_buf); } else { X20 = null; }
        {int n = System.Math.Min(_buf.ReadSize(), _buf.Size);X2 = new System.Collections.Generic.List<test.H2>(n);for(var i = 0 ; i < n ; i++) { test.H2 _e;  _e = test.H2.DeserializeH2(_buf); X2.Add(_e);}}
        {int n = System.Math.Min(_buf.ReadSize(), _buf.Size);X3 = new test.H2[n];for(var i = 0 ; i < n ; i++) { test.H2 _e;_e = test.H2.DeserializeH2(_buf); X3[i] = _e;}}
        {int n = System.Math.Min(_buf.ReadSize(), _buf.Size);X4 = new test.H2[n];for(var i = 0 ; i < n ; i++) { test.H2 _e;_e = test.H2.DeserializeH2(_buf); X4[i] = _e;}}
        PostInit();
    }

    public static MultiRowTitle DeserializeMultiRowTitle(ByteBuf _buf)
    {
        return new test.MultiRowTitle(_buf);
    }

    public int Id { get; protected set; }
    public string Name { get; protected set; }
    public test.H1 X1 { get; protected set; }
    public test.H2 X20 { get; protected set; }
    public System.Collections.Generic.List<test.H2> X2 { get; protected set; }
    public test.H2[] X3 { get; protected set; }
    public test.H2[] X4 { get; protected set; }

    public const int __ID__ = 540002427;
    public override int GetTypeId() => __ID__;

    public  void Resolve(Dictionary<string, object> _tables)
    {
        X1?.Resolve(_tables);
        X20?.Resolve(_tables);
        foreach(var _e in X2) { _e?.Resolve(_tables); }
        foreach(var _e in X3) { _e?.Resolve(_tables); }
        foreach(var _e in X4) { _e?.Resolve(_tables); }
        PostResolve();
    }

    public  void TranslateText(System.Func<string, string, string> translator)
    {
        X1?.TranslateText(translator);
        X20?.TranslateText(translator);
        foreach(var _e in X2) { _e?.TranslateText(translator); }
        foreach(var _e in X3) { _e?.TranslateText(translator); }
        foreach(var _e in X4) { _e?.TranslateText(translator); }
    }

    public void Reload(MultiRowTitle reloadData)
    {
        Id = reloadData.Id;
        Name = reloadData.Name;
        if(X1==null)
        {
            X1 = reloadData.X1;
        }else
        {
            if(X1.GetTypeId() == reloadData.X1.GetTypeId())
            {
                X1.Reload(reloadData.X1);
            }else
            {
                typeof(MultiRowTitle).GetProperty("X1").SetValue(this,reloadData.X1);
            }
        }
        if(X20==null)
        {
            X20 = reloadData.X20;
        }else
        {
            if(X20.GetTypeId() == reloadData.X20.GetTypeId())
            {
                X20.Reload(reloadData.X20);
            }else
            {
                typeof(MultiRowTitle).GetProperty("X20").SetValue(this,reloadData.X20);
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
                if(X2[i]!=null)
                {
                    X2[i].Reload(reloadData.X2[i]);
                }else
                {
                    X2[i] = reloadData.X2[i];
                }
            }
        }
        if(X3==null)
        {
            X3 = reloadData.X3;
        }else
        {
            if(X3.Length!=reloadData.X3.Length)
            {
                var newArray = new test.H2[reloadData.X3.Length];
                for(int i = 0; i<newArray.Length; i++)
                {
                    if(i<X3.Length)
                    {
                        newArray[i] = X3[i];
                    }
                }
                typeof(MultiRowTitle).GetProperty("X3").SetValue(this, newArray);
            }
                for(int i = 0; i<reloadData.X3.Length; i++)
                {
                    if(X3[i]!=null){
                        X3[i].Reload(reloadData.X3[i]);
                    }else{
                        X3[i] = reloadData.X3[i];
                    }
                }
        }
        if(X4==null)
        {
            X4 = reloadData.X4;
        }else
        {
            if(X4.Length!=reloadData.X4.Length)
            {
                var newArray = new test.H2[reloadData.X4.Length];
                for(int i = 0; i<newArray.Length; i++)
                {
                    if(i<X4.Length)
                    {
                        newArray[i] = X4[i];
                    }
                }
                typeof(MultiRowTitle).GetProperty("X4").SetValue(this, newArray);
            }
                for(int i = 0; i<reloadData.X4.Length; i++)
                {
                    if(X4[i]!=null){
                        X4[i].Reload(reloadData.X4[i]);
                    }else{
                        X4[i] = reloadData.X4[i];
                    }
                }
        }
    }

    public override string ToString()
    {
        return "{ "
        + "Id:" + Id + ","
        + "Name:" + Name + ","
        + "X1:" + X1 + ","
        + "X20:" + X20 + ","
        + "X2:" + Bright.Common.StringUtil.CollectionToString(X2) + ","
        + "X3:" + Bright.Common.StringUtil.CollectionToString(X3) + ","
        + "X4:" + Bright.Common.StringUtil.CollectionToString(X4) + ","
        + "}";
    }
    
    partial void PostInit();
    partial void PostResolve();
}

}
