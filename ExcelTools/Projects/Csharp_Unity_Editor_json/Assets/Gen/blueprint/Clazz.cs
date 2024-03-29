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



namespace editor.cfg.blueprint
{

public abstract partial class Clazz :  Bright.Config.EditorBeanBase 
{
    public Clazz()
    {
            Name = "";
            Desc = "";
            Parents = new System.Collections.Generic.List<blueprint.Clazz>();
            Methods = new System.Collections.Generic.List<blueprint.Method>();
    }


    public static Clazz LoadJsonClazz(SimpleJSON.JSONNode _json)
    {
        string type = _json["$type"];
        Clazz obj;
        switch (type)
        {
            case "blueprint.Interface":   
            case "Interface":obj = new blueprint.Interface(); break;
            case "blueprint.NormalClazz":   
            case "NormalClazz":obj = new blueprint.NormalClazz(); break;
            case "blueprint.EnumClazz":   
            case "EnumClazz":obj = new blueprint.EnumClazz(); break;
            default: throw new SerializationException();
        }
        obj.LoadJson((SimpleJSON.JSONObject)_json);
        return obj;
    }
        
    public static void SaveJsonClazz(Clazz _obj, SimpleJSON.JSONNode _json)
    {
        _json["$type"] = _obj.GetType().Name;
        _obj.SaveJson((SimpleJSON.JSONObject)_json);
    }

    public string Name { get; set; }

    public string Desc { get; set; }

    public System.Collections.Generic.List<blueprint.Clazz> Parents { get; set; }

    public System.Collections.Generic.List<blueprint.Method> Methods { get; set; }

}
}
