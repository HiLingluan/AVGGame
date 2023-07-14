//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------
package cfg.cost;

import com.google.gson.JsonElement;
import com.google.gson.JsonObject;



public final class CostItem extends cfg.cost.Cost {
    public CostItem(JsonObject __json__) { 
        super(__json__);
        itemId = __json__.get("item_id").getAsInt();
        amount = __json__.get("amount").getAsInt();
    }

    public CostItem(int item_id, int amount ) {
        super();
        this.itemId = item_id;
        this.amount = amount;
    }

    public static CostItem deserializeCostItem(JsonObject __json__) {
        return new CostItem(__json__);
    }

    public final int itemId;
    public cfg.item.Item itemId_Ref;
    public final int amount;

    public static final int __ID__ = -1249440351;

    @Override
    public int getTypeId() { return __ID__; }

    @Override
    public void resolve(java.util.HashMap<String, Object> _tables) {
        super.resolve(_tables);
        this.itemId_Ref = ((cfg.item.TbItem)_tables.get("item.TbItem")).get(itemId);
    }

    @Override
    public String toString() {
        return "{ "
        + "itemId:" + itemId + ","
        + "amount:" + amount + ","
        + "}";
    }
}