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

public sealed partial class DateTimeRange :  Bright.Config.BeanBase 
{
    public DateTimeRange(JSONNode _json) 
    {
        { if(!_json["start_time"].IsNumber) { throw new SerializationException(); }  StartTime = _json["start_time"]; }
        { if(!_json["end_time"].IsNumber) { throw new SerializationException(); }  EndTime = _json["end_time"]; }
        PostInit();
    }

    public DateTimeRange(int start_time, int end_time ) 
    {
        this.StartTime = start_time;
        this.EndTime = end_time;
        PostInit();
    }

    public static DateTimeRange DeserializeDateTimeRange(JSONNode _json)
    {
        return new test.DateTimeRange(_json);
    }

    public int StartTime { get; private set; }
    public long StartTime_Millis => StartTime * 1000L;
    public int EndTime { get; private set; }
    public long EndTime_Millis => EndTime * 1000L;

    public const int __ID__ = 495315430;
    public override int GetTypeId() => __ID__;

    public  void Resolve(Dictionary<string, object> _tables)
    {
        PostResolve();
    }

    public  void TranslateText(System.Func<string, string, string> translator)
    {
    }

    public override string ToString()
    {
        return "{ "
        + "StartTime:" + StartTime + ","
        + "EndTime:" + EndTime + ","
        + "}";
    }
    
    partial void PostInit();
    partial void PostResolve();
}
}
