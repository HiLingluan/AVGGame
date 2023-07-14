//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using Bright.Serialization;

namespace proto.test
{

    public  sealed class AllType :  Bright.Serialization.BeanBase 
    {
        public AllType()
        {
        }

        public AllType(Bright.Common.NotNullInitialization _) 
        {
            A1 = "";
            A2 = System.Array.Empty<byte>();
            A3 = new test.Simple();
            B1 = System.Array.Empty<int>();
            B2 = System.Array.Empty<test.Simple>();
            B3 = System.Array.Empty<test.Dyn>();
            C1 = new System.Collections.Generic.List<int>();
            C2 = new System.Collections.Generic.List<test.Simple>();
            C3 = new System.Collections.Generic.List<test.Dyn>();
            D1 = new System.Collections.Generic.HashSet<int>();
            E1 = new System.Collections.Generic.Dictionary<int,int>();
            E2 = new System.Collections.Generic.Dictionary<int,test.Simple>();
            E3 = new System.Collections.Generic.Dictionary<int,test.Dyn>();
        }

        public static void SerializeAllType(ByteBuf _buf, AllType x)
        {
            x.Serialize(_buf);
        }

        public static AllType DeserializeAllType(ByteBuf _buf)
        {
            var x = new test.AllType();
            x.Deserialize(_buf);
            return x;
        }

         public bool X1;

         public byte X2;

         public short X3;

         public short X4;

         public int X5;

         public int X6;

         public long X7;

         public long X8;

         public string A1;

         public byte[] A2;

         public test.Simple A3;

         public test.Dyn A4;

         public int[] B1;

         public test.Simple[] B2;

         public test.Dyn[] B3;

         public System.Collections.Generic.List<int> C1;

         public System.Collections.Generic.List<test.Simple> C2;

         public System.Collections.Generic.List<test.Dyn> C3;

         public System.Collections.Generic.HashSet<int> D1;

         public System.Collections.Generic.Dictionary<int, int> E1;

         public System.Collections.Generic.Dictionary<int, test.Simple> E2;

         public System.Collections.Generic.Dictionary<int, test.Dyn> E3;


        public const int __ID__ = 0;
        public override int GetTypeId() => __ID__;

        public override void Serialize(ByteBuf _buf)
        {
            _buf.WriteBool(X1);
            _buf.WriteByte(X2);
            _buf.WriteShort(X3);
            _buf.WriteFshort(X4);
            _buf.WriteInt(X5);
            _buf.WriteFint(X6);
            _buf.WriteLong(X7);
            _buf.WriteFlong(X8);
            _buf.WriteString(A1);
            _buf.WriteBytes(A2);
            test.Simple.SerializeSimple(_buf, A3);
            test.Dyn.SerializeDyn(_buf, A4);
            { _buf.WriteSize(B1.Length); foreach(var _e in B1) { _buf.WriteInt(_e); } }
            { _buf.WriteSize(B2.Length); foreach(var _e in B2) { test.Simple.SerializeSimple(_buf, _e); } }
            { _buf.WriteSize(B3.Length); foreach(var _e in B3) { test.Dyn.SerializeDyn(_buf, _e); } }
            { _buf.WriteSize(C1.Count); foreach(var _e in C1) { _buf.WriteInt(_e); } }
            { _buf.WriteSize(C2.Count); foreach(var _e in C2) { test.Simple.SerializeSimple(_buf, _e); } }
            { _buf.WriteSize(C3.Count); foreach(var _e in C3) { test.Dyn.SerializeDyn(_buf, _e); } }
            { _buf.WriteSize(D1.Count); foreach(var _e in D1) { _buf.WriteInt(_e); } }
            { _buf.WriteSize(E1.Count); foreach(var _e in E1) { _buf.WriteInt(_e.Key); _buf.WriteInt(_e.Value); } }
            { _buf.WriteSize(E2.Count); foreach(var _e in E2) { _buf.WriteInt(_e.Key); test.Simple.SerializeSimple(_buf, _e.Value); } }
            { _buf.WriteSize(E3.Count); foreach(var _e in E3) { _buf.WriteInt(_e.Key); test.Dyn.SerializeDyn(_buf, _e.Value); } }
        }

        public override void Deserialize(ByteBuf _buf)
        {
            X1 = _buf.ReadBool();
            X2 = _buf.ReadByte();
            X3 = _buf.ReadShort();
            X4 = _buf.ReadFshort();
            X5 = _buf.ReadInt();
            X6 = _buf.ReadFint();
            X7 = _buf.ReadLong();
            X8 = _buf.ReadFlong();
            A1 = _buf.ReadString();
            A2 = _buf.ReadBytes();
            A3 = test.Simple.DeserializeSimple(_buf);
            A4 = test.Dyn.DeserializeDyn(_buf);
            {int n = System.Math.Min(_buf.ReadSize(), _buf.Size);B1 = new int[n];for(var i = 0 ; i < n ; i++) { int _e;_e = _buf.ReadInt(); B1[i] = _e;}}
            {int n = System.Math.Min(_buf.ReadSize(), _buf.Size);B2 = new test.Simple[n];for(var i = 0 ; i < n ; i++) { test.Simple _e;_e = test.Simple.DeserializeSimple(_buf); B2[i] = _e;}}
            {int n = System.Math.Min(_buf.ReadSize(), _buf.Size);B3 = new test.Dyn[n];for(var i = 0 ; i < n ; i++) { test.Dyn _e;_e = test.Dyn.DeserializeDyn(_buf); B3[i] = _e;}}
            {int n = System.Math.Min(_buf.ReadSize(), _buf.Size);C1 = new System.Collections.Generic.List<int>(n);for(var i = 0 ; i < n ; i++) { int _e;  _e = _buf.ReadInt(); C1.Add(_e);}}
            {int n = System.Math.Min(_buf.ReadSize(), _buf.Size);C2 = new System.Collections.Generic.List<test.Simple>(n);for(var i = 0 ; i < n ; i++) { test.Simple _e;  _e = test.Simple.DeserializeSimple(_buf); C2.Add(_e);}}
            {int n = System.Math.Min(_buf.ReadSize(), _buf.Size);C3 = new System.Collections.Generic.List<test.Dyn>(n);for(var i = 0 ; i < n ; i++) { test.Dyn _e;  _e = test.Dyn.DeserializeDyn(_buf); C3.Add(_e);}}
            {int n = System.Math.Min(_buf.ReadSize(), _buf.Size);D1 = new System.Collections.Generic.HashSet<int>(/*n * 3 / 2*/);for(var i = 0 ; i < n ; i++) { int _e;  _e = _buf.ReadInt(); D1.Add(_e);}}
            {int n = System.Math.Min(_buf.ReadSize(), _buf.Size);E1 = new System.Collections.Generic.Dictionary<int, int>(n * 3 / 2);for(var i = 0 ; i < n ; i++) { int _k;  _k = _buf.ReadInt(); int _v;  _v = _buf.ReadInt();     E1.Add(_k, _v);}}
            {int n = System.Math.Min(_buf.ReadSize(), _buf.Size);E2 = new System.Collections.Generic.Dictionary<int, test.Simple>(n * 3 / 2);for(var i = 0 ; i < n ; i++) { int _k;  _k = _buf.ReadInt(); test.Simple _v;  _v = test.Simple.DeserializeSimple(_buf);     E2.Add(_k, _v);}}
            {int n = System.Math.Min(_buf.ReadSize(), _buf.Size);E3 = new System.Collections.Generic.Dictionary<int, test.Dyn>(n * 3 / 2);for(var i = 0 ; i < n ; i++) { int _k;  _k = _buf.ReadInt(); test.Dyn _v;  _v = test.Dyn.DeserializeDyn(_buf);     E3.Add(_k, _v);}}
        }

        public override string ToString()
        {
            return "test.AllType{ "
            + "X1:" + X1 + ","
            + "X2:" + X2 + ","
            + "X3:" + X3 + ","
            + "X4:" + X4 + ","
            + "X5:" + X5 + ","
            + "X6:" + X6 + ","
            + "X7:" + X7 + ","
            + "X8:" + X8 + ","
            + "A1:" + A1 + ","
            + "A2:" + A2 + ","
            + "A3:" + A3 + ","
            + "A4:" + A4 + ","
            + "B1:" + Bright.Common.StringUtil.CollectionToString(B1) + ","
            + "B2:" + Bright.Common.StringUtil.CollectionToString(B2) + ","
            + "B3:" + Bright.Common.StringUtil.CollectionToString(B3) + ","
            + "C1:" + Bright.Common.StringUtil.CollectionToString(C1) + ","
            + "C2:" + Bright.Common.StringUtil.CollectionToString(C2) + ","
            + "C3:" + Bright.Common.StringUtil.CollectionToString(C3) + ","
            + "D1:" + Bright.Common.StringUtil.CollectionToString(D1) + ","
            + "E1:" + Bright.Common.StringUtil.CollectionToString(E1) + ","
            + "E2:" + Bright.Common.StringUtil.CollectionToString(E2) + ","
            + "E3:" + Bright.Common.StringUtil.CollectionToString(E3) + ","
            + "}";
        }
    }

}