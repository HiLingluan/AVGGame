//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------
package cfg.test;

import com.google.gson.JsonElement;
import com.google.gson.JsonObject;



public final class CompositeJsonTable3 {
    public CompositeJsonTable3(JsonObject __json__) { 
        a = __json__.get("a").getAsInt();
        b = __json__.get("b").getAsInt();
    }

    public CompositeJsonTable3(int a, int b ) {
        this.a = a;
        this.b = b;
    }

    public static CompositeJsonTable3 deserializeCompositeJsonTable3(JsonObject __json__) {
        return new CompositeJsonTable3(__json__);
    }

    public final int a;
    public final int b;


    public void resolve(java.util.HashMap<String, Object> _tables) {
    }

    @Override
    public String toString() {
        return "{ "
        + "a:" + a + ","
        + "b:" + b + ","
        + "}";
    }
}
