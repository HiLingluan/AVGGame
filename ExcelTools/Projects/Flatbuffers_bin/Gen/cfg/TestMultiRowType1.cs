// <auto-generated>
//  automatically generated by the FlatBuffers compiler, do not modify
// </auto-generated>

namespace cfg
{

using global::System;
using global::System.Collections.Generic;
using global::FlatBuffers;

public struct TestMultiRowType1 : IFlatbufferObject
{
  private Table __p;
  public ByteBuffer ByteBuffer { get { return __p.bb; } }
  public static void ValidateVersion() { FlatBufferConstants.FLATBUFFERS_2_0_0(); }
  public static TestMultiRowType1 GetRootAsTestMultiRowType1(ByteBuffer _bb) { return GetRootAsTestMultiRowType1(_bb, new TestMultiRowType1()); }
  public static TestMultiRowType1 GetRootAsTestMultiRowType1(ByteBuffer _bb, TestMultiRowType1 obj) { return (obj.__assign(_bb.GetInt(_bb.Position) + _bb.Position, _bb)); }
  public void __init(int _i, ByteBuffer _bb) { __p = new Table(_i, _bb); }
  public TestMultiRowType1 __assign(int _i, ByteBuffer _bb) { __init(_i, _bb); return this; }

  public int Id { get { int o = __p.__offset(4); return o != 0 ? __p.bb.GetInt(o + __p.bb_pos) : (int)0; } }
  public int X { get { int o = __p.__offset(6); return o != 0 ? __p.bb.GetInt(o + __p.bb_pos) : (int)0; } }

  public static Offset<cfg.TestMultiRowType1> CreateTestMultiRowType1(FlatBufferBuilder builder,
      int id = 0,
      int x = 0) {
    builder.StartTable(2);
    TestMultiRowType1.AddX(builder, x);
    TestMultiRowType1.AddId(builder, id);
    return TestMultiRowType1.EndTestMultiRowType1(builder);
  }

  public static void StartTestMultiRowType1(FlatBufferBuilder builder) { builder.StartTable(2); }
  public static void AddId(FlatBufferBuilder builder, int id) { builder.AddInt(0, id, 0); }
  public static void AddX(FlatBufferBuilder builder, int x) { builder.AddInt(1, x, 0); }
  public static Offset<cfg.TestMultiRowType1> EndTestMultiRowType1(FlatBufferBuilder builder) {
    int o = builder.EndTable();
    return new Offset<cfg.TestMultiRowType1>(o);
  }
};


}