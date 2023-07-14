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



public final class WeightBonusInfo {
    public WeightBonusInfo(JsonObject __json__) { 
        bonus = cfg.bonus.Bonus.deserializeBonus(__json__.get("bonus").getAsJsonObject());
        weight = __json__.get("weight").getAsInt();
    }

    public WeightBonusInfo(cfg.bonus.Bonus bonus, int weight ) {
        this.bonus = bonus;
        this.weight = weight;
    }

    public static WeightBonusInfo deserializeWeightBonusInfo(JsonObject __json__) {
        return new WeightBonusInfo(__json__);
    }

    public final cfg.bonus.Bonus bonus;
    public final int weight;


    public void resolve(java.util.HashMap<String, Object> _tables) {
        if (bonus != null) {bonus.resolve(_tables);}
    }

    @Override
    public String toString() {
        return "{ "
        + "bonus:" + bonus + ","
        + "weight:" + weight + ","
        + "}";
    }
}
