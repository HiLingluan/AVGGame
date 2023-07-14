//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------
using Bright.Serialization;
using System.Collections.Generic;
using System.Linq;

namespace cfg.item
{
   
public partial class TbItemFunc
{
    private Dictionary<item.EMinorType, item.ItemFunction> _dataMap;
    private List<item.ItemFunction> _dataList;
    
    public TbItemFunc(ByteBuf _buf)
    {
        _dataMap = new Dictionary<item.EMinorType, item.ItemFunction>();
        _dataList = new List<item.ItemFunction>();
        
        for(int n = _buf.ReadSize() ; n > 0 ; --n)
        {
            item.ItemFunction _v;
            _v = item.ItemFunction.DeserializeItemFunction(_buf);
            _dataList.Add(_v);
            _dataMap.Add(_v.MinorType, _v);
        }
        PostInit();
    }

    public Dictionary<item.EMinorType, item.ItemFunction> DataMap => _dataMap;
    public List<item.ItemFunction> DataList => _dataList;

    public item.ItemFunction GetOrDefault(item.EMinorType key) => _dataMap.TryGetValue(key, out var v) ? v : null;
    public item.ItemFunction Get(item.EMinorType key) => _dataMap[key];
    public item.ItemFunction this[item.EMinorType key] => _dataMap[key];

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

    public void Reload(ByteBuf _buf)
    {
        var reloadMap = new TbItemFunc(_buf);
        foreach (var rowDataKey in this._dataMap.Keys.ToList())
        {
            if(!reloadMap._dataMap.ContainsKey(rowDataKey))
            {
                this._dataList.Remove(this._dataMap[rowDataKey]);
                this._dataMap.Remove(rowDataKey);
            }
        }

        foreach (var reloadData in reloadMap._dataMap)
        {
            if (this._dataMap.ContainsKey(reloadData.Key))
            {
                this._dataMap[reloadData.Key].Reload(reloadData.Value);
            }
            else
            {
                this._dataMap.Add(reloadData.Key,reloadData.Value);
                this._dataList.Add(reloadData.Value);
            }
        }
    }
    
    partial void PostInit();
    partial void PostResolve();
}

}