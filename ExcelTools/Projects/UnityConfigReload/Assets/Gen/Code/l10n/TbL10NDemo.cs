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

namespace cfg.l10n
{
   
public partial class TbL10NDemo
{
    private Dictionary<int, l10n.L10NDemo> _dataMap;
    private List<l10n.L10NDemo> _dataList;
    
    public TbL10NDemo(ByteBuf _buf)
    {
        _dataMap = new Dictionary<int, l10n.L10NDemo>();
        _dataList = new List<l10n.L10NDemo>();
        
        for(int n = _buf.ReadSize() ; n > 0 ; --n)
        {
            l10n.L10NDemo _v;
            _v = l10n.L10NDemo.DeserializeL10NDemo(_buf);
            _dataList.Add(_v);
            _dataMap.Add(_v.Id, _v);
        }
        PostInit();
    }

    public Dictionary<int, l10n.L10NDemo> DataMap => _dataMap;
    public List<l10n.L10NDemo> DataList => _dataList;

    public l10n.L10NDemo GetOrDefault(int key) => _dataMap.TryGetValue(key, out var v) ? v : null;
    public l10n.L10NDemo Get(int key) => _dataMap[key];
    public l10n.L10NDemo this[int key] => _dataMap[key];

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
        var reloadMap = new TbL10NDemo(_buf);
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