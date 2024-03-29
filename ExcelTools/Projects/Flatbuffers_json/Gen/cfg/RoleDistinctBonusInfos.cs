// <auto-generated>
//  automatically generated by the FlatBuffers compiler, do not modify
// </auto-generated>

namespace cfg
{

using global::System;
using global::System.Collections.Generic;
using global::FlatBuffers;

public struct RoleDistinctBonusInfos : IFlatbufferObject
{
  private Table __p;
  public ByteBuffer ByteBuffer { get { return __p.bb; } }
  public static void ValidateVersion() { FlatBufferConstants.FLATBUFFERS_2_0_0(); }
  public static RoleDistinctBonusInfos GetRootAsRoleDistinctBonusInfos(ByteBuffer _bb) { return GetRootAsRoleDistinctBonusInfos(_bb, new RoleDistinctBonusInfos()); }
  public static RoleDistinctBonusInfos GetRootAsRoleDistinctBonusInfos(ByteBuffer _bb, RoleDistinctBonusInfos obj) { return (obj.__assign(_bb.GetInt(_bb.Position) + _bb.Position, _bb)); }
  public void __init(int _i, ByteBuffer _bb) { __p = new Table(_i, _bb); }
  public RoleDistinctBonusInfos __assign(int _i, ByteBuffer _bb) { __init(_i, _bb); return this; }

  public int EffectiveLevel { get { int o = __p.__offset(4); return o != 0 ? __p.bb.GetInt(o + __p.bb_pos) : (int)0; } }
  public cfg.RoleBonusInfo? BonusInfo(int j) { int o = __p.__offset(6); return o != 0 ? (cfg.RoleBonusInfo?)(new cfg.RoleBonusInfo()).__assign(__p.__indirect(__p.__vector(o) + j * 4), __p.bb) : null; }
  public int BonusInfoLength { get { int o = __p.__offset(6); return o != 0 ? __p.__vector_len(o) : 0; } }

  public static Offset<cfg.RoleDistinctBonusInfos> CreateRoleDistinctBonusInfos(FlatBufferBuilder builder,
      int effective_level = 0,
      VectorOffset bonus_infoOffset = default(VectorOffset)) {
    builder.StartTable(2);
    RoleDistinctBonusInfos.AddBonusInfo(builder, bonus_infoOffset);
    RoleDistinctBonusInfos.AddEffectiveLevel(builder, effective_level);
    return RoleDistinctBonusInfos.EndRoleDistinctBonusInfos(builder);
  }

  public static void StartRoleDistinctBonusInfos(FlatBufferBuilder builder) { builder.StartTable(2); }
  public static void AddEffectiveLevel(FlatBufferBuilder builder, int effectiveLevel) { builder.AddInt(0, effectiveLevel, 0); }
  public static void AddBonusInfo(FlatBufferBuilder builder, VectorOffset bonusInfoOffset) { builder.AddOffset(1, bonusInfoOffset.Value, 0); }
  public static VectorOffset CreateBonusInfoVector(FlatBufferBuilder builder, Offset<cfg.RoleBonusInfo>[] data) { builder.StartVector(4, data.Length, 4); for (int i = data.Length - 1; i >= 0; i--) builder.AddOffset(data[i].Value); return builder.EndVector(); }
  public static VectorOffset CreateBonusInfoVectorBlock(FlatBufferBuilder builder, Offset<cfg.RoleBonusInfo>[] data) { builder.StartVector(4, data.Length, 4); builder.Add(data); return builder.EndVector(); }
  public static void StartBonusInfoVector(FlatBufferBuilder builder, int numElems) { builder.StartVector(4, numElems, 4); }
  public static Offset<cfg.RoleDistinctBonusInfos> EndRoleDistinctBonusInfos(FlatBufferBuilder builder) {
    int o = builder.EndTable();
    builder.Required(o, 6);  // bonus_info
    return new Offset<cfg.RoleDistinctBonusInfos>(o);
  }
};


}
