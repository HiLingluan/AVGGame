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

public sealed partial class TbDemoPrimitive
{
    private readonly Dictionary<int, test.DemoPrimitiveTypesTable> _dataMap;
    private readonly List<test.DemoPrimitiveTypesTable> _dataList;
    
    public TbDemoPrimitive(JsonElement _json)
    {
        _dataMap = new Dictionary<int, test.DemoPrimitiveTypesTable>();
        _dataList = new List<test.DemoPrimitiveTypesTable>();
        
        foreach(JsonElement _row in _json.EnumerateArray())
        {
            var _v = test.DemoPrimitiveTypesTable.DeserializeDemoPrimitiveTypesTable(_row);
            _dataList.Add(_v);
            _dataMap.Add(_v.X4, _v);
        }
        PostInit();
    }

    public Dictionary<int, test.DemoPrimitiveTypesTable> DataMap => _dataMap;
    public List<test.DemoPrimitiveTypesTable> DataList => _dataList;

    public test.DemoPrimitiveTypesTable GetOrDefault(int key) => _dataMap.TryGetValue(key, out var v) ? v : null;
    public test.DemoPrimitiveTypesTable Get(int key) => _dataMap[key];
    public test.DemoPrimitiveTypesTable this[int key] => _dataMap[key];

    public void Resolve(Dictionary<string, object> _tables)
    {
        foreach(var v in _dataList)
        {
            v.Resolve(_tables);
        }
        PostResolve();
    }

    public void TranslateText(System.Func<string, string, string> translator)
    {
        foreach(var v in _dataList)
        {
            v.TranslateText(translator);
        }
    }
    

    partial void PostInit();
    partial void PostResolve();
}

}