
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------
package cfg.role;

import bright.serialization.*;


public final class TbRoleLevelBonusCoefficient {
    private final java.util.HashMap<Integer, cfg.role.LevelBonus> _dataMap;
    private final java.util.ArrayList<cfg.role.LevelBonus> _dataList;
    
    public TbRoleLevelBonusCoefficient(ByteBuf _buf) {
        _dataMap = new java.util.HashMap<Integer, cfg.role.LevelBonus>();
        _dataList = new java.util.ArrayList<cfg.role.LevelBonus>();
        
        for(int n = _buf.readSize() ; n > 0 ; --n) {
            cfg.role.LevelBonus _v;
            _v = new cfg.role.LevelBonus(_buf);
            _dataList.add(_v);
            _dataMap.put(_v.id, _v);
        }
    }

    public java.util.HashMap<Integer, cfg.role.LevelBonus> getDataMap() { return _dataMap; }
    public java.util.ArrayList<cfg.role.LevelBonus> getDataList() { return _dataList; }

    public cfg.role.LevelBonus get(int key) { return _dataMap.get(key); }

    public void resolve(java.util.HashMap<String, Object> _tables) {
        for(cfg.role.LevelBonus v : _dataList) {
            v.resolve(_tables);
        }
    }
}