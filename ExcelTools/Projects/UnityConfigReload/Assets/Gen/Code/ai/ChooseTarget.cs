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



namespace cfg.ai
{

public sealed partial class ChooseTarget :  ai.Service 
{
    public ChooseTarget(ByteBuf _buf)  : base(_buf) 
    {
        ResultTargetKey = _buf.ReadString();
        PostInit();
    }

    public static ChooseTarget DeserializeChooseTarget(ByteBuf _buf)
    {
        return new ai.ChooseTarget(_buf);
    }

    public string ResultTargetKey { get; protected set; }

    public const int __ID__ = 1601247918;
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

    public void Reload(ChooseTarget reloadData)
    {
        Id = reloadData.Id;
        NodeName = reloadData.NodeName;
        ResultTargetKey = reloadData.ResultTargetKey;
    }

    public override string ToString()
    {
        return "{ "
        + "Id:" + Id + ","
        + "NodeName:" + NodeName + ","
        + "ResultTargetKey:" + ResultTargetKey + ","
        + "}";
    }
    
    partial void PostInit();
    partial void PostResolve();
}

}
