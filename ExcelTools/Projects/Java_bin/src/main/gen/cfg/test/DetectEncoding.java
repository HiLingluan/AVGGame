
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------
package cfg.test;

import bright.serialization.*;



public final class DetectEncoding {
    public DetectEncoding(ByteBuf _buf) { 
        id = _buf.readInt();
        name = _buf.readString();
    }

    public DetectEncoding(int id, String name ) {
        this.id = id;
        this.name = name;
    }


    public final int id;
    public final String name;


    public void resolve(java.util.HashMap<String, Object> _tables) {
    }

    @Override
    public String toString() {
        return "{ "
        + "id:" + id + ","
        + "name:" + name + ","
        + "}";
    }
}