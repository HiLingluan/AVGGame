// <auto-generated>
//  automatically generated by the FlatBuffers compiler, do not modify
// </auto-generated>

namespace cfg
{

using global::System;
using global::System.Collections.Generic;
using global::FlatBuffers;

public struct TestDemoType2 : IFlatbufferObject
{
  private Table __p;
  public ByteBuffer ByteBuffer { get { return __p.bb; } }
  public static void ValidateVersion() { FlatBufferConstants.FLATBUFFERS_2_0_0(); }
  public static TestDemoType2 GetRootAsTestDemoType2(ByteBuffer _bb) { return GetRootAsTestDemoType2(_bb, new TestDemoType2()); }
  public static TestDemoType2 GetRootAsTestDemoType2(ByteBuffer _bb, TestDemoType2 obj) { return (obj.__assign(_bb.GetInt(_bb.Position) + _bb.Position, _bb)); }
  public void __init(int _i, ByteBuffer _bb) { __p = new Table(_i, _bb); }
  public TestDemoType2 __assign(int _i, ByteBuffer _bb) { __init(_i, _bb); return this; }

  public int X4 { get { int o = __p.__offset(4); return o != 0 ? __p.bb.GetInt(o + __p.bb_pos) : (int)0; } }
  public bool X1 { get { int o = __p.__offset(6); return o != 0 ? 0!=__p.bb.Get(o + __p.bb_pos) : (bool)false; } }
  public byte X2 { get { int o = __p.__offset(8); return o != 0 ? __p.bb.Get(o + __p.bb_pos) : (byte)0; } }
  public short X3 { get { int o = __p.__offset(10); return o != 0 ? __p.bb.GetShort(o + __p.bb_pos) : (short)0; } }
  public long X5 { get { int o = __p.__offset(12); return o != 0 ? __p.bb.GetLong(o + __p.bb_pos) : (long)0; } }
  public float X6 { get { int o = __p.__offset(14); return o != 0 ? __p.bb.GetFloat(o + __p.bb_pos) : (float)0.0f; } }
  public double X7 { get { int o = __p.__offset(16); return o != 0 ? __p.bb.GetDouble(o + __p.bb_pos) : (double)0.0; } }
  public short X80 { get { int o = __p.__offset(18); return o != 0 ? __p.bb.GetShort(o + __p.bb_pos) : (short)0; } }
  public int X8 { get { int o = __p.__offset(20); return o != 0 ? __p.bb.GetInt(o + __p.bb_pos) : (int)0; } }
  public long X9 { get { int o = __p.__offset(22); return o != 0 ? __p.bb.GetLong(o + __p.bb_pos) : (long)0; } }
  public string X10 { get { int o = __p.__offset(24); return o != 0 ? __p.__string(o + __p.bb_pos) : null; } }
#if ENABLE_SPAN_T
  public Span<byte> GetX10Bytes() { return __p.__vector_as_span<byte>(24, 1); }
#else
  public ArraySegment<byte>? GetX10Bytes() { return __p.__vector_as_arraysegment(24); }
#endif
  public byte[] GetX10Array() { return __p.__vector_as_array<byte>(24); }
  public cfg.TestDemoType1? X12 { get { int o = __p.__offset(26); return o != 0 ? (cfg.TestDemoType1?)(new cfg.TestDemoType1()).__assign(__p.__indirect(o + __p.bb_pos), __p.bb) : null; } }
  public cfg.TestDemoEnum X13 { get { int o = __p.__offset(28); return o != 0 ? (cfg.TestDemoEnum)__p.bb.GetInt(o + __p.bb_pos) : cfg.TestDemoEnum.__GENERATE_DEFAULT_VALUE; } }
  public cfg.TestDemoDynamic X14Type { get { int o = __p.__offset(30); return o != 0 ? (cfg.TestDemoDynamic)__p.bb.Get(o + __p.bb_pos) : cfg.TestDemoDynamic.NONE; } }
  public TTable? X14<TTable>() where TTable : struct, IFlatbufferObject { int o = __p.__offset(32); return o != 0 ? (TTable?)__p.__union<TTable>(o + __p.bb_pos) : null; }
  public cfg.TestDemoD2 X14AsTestDemoD2() { return X14<cfg.TestDemoD2>().Value; }
  public cfg.TestDemoE1 X14AsTestDemoE1() { return X14<cfg.TestDemoE1>().Value; }
  public cfg.TestDemoD5 X14AsTestDemoD5() { return X14<cfg.TestDemoD5>().Value; }
  public string S1 { get { int o = __p.__offset(34); return o != 0 ? __p.__string(o + __p.bb_pos) : null; } }
#if ENABLE_SPAN_T
  public Span<byte> GetS1Bytes() { return __p.__vector_as_span<byte>(34, 1); }
#else
  public ArraySegment<byte>? GetS1Bytes() { return __p.__vector_as_arraysegment(34); }
#endif
  public byte[] GetS1Array() { return __p.__vector_as_array<byte>(34); }
  public cfg.Vector2? V2 { get { int o = __p.__offset(36); return o != 0 ? (cfg.Vector2?)(new cfg.Vector2()).__assign(o + __p.bb_pos, __p.bb) : null; } }
  public cfg.Vector3? V3 { get { int o = __p.__offset(38); return o != 0 ? (cfg.Vector3?)(new cfg.Vector3()).__assign(o + __p.bb_pos, __p.bb) : null; } }
  public cfg.Vector4? V4 { get { int o = __p.__offset(40); return o != 0 ? (cfg.Vector4?)(new cfg.Vector4()).__assign(o + __p.bb_pos, __p.bb) : null; } }
  public int T1 { get { int o = __p.__offset(42); return o != 0 ? __p.bb.GetInt(o + __p.bb_pos) : (int)0; } }
  public int K1(int j) { int o = __p.__offset(44); return o != 0 ? __p.bb.GetInt(__p.__vector(o) + j * 4) : (int)0; }
  public int K1Length { get { int o = __p.__offset(44); return o != 0 ? __p.__vector_len(o) : 0; } }
#if ENABLE_SPAN_T
  public Span<int> GetK1Bytes() { return __p.__vector_as_span<int>(44, 4); }
#else
  public ArraySegment<byte>? GetK1Bytes() { return __p.__vector_as_arraysegment(44); }
#endif
  public int[] GetK1Array() { return __p.__vector_as_array<int>(44); }
  public int K2(int j) { int o = __p.__offset(46); return o != 0 ? __p.bb.GetInt(__p.__vector(o) + j * 4) : (int)0; }
  public int K2Length { get { int o = __p.__offset(46); return o != 0 ? __p.__vector_len(o) : 0; } }
#if ENABLE_SPAN_T
  public Span<int> GetK2Bytes() { return __p.__vector_as_span<int>(46, 4); }
#else
  public ArraySegment<byte>? GetK2Bytes() { return __p.__vector_as_arraysegment(46); }
#endif
  public int[] GetK2Array() { return __p.__vector_as_array<int>(46); }
  public int K5(int j) { int o = __p.__offset(48); return o != 0 ? __p.bb.GetInt(__p.__vector(o) + j * 4) : (int)0; }
  public int K5Length { get { int o = __p.__offset(48); return o != 0 ? __p.__vector_len(o) : 0; } }
#if ENABLE_SPAN_T
  public Span<int> GetK5Bytes() { return __p.__vector_as_span<int>(48, 4); }
#else
  public ArraySegment<byte>? GetK5Bytes() { return __p.__vector_as_arraysegment(48); }
#endif
  public int[] GetK5Array() { return __p.__vector_as_array<int>(48); }
  public cfg.KeyValue_int32_int32? K8(int j) { int o = __p.__offset(50); return o != 0 ? (cfg.KeyValue_int32_int32?)(new cfg.KeyValue_int32_int32()).__assign(__p.__indirect(__p.__vector(o) + j * 4), __p.bb) : null; }
  public int K8Length { get { int o = __p.__offset(50); return o != 0 ? __p.__vector_len(o) : 0; } }
  public cfg.TestDemoE2? K9(int j) { int o = __p.__offset(52); return o != 0 ? (cfg.TestDemoE2?)(new cfg.TestDemoE2()).__assign(__p.__indirect(__p.__vector(o) + j * 4), __p.bb) : null; }
  public int K9Length { get { int o = __p.__offset(52); return o != 0 ? __p.__vector_len(o) : 0; } }
  public cfg.TestDemoDynamic K15Type(int j) { int o = __p.__offset(54); return o != 0 ? (cfg.TestDemoDynamic)__p.bb.Get(__p.__vector(o) + j * 1) : (cfg.TestDemoDynamic)0; }
  public int K15TypeLength { get { int o = __p.__offset(54); return o != 0 ? __p.__vector_len(o) : 0; } }
#if ENABLE_SPAN_T
  public Span<cfg.TestDemoDynamic> GetK15TypeBytes() { return __p.__vector_as_span<cfg.TestDemoDynamic>(54, 1); }
#else
  public ArraySegment<byte>? GetK15TypeBytes() { return __p.__vector_as_arraysegment(54); }
#endif
  public cfg.TestDemoDynamic[] GetK15TypeArray() { int o = __p.__offset(54); if (o == 0) return null; int p = __p.__vector(o); int l = __p.__vector_len(o); cfg.TestDemoDynamic[] a = new cfg.TestDemoDynamic[l]; for (int i = 0; i < l; i++) { a[i] = (cfg.TestDemoDynamic)__p.bb.Get(p + i * 1); } return a; }
  public TTable? K15<TTable>(int j) where TTable : struct, IFlatbufferObject { int o = __p.__offset(56); return o != 0 ? (TTable?)__p.__union<TTable>(__p.__vector(o) + j * 4) : null; }
  public int K15Length { get { int o = __p.__offset(56); return o != 0 ? __p.__vector_len(o) : 0; } }

  public static void StartTestDemoType2(FlatBufferBuilder builder) { builder.StartTable(27); }
  public static void AddX4(FlatBufferBuilder builder, int x4) { builder.AddInt(0, x4, 0); }
  public static void AddX1(FlatBufferBuilder builder, bool x1) { builder.AddBool(1, x1, false); }
  public static void AddX2(FlatBufferBuilder builder, byte x2) { builder.AddByte(2, x2, 0); }
  public static void AddX3(FlatBufferBuilder builder, short x3) { builder.AddShort(3, x3, 0); }
  public static void AddX5(FlatBufferBuilder builder, long x5) { builder.AddLong(4, x5, 0); }
  public static void AddX6(FlatBufferBuilder builder, float x6) { builder.AddFloat(5, x6, 0.0f); }
  public static void AddX7(FlatBufferBuilder builder, double x7) { builder.AddDouble(6, x7, 0.0); }
  public static void AddX80(FlatBufferBuilder builder, short x80) { builder.AddShort(7, x80, 0); }
  public static void AddX8(FlatBufferBuilder builder, int x8) { builder.AddInt(8, x8, 0); }
  public static void AddX9(FlatBufferBuilder builder, long x9) { builder.AddLong(9, x9, 0); }
  public static void AddX10(FlatBufferBuilder builder, StringOffset x10Offset) { builder.AddOffset(10, x10Offset.Value, 0); }
  public static void AddX12(FlatBufferBuilder builder, Offset<cfg.TestDemoType1> x12Offset) { builder.AddOffset(11, x12Offset.Value, 0); }
  public static void AddX13(FlatBufferBuilder builder, cfg.TestDemoEnum x13) { builder.AddInt(12, (int)x13, 0); }
  public static void AddX14Type(FlatBufferBuilder builder, cfg.TestDemoDynamic x14Type) { builder.AddByte(13, (byte)x14Type, 0); }
  public static void AddX14(FlatBufferBuilder builder, int x14Offset) { builder.AddOffset(14, x14Offset, 0); }
  public static void AddS1(FlatBufferBuilder builder, StringOffset s1Offset) { builder.AddOffset(15, s1Offset.Value, 0); }
  public static void AddV2(FlatBufferBuilder builder, Offset<cfg.Vector2> v2Offset) { builder.AddStruct(16, v2Offset.Value, 0); }
  public static void AddV3(FlatBufferBuilder builder, Offset<cfg.Vector3> v3Offset) { builder.AddStruct(17, v3Offset.Value, 0); }
  public static void AddV4(FlatBufferBuilder builder, Offset<cfg.Vector4> v4Offset) { builder.AddStruct(18, v4Offset.Value, 0); }
  public static void AddT1(FlatBufferBuilder builder, int t1) { builder.AddInt(19, t1, 0); }
  public static void AddK1(FlatBufferBuilder builder, VectorOffset k1Offset) { builder.AddOffset(20, k1Offset.Value, 0); }
  public static VectorOffset CreateK1Vector(FlatBufferBuilder builder, int[] data) { builder.StartVector(4, data.Length, 4); for (int i = data.Length - 1; i >= 0; i--) builder.AddInt(data[i]); return builder.EndVector(); }
  public static VectorOffset CreateK1VectorBlock(FlatBufferBuilder builder, int[] data) { builder.StartVector(4, data.Length, 4); builder.Add(data); return builder.EndVector(); }
  public static void StartK1Vector(FlatBufferBuilder builder, int numElems) { builder.StartVector(4, numElems, 4); }
  public static void AddK2(FlatBufferBuilder builder, VectorOffset k2Offset) { builder.AddOffset(21, k2Offset.Value, 0); }
  public static VectorOffset CreateK2Vector(FlatBufferBuilder builder, int[] data) { builder.StartVector(4, data.Length, 4); for (int i = data.Length - 1; i >= 0; i--) builder.AddInt(data[i]); return builder.EndVector(); }
  public static VectorOffset CreateK2VectorBlock(FlatBufferBuilder builder, int[] data) { builder.StartVector(4, data.Length, 4); builder.Add(data); return builder.EndVector(); }
  public static void StartK2Vector(FlatBufferBuilder builder, int numElems) { builder.StartVector(4, numElems, 4); }
  public static void AddK5(FlatBufferBuilder builder, VectorOffset k5Offset) { builder.AddOffset(22, k5Offset.Value, 0); }
  public static VectorOffset CreateK5Vector(FlatBufferBuilder builder, int[] data) { builder.StartVector(4, data.Length, 4); for (int i = data.Length - 1; i >= 0; i--) builder.AddInt(data[i]); return builder.EndVector(); }
  public static VectorOffset CreateK5VectorBlock(FlatBufferBuilder builder, int[] data) { builder.StartVector(4, data.Length, 4); builder.Add(data); return builder.EndVector(); }
  public static void StartK5Vector(FlatBufferBuilder builder, int numElems) { builder.StartVector(4, numElems, 4); }
  public static void AddK8(FlatBufferBuilder builder, VectorOffset k8Offset) { builder.AddOffset(23, k8Offset.Value, 0); }
  public static VectorOffset CreateK8Vector(FlatBufferBuilder builder, Offset<cfg.KeyValue_int32_int32>[] data) { builder.StartVector(4, data.Length, 4); for (int i = data.Length - 1; i >= 0; i--) builder.AddOffset(data[i].Value); return builder.EndVector(); }
  public static VectorOffset CreateK8VectorBlock(FlatBufferBuilder builder, Offset<cfg.KeyValue_int32_int32>[] data) { builder.StartVector(4, data.Length, 4); builder.Add(data); return builder.EndVector(); }
  public static void StartK8Vector(FlatBufferBuilder builder, int numElems) { builder.StartVector(4, numElems, 4); }
  public static void AddK9(FlatBufferBuilder builder, VectorOffset k9Offset) { builder.AddOffset(24, k9Offset.Value, 0); }
  public static VectorOffset CreateK9Vector(FlatBufferBuilder builder, Offset<cfg.TestDemoE2>[] data) { builder.StartVector(4, data.Length, 4); for (int i = data.Length - 1; i >= 0; i--) builder.AddOffset(data[i].Value); return builder.EndVector(); }
  public static VectorOffset CreateK9VectorBlock(FlatBufferBuilder builder, Offset<cfg.TestDemoE2>[] data) { builder.StartVector(4, data.Length, 4); builder.Add(data); return builder.EndVector(); }
  public static void StartK9Vector(FlatBufferBuilder builder, int numElems) { builder.StartVector(4, numElems, 4); }
  public static void AddK15Type(FlatBufferBuilder builder, VectorOffset k15TypeOffset) { builder.AddOffset(25, k15TypeOffset.Value, 0); }
  public static VectorOffset CreateK15TypeVector(FlatBufferBuilder builder, cfg.TestDemoDynamic[] data) { builder.StartVector(1, data.Length, 1); for (int i = data.Length - 1; i >= 0; i--) builder.AddByte((byte)data[i]); return builder.EndVector(); }
  public static VectorOffset CreateK15TypeVectorBlock(FlatBufferBuilder builder, cfg.TestDemoDynamic[] data) { builder.StartVector(1, data.Length, 1); builder.Add(data); return builder.EndVector(); }
  public static void StartK15TypeVector(FlatBufferBuilder builder, int numElems) { builder.StartVector(1, numElems, 1); }
  public static void AddK15(FlatBufferBuilder builder, VectorOffset k15Offset) { builder.AddOffset(26, k15Offset.Value, 0); }
  public static VectorOffset CreateK15Vector(FlatBufferBuilder builder, int[] data) { builder.StartVector(4, data.Length, 4); for (int i = data.Length - 1; i >= 0; i--) builder.AddOffset(data[i]); return builder.EndVector(); }
  public static VectorOffset CreateK15VectorBlock(FlatBufferBuilder builder, int[] data) { builder.StartVector(4, data.Length, 4); builder.Add(data); return builder.EndVector(); }
  public static void StartK15Vector(FlatBufferBuilder builder, int numElems) { builder.StartVector(4, numElems, 4); }
  public static Offset<cfg.TestDemoType2> EndTestDemoType2(FlatBufferBuilder builder) {
    int o = builder.EndTable();
    builder.Required(o, 26);  // x12
    builder.Required(o, 32);  // x14
    builder.Required(o, 36);  // v2
    builder.Required(o, 38);  // v3
    builder.Required(o, 40);  // v4
    builder.Required(o, 44);  // k1
    builder.Required(o, 46);  // k2
    builder.Required(o, 48);  // k5
    builder.Required(o, 50);  // k8
    builder.Required(o, 52);  // k9
    builder.Required(o, 54);  // k15_type
    builder.Required(o, 56);  // k15
    return new Offset<cfg.TestDemoType2>(o);
  }
};


}
