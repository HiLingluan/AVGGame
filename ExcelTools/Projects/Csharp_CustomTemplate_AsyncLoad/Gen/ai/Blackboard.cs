
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

public sealed class Blackboard :  Bright.Config.BeanBase 
{
    public Blackboard(JsonElement _json) 
    {
        Name = _json.GetProperty("name").GetString();
        Desc = _json.GetProperty("desc").GetString();
        ParentName = _json.GetProperty("parent_name").GetString();
        { var _json0 = _json.GetProperty("keys"); Keys = new System.Collections.Generic.List<ai.BlackboardKey>(_json0.GetArrayLength()); foreach(JsonElement __e in _json0.EnumerateArray()) { ai.BlackboardKey __v;  __v =  ai.BlackboardKey.DeserializeBlackboardKey(__e);  Keys.Add(__v); }   }
    }

    public Blackboard(string name, string desc, string parent_name, System.Collections.Generic.List<ai.BlackboardKey> keys ) 
    {
        this.Name = name;
        this.Desc = desc;
        this.ParentName = parent_name;
        this.Keys = keys;
    }

    public static Blackboard DeserializeBlackboard(JsonElement _json)
    {
        return new ai.Blackboard(_json);
    }

    public string Name { get; private set; }
    public string Desc { get; private set; }
    public string ParentName { get; private set; }
    public ai.Blackboard ParentName_Ref { get; private set; }
    public System.Collections.Generic.List<ai.BlackboardKey> Keys { get; private set; }

    public const int __ID__ = 1576193005;
    public override int GetTypeId() => __ID__;

    public  void Resolve(Dictionary<string, object> _tables)
    {
        this.ParentName_Ref = (_tables["ai.TbBlackboard"] as ai.TbBlackboard).GetOrDefault(ParentName);
        foreach(var _e in Keys) { _e?.Resolve(_tables); }
    }

    public  void TranslateText(System.Func<string, string, string> translator)
    {
        foreach(var _e in Keys) { _e?.TranslateText(translator); }
    }

    public override string ToString()
    {
        return "{ "
        + "Name:" + Name + ","
        + "Desc:" + Desc + ","
        + "ParentName:" + ParentName + ","
        + "Keys:" + Bright.Common.StringUtil.CollectionToString(Keys) + ","
        + "}";
    }
    }
}
