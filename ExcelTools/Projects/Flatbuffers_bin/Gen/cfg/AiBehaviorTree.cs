// <auto-generated>
//  automatically generated by the FlatBuffers compiler, do not modify
// </auto-generated>

namespace cfg
{

using global::System;
using global::System.Collections.Generic;
using global::FlatBuffers;

public struct AiBehaviorTree : IFlatbufferObject
{
  private Table __p;
  public ByteBuffer ByteBuffer { get { return __p.bb; } }
  public static void ValidateVersion() { FlatBufferConstants.FLATBUFFERS_2_0_0(); }
  public static AiBehaviorTree GetRootAsAiBehaviorTree(ByteBuffer _bb) { return GetRootAsAiBehaviorTree(_bb, new AiBehaviorTree()); }
  public static AiBehaviorTree GetRootAsAiBehaviorTree(ByteBuffer _bb, AiBehaviorTree obj) { return (obj.__assign(_bb.GetInt(_bb.Position) + _bb.Position, _bb)); }
  public void __init(int _i, ByteBuffer _bb) { __p = new Table(_i, _bb); }
  public AiBehaviorTree __assign(int _i, ByteBuffer _bb) { __init(_i, _bb); return this; }

  public int Id { get { int o = __p.__offset(4); return o != 0 ? __p.bb.GetInt(o + __p.bb_pos) : (int)0; } }
  public string Name { get { int o = __p.__offset(6); return o != 0 ? __p.__string(o + __p.bb_pos) : null; } }
#if ENABLE_SPAN_T
  public Span<byte> GetNameBytes() { return __p.__vector_as_span<byte>(6, 1); }
#else
  public ArraySegment<byte>? GetNameBytes() { return __p.__vector_as_arraysegment(6); }
#endif
  public byte[] GetNameArray() { return __p.__vector_as_array<byte>(6); }
  public string Desc { get { int o = __p.__offset(8); return o != 0 ? __p.__string(o + __p.bb_pos) : null; } }
#if ENABLE_SPAN_T
  public Span<byte> GetDescBytes() { return __p.__vector_as_span<byte>(8, 1); }
#else
  public ArraySegment<byte>? GetDescBytes() { return __p.__vector_as_arraysegment(8); }
#endif
  public byte[] GetDescArray() { return __p.__vector_as_array<byte>(8); }
  public string BlackboardId { get { int o = __p.__offset(10); return o != 0 ? __p.__string(o + __p.bb_pos) : null; } }
#if ENABLE_SPAN_T
  public Span<byte> GetBlackboardIdBytes() { return __p.__vector_as_span<byte>(10, 1); }
#else
  public ArraySegment<byte>? GetBlackboardIdBytes() { return __p.__vector_as_arraysegment(10); }
#endif
  public byte[] GetBlackboardIdArray() { return __p.__vector_as_array<byte>(10); }
  public cfg.AiComposeNode RootType { get { int o = __p.__offset(12); return o != 0 ? (cfg.AiComposeNode)__p.bb.Get(o + __p.bb_pos) : cfg.AiComposeNode.NONE; } }
  public TTable? Root<TTable>() where TTable : struct, IFlatbufferObject { int o = __p.__offset(14); return o != 0 ? (TTable?)__p.__union<TTable>(o + __p.bb_pos) : null; }
  public cfg.AiSequence RootAsAiSequence() { return Root<cfg.AiSequence>().Value; }
  public cfg.AiSelector RootAsAiSelector() { return Root<cfg.AiSelector>().Value; }
  public cfg.AiSimpleParallel RootAsAiSimpleParallel() { return Root<cfg.AiSimpleParallel>().Value; }

  public static Offset<cfg.AiBehaviorTree> CreateAiBehaviorTree(FlatBufferBuilder builder,
      int id = 0,
      StringOffset nameOffset = default(StringOffset),
      StringOffset descOffset = default(StringOffset),
      StringOffset blackboard_idOffset = default(StringOffset),
      cfg.AiComposeNode root_type = cfg.AiComposeNode.NONE,
      int rootOffset = 0) {
    builder.StartTable(6);
    AiBehaviorTree.AddRoot(builder, rootOffset);
    AiBehaviorTree.AddBlackboardId(builder, blackboard_idOffset);
    AiBehaviorTree.AddDesc(builder, descOffset);
    AiBehaviorTree.AddName(builder, nameOffset);
    AiBehaviorTree.AddId(builder, id);
    AiBehaviorTree.AddRootType(builder, root_type);
    return AiBehaviorTree.EndAiBehaviorTree(builder);
  }

  public static void StartAiBehaviorTree(FlatBufferBuilder builder) { builder.StartTable(6); }
  public static void AddId(FlatBufferBuilder builder, int id) { builder.AddInt(0, id, 0); }
  public static void AddName(FlatBufferBuilder builder, StringOffset nameOffset) { builder.AddOffset(1, nameOffset.Value, 0); }
  public static void AddDesc(FlatBufferBuilder builder, StringOffset descOffset) { builder.AddOffset(2, descOffset.Value, 0); }
  public static void AddBlackboardId(FlatBufferBuilder builder, StringOffset blackboardIdOffset) { builder.AddOffset(3, blackboardIdOffset.Value, 0); }
  public static void AddRootType(FlatBufferBuilder builder, cfg.AiComposeNode rootType) { builder.AddByte(4, (byte)rootType, 0); }
  public static void AddRoot(FlatBufferBuilder builder, int rootOffset) { builder.AddOffset(5, rootOffset, 0); }
  public static Offset<cfg.AiBehaviorTree> EndAiBehaviorTree(FlatBufferBuilder builder) {
    int o = builder.EndTable();
    builder.Required(o, 14);  // root
    return new Offset<cfg.AiBehaviorTree>(o);
  }
};


}
