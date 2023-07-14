//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------
package cfg.ai;

import com.google.gson.JsonElement;
import com.google.gson.JsonObject;



public final class UeCooldown extends cfg.ai.Decorator {
    public UeCooldown(JsonObject __json__) { 
        super(__json__);
        cooldownTime = __json__.get("cooldown_time").getAsFloat();
    }

    public UeCooldown(int id, String node_name, cfg.ai.EFlowAbortMode flow_abort_mode, float cooldown_time ) {
        super(id, node_name, flow_abort_mode);
        this.cooldownTime = cooldown_time;
    }

    public static UeCooldown deserializeUeCooldown(JsonObject __json__) {
        return new UeCooldown(__json__);
    }

    public final float cooldownTime;

    public static final int __ID__ = -951439423;

    @Override
    public int getTypeId() { return __ID__; }

    @Override
    public void resolve(java.util.HashMap<String, Object> _tables) {
        super.resolve(_tables);
    }

    @Override
    public String toString() {
        return "{ "
        + "id:" + id + ","
        + "nodeName:" + nodeName + ","
        + "flowAbortMode:" + flowAbortMode + ","
        + "cooldownTime:" + cooldownTime + ","
        + "}";
    }
}