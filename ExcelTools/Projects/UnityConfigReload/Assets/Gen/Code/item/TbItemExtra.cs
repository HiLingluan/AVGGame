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
   
public partial class TbItemExtra
{
    private Dictionary<int, item.ItemExtra> _dataMap;
    private List<item.ItemExtra> _dataList;
    
    public TbItemExtra(ByteBuf _buf)
    {
        _dataMap = new Dictionary<int, item.ItemExtra>();
        _dataList = new List<item.ItemExtra>();
        
        for(int n = _buf.ReadSize() ; n > 0 ; --n)
        {
            item.ItemExtra _v;
            _v = item.ItemExtra.DeserializeItemExtra(_buf);
            _dataList.Add(_v);
            _dataMap.Add(_v.Id, _v);
        }
        PostInit();
    }

    public Dictionary<int, item.ItemExtra> DataMap => _dataMap;
    public List<item.ItemExtra> DataList => _dataList;

    public T GetOrDefaultAs<T>(int key) where T : item.ItemExtra => _dataMap.TryGetValue(key, out var v) ? (T)v : null;
    public T GetAs<T>(int key) where T : item.ItemExtra => (T)_dataMap[key];
    public item.ItemExtra GetOrDefault(int key) => _dataMap.TryGetValue(key, out var v) ? v : null;
    public item.ItemExtra Get(int key) => _dataMap[key];
    public item.ItemExtra this[int key] => _dataMap[key];

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
        var reloadMap = new TbItemExtra(_buf);
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