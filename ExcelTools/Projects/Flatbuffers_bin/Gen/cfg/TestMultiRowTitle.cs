// <auto-generated>
//  automatically generated by the FlatBuffers compiler, do not modify
// </auto-generated>

namespace cfg
{

using global::System;
using global::System.Collections.Generic;
using global::FlatBuffers;

public struct TestMultiRowTitle : IFlatbufferObject
{
  private Table __p;
  public ByteBuffer ByteBuffer { get { return __p.bb; } }
  public static void ValidateVersion() { FlatBufferConstants.FLATBUFFERS_2_0_0(); }
  public static TestMultiRowTitle GetRootAsTestMultiRowTitle(ByteBuffer _bb) { return GetRootAsTestMultiRowTitle(_bb, new TestMultiRowTitle()); }
  public static TestMultiRowTitle GetRootAsTestMultiRowTitle(ByteBuffer _bb, TestMultiRowTitle obj) { return (obj.__assign(_bb.GetInt(_bb.Position) + _bb.Position, _bb)); }
  public void __init(int _i, ByteBuffer _bb) { __p = new Table(_i, _bb); }
  public TestMultiRowTitle __assign(int _i, ByteBuffer _bb) { __init(_i, _bb); return this; }

  public int Id { get { int o = __p.__offset(4); return o != 0 ? __p.bb.GetInt(o + __p.bb_pos) : (int)0; } }
  public string Name { get { int o = __p.__offset(6); return o != 0 ? __p.__string(o + __p.bb_pos) : null; } }
#if ENABLE_SPAN_T
  public Span<byte> GetNameBytes() { return __p.__vector_as_span<byte>(6, 1); }
#else
  public ArraySegment<byte>? GetNameBytes() { return __p.__vector_as_arraysegment(6); }
#endif
  public byte[] GetNameArray() { return __p.__vector_as_array<byte>(6); }
  public cfg.TestH1? X1 { get { int o = __p.__offset(8); return o != 0 ? (cfg.TestH1?)(new cfg.TestH1()).__assign(__p.__indirect(o + __p.bb_pos), __p.bb) : null; } }
  public cfg.TestH2? X20 { get { int o = __p.__offset(10); return o != 0 ? (cfg.TestH2?)(new cfg.TestH2()).__assign(__p.__indirect(o + __p.bb_pos), __p.bb) : null; } }
  public cfg.TestH2? X2(int j) { int o = __p.__offset(12); return o != 0 ? (cfg.TestH2?)(new cfg.TestH2()).__assign(__p.__indirect(__p.__vector(o) + j * 4), __p.bb) : null; }
  public int X2Length { get { int o = __p.__offset(12); return o != 0 ? __p.__vector_len(o) : 0; } }
  public cfg.TestH2? X3(int j) { int o = __p.__offset(14); return o != 0 ? (cfg.TestH2?)(new cfg.TestH2()).__assign(__p.__indirect(__p.__vector(o) + j * 4), __p.bb) : null; }
  public int X3Length { get { int o = __p.__offset(14); return o != 0 ? __p.__vector_len(o) : 0; } }
  public cfg.TestH2? X4(int j) { int o = __p.__offset(16); return o != 0 ? (cfg.TestH2?)(new cfg.TestH2()).__assign(__p.__indirect(__p.__vector(o) + j * 4), __p.bb) : null; }
  public int X4Length { get { int o = __p.__offset(16); return o != 0 ? __p.__vector_len(o) : 0; } }

  public static Offset<cfg.TestMultiRowTitle> CreateTestMultiRowTitle(FlatBufferBuilder builder,
      int id = 0,
      StringOffset nameOffset = default(StringOffset),
      Offset<cfg.TestH1> x1Offset = default(Offset<cfg.TestH1>),
      Offset<cfg.TestH2> x2_0Offset = default(Offset<cfg.TestH2>),
      VectorOffset x2Offset = default(VectorOffset),
      VectorOffset x3Offset = default(VectorOffset),
      VectorOffset x4Offset = default(VectorOffset)) {
    builder.StartTable(7);
    TestMultiRowTitle.AddX4(builder, x4Offset);
    TestMultiRowTitle.AddX3(builder, x3Offset);
    TestMultiRowTitle.AddX2(builder, x2Offset);
    TestMultiRowTitle.AddX20(builder, x2_0Offset);
    TestMultiRowTitle.AddX1(builder, x1Offset);
    TestMultiRowTitle.AddName(builder, nameOffset);
    TestMultiRowTitle.AddId(builder, id);
    return TestMultiRowTitle.EndTestMultiRowTitle(builder);
  }

  public static void StartTestMultiRowTitle(FlatBufferBuilder builder) { builder.StartTable(7); }
  public static void AddId(FlatBufferBuilder builder, int id) { builder.AddInt(0, id, 0); }
  public static void AddName(FlatBufferBuilder builder, StringOffset nameOffset) { builder.AddOffset(1, nameOffset.Value, 0); }
  public static void AddX1(FlatBufferBuilder builder, Offset<cfg.TestH1> x1Offset) { builder.AddOffset(2, x1Offset.Value, 0); }
  public static void AddX20(FlatBufferBuilder builder, Offset<cfg.TestH2> x20Offset) { builder.AddOffset(3, x20Offset.Value, 0); }
  public static void AddX2(FlatBufferBuilder builder, VectorOffset x2Offset) { builder.AddOffset(4, x2Offset.Value, 0); }
  public static VectorOffset CreateX2Vector(FlatBufferBuilder builder, Offset<cfg.TestH2>[] data) { builder.StartVector(4, data.Length, 4); for (int i = data.Length - 1; i >= 0; i--) builder.AddOffset(data[i].Value); return builder.EndVector(); }
  public static VectorOffset CreateX2VectorBlock(FlatBufferBuilder builder, Offset<cfg.TestH2>[] data) { builder.StartVector(4, data.Length, 4); builder.Add(data); return builder.EndVector(); }
  public static void StartX2Vector(FlatBufferBuilder builder, int numElems) { builder.StartVector(4, numElems, 4); }
  public static void AddX3(FlatBufferBuilder builder, VectorOffset x3Offset) { builder.AddOffset(5, x3Offset.Value, 0); }
  public static VectorOffset CreateX3Vector(FlatBufferBuilder builder, Offset<cfg.TestH2>[] data) { builder.StartVector(4, data.Length, 4); for (int i = data.Length - 1; i >= 0; i--) builder.AddOffset(data[i].Value); return builder.EndVector(); }
  public static VectorOffset CreateX3VectorBlock(FlatBufferBuilder builder, Offset<cfg.TestH2>[] data) { builder.StartVector(4, data.Length, 4); builder.Add(data); return builder.EndVector(); }
  public static void StartX3Vector(FlatBufferBuilder builder, int numElems) { builder.StartVector(4, numElems, 4); }
  public static void AddX4(FlatBufferBuilder builder, VectorOffset x4Offset) { builder.AddOffset(6, x4Offset.Value, 0); }
  public static VectorOffset CreateX4Vector(FlatBufferBuilder builder, Offset<cfg.TestH2>[] data) { builder.StartVector(4, data.Length, 4); for (int i = data.Length - 1; i >= 0; i--) builder.AddOffset(data[i].Value); return builder.EndVector(); }
  public static VectorOffset CreateX4VectorBlock(FlatBufferBuilder builder, Offset<cfg.TestH2>[] data) { builder.StartVector(4, data.Length, 4); builder.Add(data); return builder.EndVector(); }
  public static void StartX4Vector(FlatBufferBuilder builder, int numElems) { builder.StartVector(4, numElems, 4); }
  public static Offset<cfg.TestMultiRowTitle> EndTestMultiRowTitle(FlatBufferBuilder builder) {
    int o = builder.EndTable();
    builder.Required(o, 8);  // x1
    builder.Required(o, 12);  // x2
    builder.Required(o, 14);  // x3
    builder.Required(o, 16);  // x4
    return new Offset<cfg.TestMultiRowTitle>(o);
  }
};


}
