// <auto-generated>
//  automatically generated by the FlatBuffers compiler, do not modify
// </auto-generated>

namespace cfg
{

using global::System;
using global::System.Collections.Generic;
using global::FlatBuffers;

public struct TestTestNull : IFlatbufferObject
{
  private Table __p;
  public ByteBuffer ByteBuffer { get { return __p.bb; } }
  public static void ValidateVersion() { FlatBufferConstants.FLATBUFFERS_2_0_0(); }
  public static TestTestNull GetRootAsTestTestNull(ByteBuffer _bb) { return GetRootAsTestTestNull(_bb, new TestTestNull()); }
  public static TestTestNull GetRootAsTestTestNull(ByteBuffer _bb, TestTestNull obj) { return (obj.__assign(_bb.GetInt(_bb.Position) + _bb.Position, _bb)); }
  public void __init(int _i, ByteBuffer _bb) { __p = new Table(_i, _bb); }
  public TestTestNull __assign(int _i, ByteBuffer _bb) { __init(_i, _bb); return this; }

  public int Id { get { int o = __p.__offset(4); return o != 0 ? __p.bb.GetInt(o + __p.bb_pos) : (int)0; } }
  public int X1 { get { int o = __p.__offset(6); return o != 0 ? __p.bb.GetInt(o + __p.bb_pos) : (int)0; } }
  public cfg.TestDemoEnum X2 { get { int o = __p.__offset(8); return o != 0 ? (cfg.TestDemoEnum)__p.bb.GetInt(o + __p.bb_pos) : cfg.TestDemoEnum.__GENERATE_DEFAULT_VALUE; } }
  public cfg.TestDemoType1? X3 { get { int o = __p.__offset(10); return o != 0 ? (cfg.TestDemoType1?)(new cfg.TestDemoType1()).__assign(__p.__indirect(o + __p.bb_pos), __p.bb) : null; } }
  public cfg.TestDemoDynamic X4Type { get { int o = __p.__offset(12); return o != 0 ? (cfg.TestDemoDynamic)__p.bb.Get(o + __p.bb_pos) : cfg.TestDemoDynamic.NONE; } }
  public TTable? X4<TTable>() where TTable : struct, IFlatbufferObject { int o = __p.__offset(14); return o != 0 ? (TTable?)__p.__union<TTable>(o + __p.bb_pos) : null; }
  public cfg.TestDemoD2 X4AsTestDemoD2() { return X4<cfg.TestDemoD2>().Value; }
  public cfg.TestDemoE1 X4AsTestDemoE1() { return X4<cfg.TestDemoE1>().Value; }
  public cfg.TestDemoD5 X4AsTestDemoD5() { return X4<cfg.TestDemoD5>().Value; }
  public string S1 { get { int o = __p.__offset(16); return o != 0 ? __p.__string(o + __p.bb_pos) : null; } }
#if ENABLE_SPAN_T
  public Span<byte> GetS1Bytes() { return __p.__vector_as_span<byte>(16, 1); }
#else
  public ArraySegment<byte>? GetS1Bytes() { return __p.__vector_as_arraysegment(16); }
#endif
  public byte[] GetS1Array() { return __p.__vector_as_array<byte>(16); }
  public string S2 { get { int o = __p.__offset(18); return o != 0 ? __p.__string(o + __p.bb_pos) : null; } }
#if ENABLE_SPAN_T
  public Span<byte> GetS2Bytes() { return __p.__vector_as_span<byte>(18, 1); }
#else
  public ArraySegment<byte>? GetS2Bytes() { return __p.__vector_as_arraysegment(18); }
#endif
  public byte[] GetS2Array() { return __p.__vector_as_array<byte>(18); }

  public static Offset<cfg.TestTestNull> CreateTestTestNull(FlatBufferBuilder builder,
      int id = 0,
      int x1 = 0,
      cfg.TestDemoEnum x2 = cfg.TestDemoEnum.__GENERATE_DEFAULT_VALUE,
      Offset<cfg.TestDemoType1> x3Offset = default(Offset<cfg.TestDemoType1>),
      cfg.TestDemoDynamic x4_type = cfg.TestDemoDynamic.NONE,
      int x4Offset = 0,
      StringOffset s1Offset = default(StringOffset),
      StringOffset s2Offset = default(StringOffset)) {
    builder.StartTable(8);
    TestTestNull.AddS2(builder, s2Offset);
    TestTestNull.AddS1(builder, s1Offset);
    TestTestNull.AddX4(builder, x4Offset);
    TestTestNull.AddX3(builder, x3Offset);
    TestTestNull.AddX2(builder, x2);
    TestTestNull.AddX1(builder, x1);
    TestTestNull.AddId(builder, id);
    TestTestNull.AddX4Type(builder, x4_type);
    return TestTestNull.EndTestTestNull(builder);
  }

  public static void StartTestTestNull(FlatBufferBuilder builder) { builder.StartTable(8); }
  public static void AddId(FlatBufferBuilder builder, int id) { builder.AddInt(0, id, 0); }
  public static void AddX1(FlatBufferBuilder builder, int x1) { builder.AddInt(1, x1, 0); }
  public static void AddX2(FlatBufferBuilder builder, cfg.TestDemoEnum x2) { builder.AddInt(2, (int)x2, 0); }
  public static void AddX3(FlatBufferBuilder builder, Offset<cfg.TestDemoType1> x3Offset) { builder.AddOffset(3, x3Offset.Value, 0); }
  public static void AddX4Type(FlatBufferBuilder builder, cfg.TestDemoDynamic x4Type) { builder.AddByte(4, (byte)x4Type, 0); }
  public static void AddX4(FlatBufferBuilder builder, int x4Offset) { builder.AddOffset(5, x4Offset, 0); }
  public static void AddS1(FlatBufferBuilder builder, StringOffset s1Offset) { builder.AddOffset(6, s1Offset.Value, 0); }
  public static void AddS2(FlatBufferBuilder builder, StringOffset s2Offset) { builder.AddOffset(7, s2Offset.Value, 0); }
  public static Offset<cfg.TestTestNull> EndTestTestNull(FlatBufferBuilder builder) {
    int o = builder.EndTable();
    return new Offset<cfg.TestTestNull>(o);
  }
};


}
