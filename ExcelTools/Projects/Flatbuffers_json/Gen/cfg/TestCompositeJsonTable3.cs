// <auto-generated>
//  automatically generated by the FlatBuffers compiler, do not modify
// </auto-generated>

namespace cfg
{

using global::System;
using global::System.Collections.Generic;
using global::FlatBuffers;

public struct TestCompositeJsonTable3 : IFlatbufferObject
{
  private Table __p;
  public ByteBuffer ByteBuffer { get { return __p.bb; } }
  public static void ValidateVersion() { FlatBufferConstants.FLATBUFFERS_2_0_0(); }
  public static TestCompositeJsonTable3 GetRootAsTestCompositeJsonTable3(ByteBuffer _bb) { return GetRootAsTestCompositeJsonTable3(_bb, new TestCompositeJsonTable3()); }
  public static TestCompositeJsonTable3 GetRootAsTestCompositeJsonTable3(ByteBuffer _bb, TestCompositeJsonTable3 obj) { return (obj.__assign(_bb.GetInt(_bb.Position) + _bb.Position, _bb)); }
  public void __init(int _i, ByteBuffer _bb) { __p = new Table(_i, _bb); }
  public TestCompositeJsonTable3 __assign(int _i, ByteBuffer _bb) { __init(_i, _bb); return this; }

  public int A { get { int o = __p.__offset(4); return o != 0 ? __p.bb.GetInt(o + __p.bb_pos) : (int)0; } }
  public int B { get { int o = __p.__offset(6); return o != 0 ? __p.bb.GetInt(o + __p.bb_pos) : (int)0; } }

  public static Offset<cfg.TestCompositeJsonTable3> CreateTestCompositeJsonTable3(FlatBufferBuilder builder,
      int a = 0,
      int b = 0) {
    builder.StartTable(2);
    TestCompositeJsonTable3.AddB(builder, b);
    TestCompositeJsonTable3.AddA(builder, a);
    return TestCompositeJsonTable3.EndTestCompositeJsonTable3(builder);
  }

  public static void StartTestCompositeJsonTable3(FlatBufferBuilder builder) { builder.StartTable(2); }
  public static void AddA(FlatBufferBuilder builder, int a) { builder.AddInt(0, a, 0); }
  public static void AddB(FlatBufferBuilder builder, int b) { builder.AddInt(1, b, 0); }
  public static Offset<cfg.TestCompositeJsonTable3> EndTestCompositeJsonTable3(FlatBufferBuilder builder) {
    int o = builder.EndTable();
    return new Offset<cfg.TestCompositeJsonTable3>(o);
  }
};


}
