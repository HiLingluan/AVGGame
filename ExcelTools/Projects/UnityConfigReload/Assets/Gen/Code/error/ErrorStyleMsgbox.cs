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



namespace cfg.error
{

public sealed partial class ErrorStyleMsgbox :  error.ErrorStyle 
{
    public ErrorStyleMsgbox(ByteBuf _buf)  : base(_buf) 
    {
        BtnName = _buf.ReadString();
        Operation = (error.EOperation)_buf.ReadInt();
        PostInit();
    }

    public static ErrorStyleMsgbox DeserializeErrorStyleMsgbox(ByteBuf _buf)
    {
        return new error.ErrorStyleMsgbox(_buf);
    }

    public string BtnName { get; protected set; }
    public error.EOperation Operation { get; protected set; }

    public const int __ID__ = -1920482343;
    public override int GetTypeId() => __ID__;

    public override void Resolve(Dictionary<string, object> _tables)
    {
        base.Resolve(_tables);
        PostResolve();
    }

    public override void TranslateText(System.Func<string, string, string> translator)
    {
        base.TranslateText(translator);
    }

    public void Reload(ErrorStyleMsgbox reloadData)
    {
        BtnName = reloadData.BtnName;
        Operation = reloadData.Operation;
    }

    public override string ToString()
    {
        return "{ "
        + "BtnName:" + BtnName + ","
        + "Operation:" + Operation + ","
        + "}";
    }
    
    partial void PostInit();
    partial void PostResolve();
}

}
