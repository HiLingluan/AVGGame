
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

public sealed class Test3 :  Bright.Config.BeanBase 
{
    public Test3(JsonElement _json) 
    {
        X = _json.GetProperty("x").GetInt32();
        Y = _json.GetProperty("y").GetInt32();
    }

    public Test3(int x, int y ) 
    {
        this.X = x;
        this.Y = y;
    }

    public static Test3 DeserializeTest3(JsonElement _json)
    {
        return new test.Test3(_json);
    }

    public int X { get; private set; }
    public int Y { get; private set; }

    public const int __ID__ = 638540133;
    public override int GetTypeId() => __ID__;

    public  void Resolve(Dictionary<string, object> _tables)
    {
    }

    public  void TranslateText(System.Func<string, string, string> translator)
    {
    }

    public override string ToString()
    {
        return "{ "
        + "X:" + X + ","
        + "Y:" + Y + ","
        + "}";
    }
    }
}
