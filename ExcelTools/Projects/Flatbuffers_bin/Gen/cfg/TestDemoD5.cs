// <auto-generated>
//  automatically generated by the FlatBuffers compiler, do not modify
// </auto-generated>

namespace cfg
{

using global::System;
using global::System.Collections.Generic;
using global::FlatBuffers;

public struct TestDemoD5 : IFlatbufferObject
{
  private Table __p;
  public ByteBuffer ByteBuffer { get { return __p.bb; } }
  public static void ValidateVersion() { FlatBufferConstants.FLATBUFFERS_2_0_0(); }
  public static TestDemoD5 GetRootAsTestDemoD5(ByteBuffer _bb) { return GetRootAsTestDemoD5(_bb, new TestDemoD5()); }
  public static TestDemoD5 GetRootAsTestDemoD5(ByteBuffer _bb, TestDemoD5 obj) { return (obj.__assign(_bb.GetInt(_bb.Position) + _bb.Position, _bb)); }
  public void __init(int _i, ByteBuffer _bb) { __p = new Table(_i, _bb); }
  public TestDemoD5 __assign(int _i, ByteBuffer _bb) { __init(_i, _bb); return this; }

  public int X1 { get { int o = __p.__offset(4); return o != 0 ? __p.bb.GetInt(o + __p.bb_pos) : (int)0; } }
  public cfg.TestDateTimeRange? Time { get { int o = __p.__offset(6); return o != 0 ? (cfg.TestDateTimeRange?)(new cfg.TestDateTimeRange()).__assign(__p.__indirect(o + __p.bb_pos), __p.bb) : null; } }

  public static Offset<cfg.TestDemoD5> CreateTestDemoD5(FlatBufferBuilder builder,
      int x1 = 0,
      Offset<cfg.TestDateTimeRange> timeOffset = default(Offset<cfg.TestDateTimeRange>)) {
    builder.StartTable(2);
    TestDemoD5.AddTime(builder, timeOffset);
    TestDemoD5.AddX1(builder, x1);
    return TestDemoD5.EndTestDemoD5(builder);
  }

  public static void StartTestDemoD5(FlatBufferBuilder builder) { builder.StartTable(2); }
  public static void AddX1(FlatBufferBuilder builder, int x1) { builder.AddInt(0, x1, 0); }
  public static void AddTime(FlatBufferBuilder builder, Offset<cfg.TestDateTimeRange> timeOffset) { builder.AddOffset(1, timeOffset.Value, 0); }
  public static Offset<cfg.TestDemoD5> EndTestDemoD5(FlatBufferBuilder builder) {
    int o = builder.EndTable();
    builder.Required(o, 6);  // time
    return new Offset<cfg.TestDemoD5>(o);
  }
};


}