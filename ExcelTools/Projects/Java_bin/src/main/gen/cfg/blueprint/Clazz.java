
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------
package cfg.blueprint;

import bright.serialization.*;



public abstract class Clazz {
    public Clazz(ByteBuf _buf) { 
        name = _buf.readString();
        desc = _buf.readString();
        {int n = Math.min(_buf.readSize(), _buf.size());parents = new java.util.ArrayList<cfg.blueprint.Clazz>(n);for(int i = 0 ; i < n ; i++) { cfg.blueprint.Clazz _e;  _e = cfg.blueprint.Clazz.deserializeClazz(_buf); parents.add(_e);}}
        {int n = Math.min(_buf.readSize(), _buf.size());methods = new java.util.ArrayList<cfg.blueprint.Method>(n);for(int i = 0 ; i < n ; i++) { cfg.blueprint.Method _e;  _e = cfg.blueprint.Method.deserializeMethod(_buf); methods.add(_e);}}
    }

    public Clazz(String name, String desc, java.util.ArrayList<cfg.blueprint.Clazz> parents, java.util.ArrayList<cfg.blueprint.Method> methods ) {
        this.name = name;
        this.desc = desc;
        this.parents = parents;
        this.methods = methods;
    }

    public static Clazz deserializeClazz(ByteBuf _buf) {
        switch (_buf.readInt()) {
            case cfg.blueprint.Interface.__ID__: return new cfg.blueprint.Interface(_buf);
            case cfg.blueprint.NormalClazz.__ID__: return new cfg.blueprint.NormalClazz(_buf);
            case cfg.blueprint.EnumClazz.__ID__: return new cfg.blueprint.EnumClazz(_buf);
            default: throw new SerializationException();
        }
    }

    public final String name;
    public final String desc;
    public final java.util.ArrayList<cfg.blueprint.Clazz> parents;
    public final java.util.ArrayList<cfg.blueprint.Method> methods;

    public abstract int getTypeId();

    public void resolve(java.util.HashMap<String, Object> _tables) {
        for(cfg.blueprint.Clazz _e : parents) { if (_e != null) _e.resolve(_tables); }
        for(cfg.blueprint.Method _e : methods) { if (_e != null) _e.resolve(_tables); }
    }

    @Override
    public String toString() {
        return "{ "
        + "name:" + name + ","
        + "desc:" + desc + ","
        + "parents:" + parents + ","
        + "methods:" + methods + ","
        + "}";
    }
}