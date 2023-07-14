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



public final class Selector extends cfg.ai.ComposeNode {
    public Selector(JsonObject __json__) { 
        super(__json__);
        { com.google.gson.JsonArray _json0_ = __json__.get("children").getAsJsonArray(); children = new java.util.ArrayList<cfg.ai.FlowNode>(_json0_.size()); for(JsonElement __e : _json0_) { cfg.ai.FlowNode __v;  __v = cfg.ai.FlowNode.deserializeFlowNode(__e.getAsJsonObject());  children.add(__v); }   }
    }

    public Selector(int id, String node_name, java.util.ArrayList<cfg.ai.Decorator> decorators, java.util.ArrayList<cfg.ai.Service> services, java.util.ArrayList<cfg.ai.FlowNode> children ) {
        super(id, node_name, decorators, services);
        this.children = children;
    }

    public static Selector deserializeSelector(JsonObject __json__) {
        return new Selector(__json__);
    }

    public final java.util.ArrayList<cfg.ai.FlowNode> children;

    public static final int __ID__ = -1946981627;

    @Override
    public int getTypeId() { return __ID__; }

    @Override
    public void resolve(java.util.HashMap<String, Object> _tables) {
        super.resolve(_tables);
        for(cfg.ai.FlowNode _e : children) { if (_e != null) _e.resolve(_tables); }
    }

    @Override
    public String toString() {
        return "{ "
        + "id:" + id + ","
        + "nodeName:" + nodeName + ","
        + "decorators:" + decorators + ","
        + "services:" + services + ","
        + "children:" + children + ","
        + "}";
    }
}