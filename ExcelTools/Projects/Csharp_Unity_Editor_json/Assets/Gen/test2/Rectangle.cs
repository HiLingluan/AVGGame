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



namespace editor.cfg.test2
{

/// <summary>
/// 矩形
/// </summary>
public sealed partial class Rectangle :  test.Shape 
{
    public Rectangle()
    {
    }

    public override void LoadJson(SimpleJSON.JSONObject _json)
    {
        { 
            var _fieldJson = _json["width"];
            if (_fieldJson != null)
            {
                if(!_fieldJson.IsNumber) { throw new SerializationException(); }  Width = _fieldJson;
            }
        }
        
        { 
            var _fieldJson = _json["height"];
            if (_fieldJson != null)
            {
                if(!_fieldJson.IsNumber) { throw new SerializationException(); }  Height = _fieldJson;
            }
        }
        
    }

    public override void SaveJson(SimpleJSON.JSONObject _json)
    {
        _json["$type"] = "test2.Rectangle";
        {
            _json["width"] = new JSONNumber(Width);
        }
        {
            _json["height"] = new JSONNumber(Height);
        }
    }

    public static Rectangle LoadJsonRectangle(SimpleJSON.JSONNode _json)
    {
        Rectangle obj = new test2.Rectangle();
        obj.LoadJson((SimpleJSON.JSONObject)_json);
        return obj;
    }
        
    public static void SaveJsonRectangle(Rectangle _obj, SimpleJSON.JSONNode _json)
    {
        _obj.SaveJson((SimpleJSON.JSONObject)_json);
    }

    /// <summary>
    /// 宽度
    /// </summary>
    public float Width { get; set; }

    /// <summary>
    /// 高度
    /// </summary>
    public float Height { get; set; }

}
}
