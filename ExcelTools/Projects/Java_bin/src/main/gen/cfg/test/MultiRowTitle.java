
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------
package cfg.test;

import bright.serialization.*;



public final class MultiRowTitle {
    public MultiRowTitle(ByteBuf _buf) { 
        id = _buf.readInt();
        name = _buf.readString();
        x1 = new cfg.test.H1(_buf);
        {int n = Math.min(_buf.readSize(), _buf.size());x2 = new java.util.ArrayList<cfg.test.H2>(n);for(int i = 0 ; i < n ; i++) { cfg.test.H2 _e;  _e = new cfg.test.H2(_buf); x2.add(_e);}}
        {int n = Math.min(_buf.readSize(), _buf.size());x3 = new cfg.test.H2[n];for(int i = 0 ; i < n ; i++) { cfg.test.H2 _e;_e = new cfg.test.H2(_buf); x3[i] = _e;}}
        {int n = Math.min(_buf.readSize(), _buf.size());x4 = new cfg.test.H2[n];for(int i = 0 ; i < n ; i++) { cfg.test.H2 _e;_e = new cfg.test.H2(_buf); x4[i] = _e;}}
    }

    public MultiRowTitle(int id, String name, cfg.test.H1 x1, java.util.ArrayList<cfg.test.H2> x2, cfg.test.H2[] x3, cfg.test.H2[] x4 ) {
        this.id = id;
        this.name = name;
        this.x1 = x1;
        this.x2 = x2;
        this.x3 = x3;
        this.x4 = x4;
    }


    public final int id;
    public final String name;
    public final cfg.test.H1 x1;
    public final java.util.ArrayList<cfg.test.H2> x2;
    public final cfg.test.H2[] x3;
    public final cfg.test.H2[] x4;


    public void resolve(java.util.HashMap<String, Object> _tables) {
        if (x1 != null) {x1.resolve(_tables);}
        for(cfg.test.H2 _e : x2) { if (_e != null) _e.resolve(_tables); }
        for(cfg.test.H2 _e : x3) { if (_e != null) _e.resolve(_tables); }
        for(cfg.test.H2 _e : x4) { if (_e != null) _e.resolve(_tables); }
    }

    @Override
    public String toString() {
        return "{ "
        + "id:" + id + ","
        + "name:" + name + ","
        + "x1:" + x1 + ","
        + "x2:" + x2 + ","
        + "x3:" + x3 + ","
        + "x4:" + x4 + ","
        + "}";
    }
}