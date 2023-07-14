
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------
package cfg.ai;

import bright.serialization.*;



public final class IsAtLocation extends cfg.ai.Decorator {
    public IsAtLocation(ByteBuf _buf) { 
        super(_buf);
        acceptableRadius = _buf.readFloat();
        keyboardKey = _buf.readString();
        inverseCondition = _buf.readBool();
    }

    public IsAtLocation(int id, String node_name, cfg.ai.EFlowAbortMode flow_abort_mode, float acceptable_radius, String keyboard_key, boolean inverse_condition ) {
        super(id, node_name, flow_abort_mode);
        this.acceptableRadius = acceptable_radius;
        this.keyboardKey = keyboard_key;
        this.inverseCondition = inverse_condition;
    }


    public final float acceptableRadius;
    public final String keyboardKey;
    public final boolean inverseCondition;

    public static final int __ID__ = 1255972344;

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
        + "acceptableRadius:" + acceptableRadius + ","
        + "keyboardKey:" + keyboardKey + ","
        + "inverseCondition:" + inverseCondition + ","
        + "}";
    }
}