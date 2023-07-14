// <auto-generated>
//  automatically generated by the FlatBuffers compiler, do not modify
// </auto-generated>

namespace cfg
{

using global::System;
using global::System.Collections.Generic;
using global::FlatBuffers;

public struct BlueprintEnumClazz : IFlatbufferObject
{
  private Table __p;
  public ByteBuffer ByteBuffer { get { return __p.bb; } }
  public static void ValidateVersion() { FlatBufferConstants.FLATBUFFERS_2_0_0(); }
  public static BlueprintEnumClazz GetRootAsBlueprintEnumClazz(ByteBuffer _bb) { return GetRootAsBlueprintEnumClazz(_bb, new BlueprintEnumClazz()); }
  public static BlueprintEnumClazz GetRootAsBlueprintEnumClazz(ByteBuffer _bb, BlueprintEnumClazz obj) { return (obj.__assign(_bb.GetInt(_bb.Position) + _bb.Position, _bb)); }
  public void __init(int _i, ByteBuffer _bb) { __p = new Table(_i, _bb); }
  public BlueprintEnumClazz __assign(int _i, ByteBuffer _bb) { __init(_i, _bb); return this; }

  public string Name { get { int o = __p.__offset(4); return o != 0 ? __p.__string(o + __p.bb_pos) : null; } }
#if ENABLE_SPAN_T
  public Span<byte> GetNameBytes() { return __p.__vector_as_span<byte>(4, 1); }
#else
  public ArraySegment<byte>? GetNameBytes() { return __p.__vector_as_arraysegment(4); }
#endif
  public byte[] GetNameArray() { return __p.__vector_as_array<byte>(4); }
  public string Desc { get { int o = __p.__offset(6); return o != 0 ? __p.__string(o + __p.bb_pos) : null; } }
#if ENABLE_SPAN_T
  public Span<byte> GetDescBytes() { return __p.__vector_as_span<byte>(6, 1); }
#else
  public ArraySegment<byte>? GetDescBytes() { return __p.__vector_as_arraysegment(6); }
#endif
  public byte[] GetDescArray() { return __p.__vector_as_array<byte>(6); }
  public cfg.BlueprintClazz ParentsType(int j) { int o = __p.__offset(8); return o != 0 ? (cfg.BlueprintClazz)__p.bb.Get(__p.__vector(o) + j * 1) : (cfg.BlueprintClazz)0; }
  public int ParentsTypeLength { get { int o = __p.__offset(8); return o != 0 ? __p.__vector_len(o) : 0; } }
#if ENABLE_SPAN_T
  public Span<cfg.BlueprintClazz> GetParentsTypeBytes() { return __p.__vector_as_span<cfg.BlueprintClazz>(8, 1); }
#else
  public ArraySegment<byte>? GetParentsTypeBytes() { return __p.__vector_as_arraysegment(8); }
#endif
  public cfg.BlueprintClazz[] GetParentsTypeArray() { int o = __p.__offset(8); if (o == 0) return null; int p = __p.__vector(o); int l = __p.__vector_len(o); cfg.BlueprintClazz[] a = new cfg.BlueprintClazz[l]; for (int i = 0; i < l; i++) { a[i] = (cfg.BlueprintClazz)__p.bb.Get(p + i * 1); } return a; }
  public TTable? Parents<TTable>(int j) where TTable : struct, IFlatbufferObject { int o = __p.__offset(10); return o != 0 ? (TTable?)__p.__union<TTable>(__p.__vector(o) + j * 4) : null; }
  public int ParentsLength { get { int o = __p.__offset(10); return o != 0 ? __p.__vector_len(o) : 0; } }
  public cfg.BlueprintMethod MethodsType(int j) { int o = __p.__offset(12); return o != 0 ? (cfg.BlueprintMethod)__p.bb.Get(__p.__vector(o) + j * 1) : (cfg.BlueprintMethod)0; }
  public int MethodsTypeLength { get { int o = __p.__offset(12); return o != 0 ? __p.__vector_len(o) : 0; } }
#if ENABLE_SPAN_T
  public Span<cfg.BlueprintMethod> GetMethodsTypeBytes() { return __p.__vector_as_span<cfg.BlueprintMethod>(12, 1); }
#else
  public ArraySegment<byte>? GetMethodsTypeBytes() { return __p.__vector_as_arraysegment(12); }
#endif
  public cfg.BlueprintMethod[] GetMethodsTypeArray() { int o = __p.__offset(12); if (o == 0) return null; int p = __p.__vector(o); int l = __p.__vector_len(o); cfg.BlueprintMethod[] a = new cfg.BlueprintMethod[l]; for (int i = 0; i < l; i++) { a[i] = (cfg.BlueprintMethod)__p.bb.Get(p + i * 1); } return a; }
  public TTable? Methods<TTable>(int j) where TTable : struct, IFlatbufferObject { int o = __p.__offset(14); return o != 0 ? (TTable?)__p.__union<TTable>(__p.__vector(o) + j * 4) : null; }
  public int MethodsLength { get { int o = __p.__offset(14); return o != 0 ? __p.__vector_len(o) : 0; } }
  public cfg.BlueprintEnumField? Enums(int j) { int o = __p.__offset(16); return o != 0 ? (cfg.BlueprintEnumField?)(new cfg.BlueprintEnumField()).__assign(__p.__indirect(__p.__vector(o) + j * 4), __p.bb) : null; }
  public int EnumsLength { get { int o = __p.__offset(16); return o != 0 ? __p.__vector_len(o) : 0; } }

  public static Offset<cfg.BlueprintEnumClazz> CreateBlueprintEnumClazz(FlatBufferBuilder builder,
      StringOffset nameOffset = default(StringOffset),
      StringOffset descOffset = default(StringOffset),
      VectorOffset parents_typeOffset = default(VectorOffset),
      VectorOffset parentsOffset = default(VectorOffset),
      VectorOffset methods_typeOffset = default(VectorOffset),
      VectorOffset methodsOffset = default(VectorOffset),
      VectorOffset enumsOffset = default(VectorOffset)) {
    builder.StartTable(7);
    BlueprintEnumClazz.AddEnums(builder, enumsOffset);
    BlueprintEnumClazz.AddMethods(builder, methodsOffset);
    BlueprintEnumClazz.AddMethodsType(builder, methods_typeOffset);
    BlueprintEnumClazz.AddParents(builder, parentsOffset);
    BlueprintEnumClazz.AddParentsType(builder, parents_typeOffset);
    BlueprintEnumClazz.AddDesc(builder, descOffset);
    BlueprintEnumClazz.AddName(builder, nameOffset);
    return BlueprintEnumClazz.EndBlueprintEnumClazz(builder);
  }

  public static void StartBlueprintEnumClazz(FlatBufferBuilder builder) { builder.StartTable(7); }
  public static void AddName(FlatBufferBuilder builder, StringOffset nameOffset) { builder.AddOffset(0, nameOffset.Value, 0); }
  public static void AddDesc(FlatBufferBuilder builder, StringOffset descOffset) { builder.AddOffset(1, descOffset.Value, 0); }
  public static void AddParentsType(FlatBufferBuilder builder, VectorOffset parentsTypeOffset) { builder.AddOffset(2, parentsTypeOffset.Value, 0); }
  public static VectorOffset CreateParentsTypeVector(FlatBufferBuilder builder, cfg.BlueprintClazz[] data) { builder.StartVector(1, data.Length, 1); for (int i = data.Length - 1; i >= 0; i--) builder.AddByte((byte)data[i]); return builder.EndVector(); }
  public static VectorOffset CreateParentsTypeVectorBlock(FlatBufferBuilder builder, cfg.BlueprintClazz[] data) { builder.StartVector(1, data.Length, 1); builder.Add(data); return builder.EndVector(); }
  public static void StartParentsTypeVector(FlatBufferBuilder builder, int numElems) { builder.StartVector(1, numElems, 1); }
  public static void AddParents(FlatBufferBuilder builder, VectorOffset parentsOffset) { builder.AddOffset(3, parentsOffset.Value, 0); }
  public static VectorOffset CreateParentsVector(FlatBufferBuilder builder, int[] data) { builder.StartVector(4, data.Length, 4); for (int i = data.Length - 1; i >= 0; i--) builder.AddOffset(data[i]); return builder.EndVector(); }
  public static VectorOffset CreateParentsVectorBlock(FlatBufferBuilder builder, int[] data) { builder.StartVector(4, data.Length, 4); builder.Add(data); return builder.EndVector(); }
  public static void StartParentsVector(FlatBufferBuilder builder, int numElems) { builder.StartVector(4, numElems, 4); }
  public static void AddMethodsType(FlatBufferBuilder builder, VectorOffset methodsTypeOffset) { builder.AddOffset(4, methodsTypeOffset.Value, 0); }
  public static VectorOffset CreateMethodsTypeVector(FlatBufferBuilder builder, cfg.BlueprintMethod[] data) { builder.StartVector(1, data.Length, 1); for (int i = data.Length - 1; i >= 0; i--) builder.AddByte((byte)data[i]); return builder.EndVector(); }
  public static VectorOffset CreateMethodsTypeVectorBlock(FlatBufferBuilder builder, cfg.BlueprintMethod[] data) { builder.StartVector(1, data.Length, 1); builder.Add(data); return builder.EndVector(); }
  public static void StartMethodsTypeVector(FlatBufferBuilder builder, int numElems) { builder.StartVector(1, numElems, 1); }
  public static void AddMethods(FlatBufferBuilder builder, VectorOffset methodsOffset) { builder.AddOffset(5, methodsOffset.Value, 0); }
  public static VectorOffset CreateMethodsVector(FlatBufferBuilder builder, int[] data) { builder.StartVector(4, data.Length, 4); for (int i = data.Length - 1; i >= 0; i--) builder.AddOffset(data[i]); return builder.EndVector(); }
  public static VectorOffset CreateMethodsVectorBlock(FlatBufferBuilder builder, int[] data) { builder.StartVector(4, data.Length, 4); builder.Add(data); return builder.EndVector(); }
  public static void StartMethodsVector(FlatBufferBuilder builder, int numElems) { builder.StartVector(4, numElems, 4); }
  public static void AddEnums(FlatBufferBuilder builder, VectorOffset enumsOffset) { builder.AddOffset(6, enumsOffset.Value, 0); }
  public static VectorOffset CreateEnumsVector(FlatBufferBuilder builder, Offset<cfg.BlueprintEnumField>[] data) { builder.StartVector(4, data.Length, 4); for (int i = data.Length - 1; i >= 0; i--) builder.AddOffset(data[i].Value); return builder.EndVector(); }
  public static VectorOffset CreateEnumsVectorBlock(FlatBufferBuilder builder, Offset<cfg.BlueprintEnumField>[] data) { builder.StartVector(4, data.Length, 4); builder.Add(data); return builder.EndVector(); }
  public static void StartEnumsVector(FlatBufferBuilder builder, int numElems) { builder.StartVector(4, numElems, 4); }
  public static Offset<cfg.BlueprintEnumClazz> EndBlueprintEnumClazz(FlatBufferBuilder builder) {
    int o = builder.EndTable();
    builder.Required(o, 8);  // parents_type
    builder.Required(o, 10);  // parents
    builder.Required(o, 12);  // methods_type
    builder.Required(o, 14);  // methods
    builder.Required(o, 16);  // enums
    return new Offset<cfg.BlueprintEnumClazz>(o);
  }
};


}
