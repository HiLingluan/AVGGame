// <auto-generated>
//  automatically generated by the FlatBuffers compiler, do not modify
// </auto-generated>

namespace cfg
{

using global::System;
using global::System.Collections.Generic;
using global::FlatBuffers;

public struct AiSelector : IFlatbufferObject
{
  private Table __p;
  public ByteBuffer ByteBuffer { get { return __p.bb; } }
  public static void ValidateVersion() { FlatBufferConstants.FLATBUFFERS_2_0_0(); }
  public static AiSelector GetRootAsAiSelector(ByteBuffer _bb) { return GetRootAsAiSelector(_bb, new AiSelector()); }
  public static AiSelector GetRootAsAiSelector(ByteBuffer _bb, AiSelector obj) { return (obj.__assign(_bb.GetInt(_bb.Position) + _bb.Position, _bb)); }
  public void __init(int _i, ByteBuffer _bb) { __p = new Table(_i, _bb); }
  public AiSelector __assign(int _i, ByteBuffer _bb) { __init(_i, _bb); return this; }

  public int Id { get { int o = __p.__offset(4); return o != 0 ? __p.bb.GetInt(o + __p.bb_pos) : (int)0; } }
  public string NodeName { get { int o = __p.__offset(6); return o != 0 ? __p.__string(o + __p.bb_pos) : null; } }
#if ENABLE_SPAN_T
  public Span<byte> GetNodeNameBytes() { return __p.__vector_as_span<byte>(6, 1); }
#else
  public ArraySegment<byte>? GetNodeNameBytes() { return __p.__vector_as_arraysegment(6); }
#endif
  public byte[] GetNodeNameArray() { return __p.__vector_as_array<byte>(6); }
  public cfg.AiDecorator DecoratorsType(int j) { int o = __p.__offset(8); return o != 0 ? (cfg.AiDecorator)__p.bb.Get(__p.__vector(o) + j * 1) : (cfg.AiDecorator)0; }
  public int DecoratorsTypeLength { get { int o = __p.__offset(8); return o != 0 ? __p.__vector_len(o) : 0; } }
#if ENABLE_SPAN_T
  public Span<cfg.AiDecorator> GetDecoratorsTypeBytes() { return __p.__vector_as_span<cfg.AiDecorator>(8, 1); }
#else
  public ArraySegment<byte>? GetDecoratorsTypeBytes() { return __p.__vector_as_arraysegment(8); }
#endif
  public cfg.AiDecorator[] GetDecoratorsTypeArray() { int o = __p.__offset(8); if (o == 0) return null; int p = __p.__vector(o); int l = __p.__vector_len(o); cfg.AiDecorator[] a = new cfg.AiDecorator[l]; for (int i = 0; i < l; i++) { a[i] = (cfg.AiDecorator)__p.bb.Get(p + i * 1); } return a; }
  public TTable? Decorators<TTable>(int j) where TTable : struct, IFlatbufferObject { int o = __p.__offset(10); return o != 0 ? (TTable?)__p.__union<TTable>(__p.__vector(o) + j * 4) : null; }
  public int DecoratorsLength { get { int o = __p.__offset(10); return o != 0 ? __p.__vector_len(o) : 0; } }
  public cfg.AiService ServicesType(int j) { int o = __p.__offset(12); return o != 0 ? (cfg.AiService)__p.bb.Get(__p.__vector(o) + j * 1) : (cfg.AiService)0; }
  public int ServicesTypeLength { get { int o = __p.__offset(12); return o != 0 ? __p.__vector_len(o) : 0; } }
#if ENABLE_SPAN_T
  public Span<cfg.AiService> GetServicesTypeBytes() { return __p.__vector_as_span<cfg.AiService>(12, 1); }
#else
  public ArraySegment<byte>? GetServicesTypeBytes() { return __p.__vector_as_arraysegment(12); }
#endif
  public cfg.AiService[] GetServicesTypeArray() { int o = __p.__offset(12); if (o == 0) return null; int p = __p.__vector(o); int l = __p.__vector_len(o); cfg.AiService[] a = new cfg.AiService[l]; for (int i = 0; i < l; i++) { a[i] = (cfg.AiService)__p.bb.Get(p + i * 1); } return a; }
  public TTable? Services<TTable>(int j) where TTable : struct, IFlatbufferObject { int o = __p.__offset(14); return o != 0 ? (TTable?)__p.__union<TTable>(__p.__vector(o) + j * 4) : null; }
  public int ServicesLength { get { int o = __p.__offset(14); return o != 0 ? __p.__vector_len(o) : 0; } }
  public cfg.AiFlowNode ChildrenType(int j) { int o = __p.__offset(16); return o != 0 ? (cfg.AiFlowNode)__p.bb.Get(__p.__vector(o) + j * 1) : (cfg.AiFlowNode)0; }
  public int ChildrenTypeLength { get { int o = __p.__offset(16); return o != 0 ? __p.__vector_len(o) : 0; } }
#if ENABLE_SPAN_T
  public Span<cfg.AiFlowNode> GetChildrenTypeBytes() { return __p.__vector_as_span<cfg.AiFlowNode>(16, 1); }
#else
  public ArraySegment<byte>? GetChildrenTypeBytes() { return __p.__vector_as_arraysegment(16); }
#endif
  public cfg.AiFlowNode[] GetChildrenTypeArray() { int o = __p.__offset(16); if (o == 0) return null; int p = __p.__vector(o); int l = __p.__vector_len(o); cfg.AiFlowNode[] a = new cfg.AiFlowNode[l]; for (int i = 0; i < l; i++) { a[i] = (cfg.AiFlowNode)__p.bb.Get(p + i * 1); } return a; }
  public TTable? Children<TTable>(int j) where TTable : struct, IFlatbufferObject { int o = __p.__offset(18); return o != 0 ? (TTable?)__p.__union<TTable>(__p.__vector(o) + j * 4) : null; }
  public int ChildrenLength { get { int o = __p.__offset(18); return o != 0 ? __p.__vector_len(o) : 0; } }

  public static Offset<cfg.AiSelector> CreateAiSelector(FlatBufferBuilder builder,
      int id = 0,
      StringOffset node_nameOffset = default(StringOffset),
      VectorOffset decorators_typeOffset = default(VectorOffset),
      VectorOffset decoratorsOffset = default(VectorOffset),
      VectorOffset services_typeOffset = default(VectorOffset),
      VectorOffset servicesOffset = default(VectorOffset),
      VectorOffset children_typeOffset = default(VectorOffset),
      VectorOffset childrenOffset = default(VectorOffset)) {
    builder.StartTable(8);
    AiSelector.AddChildren(builder, childrenOffset);
    AiSelector.AddChildrenType(builder, children_typeOffset);
    AiSelector.AddServices(builder, servicesOffset);
    AiSelector.AddServicesType(builder, services_typeOffset);
    AiSelector.AddDecorators(builder, decoratorsOffset);
    AiSelector.AddDecoratorsType(builder, decorators_typeOffset);
    AiSelector.AddNodeName(builder, node_nameOffset);
    AiSelector.AddId(builder, id);
    return AiSelector.EndAiSelector(builder);
  }

  public static void StartAiSelector(FlatBufferBuilder builder) { builder.StartTable(8); }
  public static void AddId(FlatBufferBuilder builder, int id) { builder.AddInt(0, id, 0); }
  public static void AddNodeName(FlatBufferBuilder builder, StringOffset nodeNameOffset) { builder.AddOffset(1, nodeNameOffset.Value, 0); }
  public static void AddDecoratorsType(FlatBufferBuilder builder, VectorOffset decoratorsTypeOffset) { builder.AddOffset(2, decoratorsTypeOffset.Value, 0); }
  public static VectorOffset CreateDecoratorsTypeVector(FlatBufferBuilder builder, cfg.AiDecorator[] data) { builder.StartVector(1, data.Length, 1); for (int i = data.Length - 1; i >= 0; i--) builder.AddByte((byte)data[i]); return builder.EndVector(); }
  public static VectorOffset CreateDecoratorsTypeVectorBlock(FlatBufferBuilder builder, cfg.AiDecorator[] data) { builder.StartVector(1, data.Length, 1); builder.Add(data); return builder.EndVector(); }
  public static void StartDecoratorsTypeVector(FlatBufferBuilder builder, int numElems) { builder.StartVector(1, numElems, 1); }
  public static void AddDecorators(FlatBufferBuilder builder, VectorOffset decoratorsOffset) { builder.AddOffset(3, decoratorsOffset.Value, 0); }
  public static VectorOffset CreateDecoratorsVector(FlatBufferBuilder builder, int[] data) { builder.StartVector(4, data.Length, 4); for (int i = data.Length - 1; i >= 0; i--) builder.AddOffset(data[i]); return builder.EndVector(); }
  public static VectorOffset CreateDecoratorsVectorBlock(FlatBufferBuilder builder, int[] data) { builder.StartVector(4, data.Length, 4); builder.Add(data); return builder.EndVector(); }
  public static void StartDecoratorsVector(FlatBufferBuilder builder, int numElems) { builder.StartVector(4, numElems, 4); }
  public static void AddServicesType(FlatBufferBuilder builder, VectorOffset servicesTypeOffset) { builder.AddOffset(4, servicesTypeOffset.Value, 0); }
  public static VectorOffset CreateServicesTypeVector(FlatBufferBuilder builder, cfg.AiService[] data) { builder.StartVector(1, data.Length, 1); for (int i = data.Length - 1; i >= 0; i--) builder.AddByte((byte)data[i]); return builder.EndVector(); }
  public static VectorOffset CreateServicesTypeVectorBlock(FlatBufferBuilder builder, cfg.AiService[] data) { builder.StartVector(1, data.Length, 1); builder.Add(data); return builder.EndVector(); }
  public static void StartServicesTypeVector(FlatBufferBuilder builder, int numElems) { builder.StartVector(1, numElems, 1); }
  public static void AddServices(FlatBufferBuilder builder, VectorOffset servicesOffset) { builder.AddOffset(5, servicesOffset.Value, 0); }
  public static VectorOffset CreateServicesVector(FlatBufferBuilder builder, int[] data) { builder.StartVector(4, data.Length, 4); for (int i = data.Length - 1; i >= 0; i--) builder.AddOffset(data[i]); return builder.EndVector(); }
  public static VectorOffset CreateServicesVectorBlock(FlatBufferBuilder builder, int[] data) { builder.StartVector(4, data.Length, 4); builder.Add(data); return builder.EndVector(); }
  public static void StartServicesVector(FlatBufferBuilder builder, int numElems) { builder.StartVector(4, numElems, 4); }
  public static void AddChildrenType(FlatBufferBuilder builder, VectorOffset childrenTypeOffset) { builder.AddOffset(6, childrenTypeOffset.Value, 0); }
  public static VectorOffset CreateChildrenTypeVector(FlatBufferBuilder builder, cfg.AiFlowNode[] data) { builder.StartVector(1, data.Length, 1); for (int i = data.Length - 1; i >= 0; i--) builder.AddByte((byte)data[i]); return builder.EndVector(); }
  public static VectorOffset CreateChildrenTypeVectorBlock(FlatBufferBuilder builder, cfg.AiFlowNode[] data) { builder.StartVector(1, data.Length, 1); builder.Add(data); return builder.EndVector(); }
  public static void StartChildrenTypeVector(FlatBufferBuilder builder, int numElems) { builder.StartVector(1, numElems, 1); }
  public static void AddChildren(FlatBufferBuilder builder, VectorOffset childrenOffset) { builder.AddOffset(7, childrenOffset.Value, 0); }
  public static VectorOffset CreateChildrenVector(FlatBufferBuilder builder, int[] data) { builder.StartVector(4, data.Length, 4); for (int i = data.Length - 1; i >= 0; i--) builder.AddOffset(data[i]); return builder.EndVector(); }
  public static VectorOffset CreateChildrenVectorBlock(FlatBufferBuilder builder, int[] data) { builder.StartVector(4, data.Length, 4); builder.Add(data); return builder.EndVector(); }
  public static void StartChildrenVector(FlatBufferBuilder builder, int numElems) { builder.StartVector(4, numElems, 4); }
  public static Offset<cfg.AiSelector> EndAiSelector(FlatBufferBuilder builder) {
    int o = builder.EndTable();
    builder.Required(o, 8);  // decorators_type
    builder.Required(o, 10);  // decorators
    builder.Required(o, 12);  // services_type
    builder.Required(o, 14);  // services
    builder.Required(o, 16);  // children_type
    builder.Required(o, 18);  // children
    return new Offset<cfg.AiSelector>(o);
  }
};


}
