// <auto-generated>
//  automatically generated by the FlatBuffers compiler, do not modify
// </auto-generated>

namespace cfg
{

using global::System;
using global::System.Collections.Generic;
using global::FlatBuffers;

public struct BonusOneItem : IFlatbufferObject
{
  private Table __p;
  public ByteBuffer ByteBuffer { get { return __p.bb; } }
  public static void ValidateVersion() { FlatBufferConstants.FLATBUFFERS_2_0_0(); }
  public static BonusOneItem GetRootAsBonusOneItem(ByteBuffer _bb) { return GetRootAsBonusOneItem(_bb, new BonusOneItem()); }
  public static BonusOneItem GetRootAsBonusOneItem(ByteBuffer _bb, BonusOneItem obj) { return (obj.__assign(_bb.GetInt(_bb.Position) + _bb.Position, _bb)); }
  public void __init(int _i, ByteBuffer _bb) { __p = new Table(_i, _bb); }
  public BonusOneItem __assign(int _i, ByteBuffer _bb) { __init(_i, _bb); return this; }

  public int ItemId { get { int o = __p.__offset(4); return o != 0 ? __p.bb.GetInt(o + __p.bb_pos) : (int)0; } }

  public static Offset<cfg.BonusOneItem> CreateBonusOneItem(FlatBufferBuilder builder,
      int item_id = 0) {
    builder.StartTable(1);
    BonusOneItem.AddItemId(builder, item_id);
    return BonusOneItem.EndBonusOneItem(builder);
  }

  public static void StartBonusOneItem(FlatBufferBuilder builder) { builder.StartTable(1); }
  public static void AddItemId(FlatBufferBuilder builder, int itemId) { builder.AddInt(0, itemId, 0); }
  public static Offset<cfg.BonusOneItem> EndBonusOneItem(FlatBufferBuilder builder) {
    int o = builder.EndTable();
    return new Offset<cfg.BonusOneItem>(o);
  }
};


}
