
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------
package cfg.bonus;

import bright.serialization.*;



public final class ProbabilityItemInfo {
    public ProbabilityItemInfo(ByteBuf _buf) { 
        itemId = _buf.readInt();
        num = _buf.readInt();
        probability = _buf.readFloat();
    }

    public ProbabilityItemInfo(int item_id, int num, float probability ) {
        this.itemId = item_id;
        this.num = num;
        this.probability = probability;
    }


    public final int itemId;
    public cfg.item.Item itemId_Ref;
    public final int num;
    public final float probability;


    public void resolve(java.util.HashMap<String, Object> _tables) {
        this.itemId_Ref = ((cfg.item.TbItem)_tables.get("item.TbItem")).get(itemId);
    }

    @Override
    public String toString() {
        return "{ "
        + "itemId:" + itemId + ","
        + "num:" + num + ","
        + "probability:" + probability + ","
        + "}";
    }
}
