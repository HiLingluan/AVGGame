
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------
package cfg.test;

import bright.serialization.*;



public final class Test3 {
    public Test3(ByteBuf _buf) { 
        x = _buf.readInt();
        y = _buf.readInt();
    }

    public Test3(int x, int y ) {
        this.x = x;
        this.y = y;
    }


    public final int x;
    public final int y;


    public void resolve(java.util.HashMap<String, Object> _tables) {
    }

    @Override
    public String toString() {
        return "{ "
        + "x:" + x + ","
        + "y:" + y + ","
        + "}";
    }
}