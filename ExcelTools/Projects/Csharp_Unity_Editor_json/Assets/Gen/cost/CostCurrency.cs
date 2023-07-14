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



namespace editor.cfg.cost
{

public sealed partial class CostCurrency :  cost.Cost 
{
    public CostCurrency()
    {
            Type = item.ECurrencyType.DIAMOND;
    }

    public override void LoadJson(SimpleJSON.JSONObject _json)
    {
        { 
            var _fieldJson = _json["type"];
            if (_fieldJson != null)
            {
                if(_fieldJson.IsString) { Type = (item.ECurrencyType)System.Enum.Parse(typeof(item.ECurrencyType), _fieldJson); } else if(_fieldJson.IsNumber) { Type = (item.ECurrencyType)(int)_fieldJson; } else { throw new SerializationException(); }  
            }
        }
        
        { 
            var _fieldJson = _json["num"];
            if (_fieldJson != null)
            {
                if(!_fieldJson.IsNumber) { throw new SerializationException(); }  Num = _fieldJson;
            }
        }
        
    }

    public override void SaveJson(SimpleJSON.JSONObject _json)
    {
        _json["$type"] = "cost.CostCurrency";
        {
            _json["type"] = new JSONNumber((int)Type);
        }
        {
            _json["num"] = new JSONNumber(Num);
        }
    }

    public static CostCurrency LoadJsonCostCurrency(SimpleJSON.JSONNode _json)
    {
        CostCurrency obj = new cost.CostCurrency();
        obj.LoadJson((SimpleJSON.JSONObject)_json);
        return obj;
    }
        
    public static void SaveJsonCostCurrency(CostCurrency _obj, SimpleJSON.JSONNode _json)
    {
        _obj.SaveJson((SimpleJSON.JSONObject)_json);
    }

    public item.ECurrencyType Type { get; set; }

    public int Num { get; set; }

}
}
