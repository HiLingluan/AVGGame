// <auto-generated>
//  automatically generated by the FlatBuffers compiler, do not modify
// </auto-generated>

namespace cfg
{

using global::System;
using global::System.Collections.Generic;
using global::FlatBuffers;

public struct TestH1 : IFlatbufferObject
{
  private Table __p;
  public ByteBuffer ByteBuffer { get { return __p.bb; } }
  public static void ValidateVersion() { FlatBufferConstants.FLATBUFFERS_2_0_0(); }
  public static TestH1 GetRootAsTestH1(ByteBuffer _bb) { return GetRootAsTestH1(_bb, new TestH1()); }
  public static TestH1 GetRootAsTestH1(ByteBuffer _bb, TestH1 obj) { return (obj.__assign(_bb.GetInt(_bb.Position) + _bb.Position, _bb)); }
  public void __init(int _i, ByteBuffer _bb) { __p = new Table(_i, _bb); }
  public TestH1 __assign(int _i, ByteBuffer _bb) { __init(_i, _bb); return this; }

  public cfg.TestH2? Y2 { get { int o = __p.__offset(4); return o != 0 ? (cfg.TestH2?)(new cfg.TestH2()).__assign(__p.__indirect(o + __p.bb_pos), __p.bb) : null; } }
  public int Y3 { get { int o = __p.__offset(6); return o != 0 ? __p.bb.GetInt(o + __p.bb_pos) : (int)0; } }

  public static Offset<cfg.TestH1> CreateTestH1(FlatBufferBuilder builder,
      Offset<cfg.TestH2> y2Offset = default(Offset<cfg.TestH2>),
      int y3 = 0) {
    builder.StartTable(2);
    TestH1.AddY3(builder, y3);
    TestH1.AddY2(builder, y2Offset);
    return TestH1.EndTestH1(builder);
  }

  public static void StartTestH1(FlatBufferBuilder builder) { builder.StartTable(2); }
  public static void AddY2(FlatBufferBuilder builder, Offset<cfg.TestH2> y2Offset) { builder.AddOffset(0, y2Offset.Value, 0); }
  public static void AddY3(FlatBufferBuilder builder, int y3) { builder.AddInt(1, y3, 0); }
  public static Offset<cfg.TestH1> EndTestH1(FlatBufferBuilder builder) {
    int o = builder.EndTable();
    builder.Required(o, 4);  // y2
    return new Offset<cfg.TestH1>(o);
  }
};


}
