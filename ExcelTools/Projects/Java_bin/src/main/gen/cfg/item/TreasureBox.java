
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------
package cfg.item;

import bright.serialization.*;



public final class TreasureBox extends cfg.item.ItemExtra {
    public TreasureBox(ByteBuf _buf) { 
        super(_buf);
        if(_buf.readBool()){ keyItemId = _buf.readInt(); } else { keyItemId = null; }
        openLevel = new cfg.condition.MinLevel(_buf);
        useOnObtain = _buf.readBool();
        {int n = Math.min(_buf.readSize(), _buf.size());dropIds = new java.util.ArrayList<Integer>(n);for(int i = 0 ; i < n ; i++) { Integer _e;  _e = _buf.readInt(); dropIds.add(_e);}}
        {int n = Math.min(_buf.readSize(), _buf.size());chooseList = new java.util.ArrayList<cfg.item.ChooseOneBonus>(n);for(int i = 0 ; i < n ; i++) { cfg.item.ChooseOneBonus _e;  _e = new cfg.item.ChooseOneBonus(_buf); chooseList.add(_e);}}
    }

    public TreasureBox(int id, Integer key_item_id, cfg.condition.MinLevel open_level, boolean use_on_obtain, java.util.ArrayList<Integer> drop_ids, java.util.ArrayList<cfg.item.ChooseOneBonus> choose_list ) {
        super(id);
        this.keyItemId = key_item_id;
        this.openLevel = open_level;
        this.useOnObtain = use_on_obtain;
        this.dropIds = drop_ids;
        this.chooseList = choose_list;
    }


    public final Integer keyItemId;
    public final cfg.condition.MinLevel openLevel;
    public final boolean useOnObtain;
    public final java.util.ArrayList<Integer> dropIds;
    public final java.util.ArrayList<cfg.item.ChooseOneBonus> chooseList;

    public static final int __ID__ = 1494222369;

    @Override
    public int getTypeId() { return __ID__; }

    @Override
    public void resolve(java.util.HashMap<String, Object> _tables) {
        super.resolve(_tables);
        if (openLevel != null) {openLevel.resolve(_tables);}
        for(cfg.item.ChooseOneBonus _e : chooseList) { if (_e != null) _e.resolve(_tables); }
    }

    @Override
    public String toString() {
        return "{ "
        + "id:" + id + ","
        + "keyItemId:" + keyItemId + ","
        + "openLevel:" + openLevel + ","
        + "useOnObtain:" + useOnObtain + ","
        + "dropIds:" + dropIds + ","
        + "chooseList:" + chooseList + ","
        + "}";
    }
}