// <auto-generated>
//  automatically generated by the FlatBuffers compiler, do not modify
// </auto-generated>

namespace cfg
{

using global::System;
using global::System.Collections.Generic;
using global::FlatBuffers;

public struct ConditionMultiRoleCondition : IFlatbufferObject
{
  private Table __p;
  public ByteBuffer ByteBuffer { get { return __p.bb; } }
  public static void ValidateVersion() { FlatBufferConstants.FLATBUFFERS_2_0_0(); }
  public static ConditionMultiRoleCondition GetRootAsConditionMultiRoleCondition(ByteBuffer _bb) { return GetRootAsConditionMultiRoleCondition(_bb, new ConditionMultiRoleCondition()); }
  public static ConditionMultiRoleCondition GetRootAsConditionMultiRoleCondition(ByteBuffer _bb, ConditionMultiRoleCondition obj) { return (obj.__assign(_bb.GetInt(_bb.Position) + _bb.Position, _bb)); }
  public void __init(int _i, ByteBuffer _bb) { __p = new Table(_i, _bb); }
  public ConditionMultiRoleCondition __assign(int _i, ByteBuffer _bb) { __init(_i, _bb); return this; }

  public cfg.ConditionRoleCondition ConditionsType(int j) { int o = __p.__offset(4); return o != 0 ? (cfg.ConditionRoleCondition)__p.bb.Get(__p.__vector(o) + j * 1) : (cfg.ConditionRoleCondition)0; }
  public int ConditionsTypeLength { get { int o = __p.__offset(4); return o != 0 ? __p.__vector_len(o) : 0; } }
#if ENABLE_SPAN_T
  public Span<cfg.ConditionRoleCondition> GetConditionsTypeBytes() { return __p.__vector_as_span<cfg.ConditionRoleCondition>(4, 1); }
#else
  public ArraySegment<byte>? GetConditionsTypeBytes() { return __p.__vector_as_arraysegment(4); }
#endif
  public cfg.ConditionRoleCondition[] GetConditionsTypeArray() { int o = __p.__offset(4); if (o == 0) return null; int p = __p.__vector(o); int l = __p.__vector_len(o); cfg.ConditionRoleCondition[] a = new cfg.ConditionRoleCondition[l]; for (int i = 0; i < l; i++) { a[i] = (cfg.ConditionRoleCondition)__p.bb.Get(p + i * 1); } return a; }
  public TTable? Conditions<TTable>(int j) where TTable : struct, IFlatbufferObject { int o = __p.__offset(6); return o != 0 ? (TTable?)__p.__union<TTable>(__p.__vector(o) + j * 4) : null; }
  public int ConditionsLength { get { int o = __p.__offset(6); return o != 0 ? __p.__vector_len(o) : 0; } }

  public static Offset<cfg.ConditionMultiRoleCondition> CreateConditionMultiRoleCondition(FlatBufferBuilder builder,
      VectorOffset conditions_typeOffset = default(VectorOffset),
      VectorOffset conditionsOffset = default(VectorOffset)) {
    builder.StartTable(2);
    ConditionMultiRoleCondition.AddConditions(builder, conditionsOffset);
    ConditionMultiRoleCondition.AddConditionsType(builder, conditions_typeOffset);
    return ConditionMultiRoleCondition.EndConditionMultiRoleCondition(builder);
  }

  public static void StartConditionMultiRoleCondition(FlatBufferBuilder builder) { builder.StartTable(2); }
  public static void AddConditionsType(FlatBufferBuilder builder, VectorOffset conditionsTypeOffset) { builder.AddOffset(0, conditionsTypeOffset.Value, 0); }
  public static VectorOffset CreateConditionsTypeVector(FlatBufferBuilder builder, cfg.ConditionRoleCondition[] data) { builder.StartVector(1, data.Length, 1); for (int i = data.Length - 1; i >= 0; i--) builder.AddByte((byte)data[i]); return builder.EndVector(); }
  public static VectorOffset CreateConditionsTypeVectorBlock(FlatBufferBuilder builder, cfg.ConditionRoleCondition[] data) { builder.StartVector(1, data.Length, 1); builder.Add(data); return builder.EndVector(); }
  public static void StartConditionsTypeVector(FlatBufferBuilder builder, int numElems) { builder.StartVector(1, numElems, 1); }
  public static void AddConditions(FlatBufferBuilder builder, VectorOffset conditionsOffset) { builder.AddOffset(1, conditionsOffset.Value, 0); }
  public static VectorOffset CreateConditionsVector(FlatBufferBuilder builder, int[] data) { builder.StartVector(4, data.Length, 4); for (int i = data.Length - 1; i >= 0; i--) builder.AddOffset(data[i]); return builder.EndVector(); }
  public static VectorOffset CreateConditionsVectorBlock(FlatBufferBuilder builder, int[] data) { builder.StartVector(4, data.Length, 4); builder.Add(data); return builder.EndVector(); }
  public static void StartConditionsVector(FlatBufferBuilder builder, int numElems) { builder.StartVector(4, numElems, 4); }
  public static Offset<cfg.ConditionMultiRoleCondition> EndConditionMultiRoleCondition(FlatBufferBuilder builder) {
    int o = builder.EndTable();
    builder.Required(o, 4);  // conditions_type
    builder.Required(o, 6);  // conditions
    return new Offset<cfg.ConditionMultiRoleCondition>(o);
  }
};


}
