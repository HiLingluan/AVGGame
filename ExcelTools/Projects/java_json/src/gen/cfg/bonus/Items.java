//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------
package cfg.bonus;

import com.google.gson.JsonElement;
import com.google.gson.JsonObject;



public final class Items extends cfg.bonus.Bonus {
    public Items(JsonObject __json__) { 
        super(__json__);
        { com.google.gson.JsonArray _json0_ = __json__.get("item_list").getAsJsonArray(); int _n = _json0_.size(); itemList = new cfg.bonus.Item[_n]; int _index=0; for(JsonElement __e : _json0_) { cfg.bonus.Item __v;  __v = new cfg.bonus.Item(__e.getAsJsonObject());  itemList[_index++] = __v; }   }
    }

    public Items(cfg.bonus.Item[] item_list ) {
        super();
        this.itemList = item_list;
    }

    public static Items deserializeItems(JsonObject __json__) {
        return new Items(__json__);
    }

    public final cfg.bonus.Item[] itemList;

    public static final int __ID__ = 819736849;

    @Override
    public int getTypeId() { return __ID__; }

    @Override
    public void resolve(java.util.HashMap<String, Object> _tables) {
        super.resolve(_tables);
        for(cfg.bonus.Item _e : itemList) { if (_e != null) _e.resolve(_tables); }
    }

    @Override
    public String toString() {
        return "{ "
        + "itemList:" + itemList + ","
        + "}";
    }
}
