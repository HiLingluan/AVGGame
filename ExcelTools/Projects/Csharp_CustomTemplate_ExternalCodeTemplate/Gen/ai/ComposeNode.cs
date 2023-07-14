
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



namespace cfg.ai
{

public abstract class ComposeNode :  ai.FlowNode 
{
    public ComposeNode(JsonElement _json)  : base(_json) 
    {
    }

    public ComposeNode(int id, string node_name, System.Collections.Generic.List<ai.Decorator> decorators, System.Collections.Generic.List<ai.Service> services )  : base(id,node_name,decorators,services) 
    {
    }

    public static ComposeNode DeserializeComposeNode(JsonElement _json)
    {
        switch (_json.GetProperty("__type__").GetString())
        {
            case "Sequence": return new ai.Sequence(_json);
            case "Selector": return new ai.Selector(_json);
            case "SimpleParallel": return new ai.SimpleParallel(_json);
            default: throw new SerializationException();
        }
    }



    public override void Resolve(Dictionary<string, object> _tables)
    {
        base.Resolve(_tables);
    }

    public override void TranslateText(System.Func<string, string, string> translator)
    {
        base.TranslateText(translator);
    }

    public override string ToString()
    {
        return "{ "
        + "Id:" + Id + ","
        + "NodeName:" + NodeName + ","
        + "Decorators:" + Bright.Common.StringUtil.CollectionToString(Decorators) + ","
        + "Services:" + Bright.Common.StringUtil.CollectionToString(Services) + ","
        + "}";
    }
    }
}
