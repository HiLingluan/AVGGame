
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------
package cfg.test;

import bright.serialization.*;


public final class TbTestDesc {
    private final java.util.HashMap<Integer, cfg.test.TestDesc> _dataMap;
    private final java.util.ArrayList<cfg.test.TestDesc> _dataList;
    
    public TbTestDesc(ByteBuf _buf) {
        _dataMap = new java.util.HashMap<Integer, cfg.test.TestDesc>();
        _dataList = new java.util.ArrayList<cfg.test.TestDesc>();
        
        for(int n = _buf.readSize() ; n > 0 ; --n) {
            cfg.test.TestDesc _v;
            _v = new cfg.test.TestDesc(_buf);
            _dataList.add(_v);
            _dataMap.put(_v.id, _v);
        }
    }

    public java.util.HashMap<Integer, cfg.test.TestDesc> getDataMap() { return _dataMap; }
    public java.util.ArrayList<cfg.test.TestDesc> getDataList() { return _dataList; }

    public cfg.test.TestDesc get(int key) { return _dataMap.get(key); }

    public void resolve(java.util.HashMap<String, Object> _tables) {
        for(cfg.test.TestDesc v : _dataList) {
            v.resolve(_tables);
        }
    }
}