
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------
package cfg.bonus;

import bright.serialization.*;



public final class WeightBonusInfo {
    public WeightBonusInfo(ByteBuf _buf) { 
        bonus = cfg.bonus.Bonus.deserializeBonus(_buf);
        weight = _buf.readInt();
    }

    public WeightBonusInfo(cfg.bonus.Bonus bonus, int weight ) {
        this.bonus = bonus;
        this.weight = weight;
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
