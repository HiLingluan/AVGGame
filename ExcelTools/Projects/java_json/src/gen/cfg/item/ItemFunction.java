//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------
package cfg.item;

import com.google.gson.JsonElement;
import com.google.gson.JsonObject;



public final class ItemFunction {
    public ItemFunction(JsonObject __json__) { 
        minorType = cfg.item.EMinorType.valueOf(__json__.get("minor_type").getAsInt());
        funcType = cfg.item.EItemFunctionType.valueOf(__json__.get("func_type").getAsInt());
        method = __json__.get("method").getAsString();
        closeBagUi = __json__.get("close_bag_ui").getAsBoolean();
    }

    public ItemFunction(cfg.item.EMinorType minor_type, cfg.item.EItemFunctionType func_type, String method, boolean close_bag_ui ) {
        this.minorType = minor_type;
        this.funcType = func_type;
        this.method = method;
        this.closeBagUi = close_bag_ui;
    }

    public static ItemFunction deserializeItemFunction(JsonObject __json__) {
        return new ItemFunction(__json__);
    }

    public final cfg.item.EMinorType minorType;
    public final cfg.item.EItemFunctionType funcType;
    public final String method;
    public final boolean closeBagUi;


    public void resolve(java.util.HashMap<String, Object> _tables) {
    }

    @Override
    public String toString() {
        return "{ "
        + "minorType:" + minorType + ","
        + "funcType:" + funcType + ","
        + "method:" + method + ","
        + "closeBagUi:" + closeBagUi + ","
        + "}";
    }
}