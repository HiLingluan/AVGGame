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



namespace editor.cfg.item
{

public sealed partial class Clothes :  item.ItemExtra 
{
    public Clothes()
    {
    }

    public override void LoadJson(SimpleJSON.JSONObject _json)
    {
        { 
            var _fieldJson = _json["id"];
            if (_fieldJson != null)
            {
                if(!_fieldJson.IsNumber) { throw new SerializationException(); }  Id = _fieldJson;
            }
        }
        
        { 
            var _fieldJson = _json["attack"];
            if (_fieldJson != null)
            {
                if(!_fieldJson.IsNumber) { throw new SerializationException(); }  Attack = _fieldJson;
            }
        }
        
        { 
            var _fieldJson = _json["hp"];
            if (_fieldJson != null)
            {
                if(!_fieldJson.IsNumber) { throw new SerializationException(); }  Hp = _fieldJson;
            }
        }
        
        { 
            var _fieldJson = _json["energy_limit"];
            if (_fieldJson != null)
            {
                if(!_fieldJson.IsNumber) { throw new SerializationException(); }  EnergyLimit = _fieldJson;
            }
        }
        
        { 
            var _fieldJson = _json["energy_resume"];
            if (_fieldJson != null)
            {
                if(!_fieldJson.IsNumber) { throw new SerializationException(); }  EnergyResume = _fieldJson;
            }
        }
        
    }

    public override void SaveJson(SimpleJSON.JSONObject _json)
    {
        _json["$type"] = "item.Clothes";
        {
            _json["id"] = new JSONNumber(Id);
        }
        {
            _json["attack"] = new JSONNumber(Attack);
        }
        {
            _json["hp"] = new JSONNumber(Hp);
        }
        {
            _json["energy_limit"] = new JSONNumber(EnergyLimit);
        }
        {
            _json["energy_resume"] = new JSONNumber(EnergyResume);
        }
    }

    public static Clothes LoadJsonClothes(SimpleJSON.JSONNode _json)
    {
        Clothes obj = new item.Clothes();
        obj.LoadJson((SimpleJSON.JSONObject)_json);
        return obj;
    }
        
    public static void SaveJsonClothes(Clothes _obj, SimpleJSON.JSONNode _json)
    {
        _obj.SaveJson((SimpleJSON.JSONObject)_json);
    }

    public int Attack { get; set; }

    public long Hp { get; set; }

    public int EnergyLimit { get; set; }

    public int EnergyResume { get; set; }

}
}
