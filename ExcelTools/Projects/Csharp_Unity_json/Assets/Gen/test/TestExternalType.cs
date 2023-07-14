//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------
using Bright.Serialization;
using System.Collections.Generic;
using SimpleJSON;



namespace cfg.test
{

public sealed partial class TestExternalType :  Bright.Config.BeanBase 
{
    public TestExternalType(JSONNode _json) 
    {
        { if(!_json["id"].IsNumber) { throw new SerializationException(); }  Id = _json["id"]; }
        { if(!_json["audio_type"].IsNumber) { throw new SerializationException(); }  AudioType = (test.AudioType)_json["audio_type"].AsInt; }
        { if(!_json["color"].IsObject) { throw new SerializationException(); }  Color = test.Color.DeserializeColor(_json["color"]); }
        PostInit();
    }

    public TestExternalType(int id, test.AudioType audio_type, test.Color color ) 
    {
        this.Id = id;
        this.AudioType = audio_type;
        this.Color = color;
        PostInit();
    }

    public static TestExternalType DeserializeTestExternalType(JSONNode _json)
    {
        return new test.TestExternalType(_json);
    }

    public int Id { get; private set; }
    public test.AudioType AudioType { get; private set; }
    public test.Color Color { get; private set; }

    public const int __ID__ = -990826157;
    public override int GetTypeId() => __ID__;

    public  void Resolve(Dictionary<string, object> _tables)
    {
        Color?.Resolve(_tables);
        PostResolve();
    }

    public  void TranslateText(System.Func<string, string, string> translator)
    {
        Color?.TranslateText(translator);
    }

    public override string ToString()
    {
        return "{ "
        + "Id:" + Id + ","
        + "AudioType:" + AudioType + ","
        + "Color:" + Color + ","
        + "}";
    }
    
    partial void PostInit();
    partial void PostResolve();
}
}
