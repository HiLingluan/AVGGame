// <auto-generated>
//  automatically generated by the FlatBuffers compiler, do not modify
// </auto-generated>

namespace cfg
{

using global::System;
using global::System.Collections.Generic;
using global::FlatBuffers;

public struct ItemDymmy : IFlatbufferObject
{
  private Table __p;
  public ByteBuffer ByteBuffer { get { return __p.bb; } }
  public static void ValidateVersion() { FlatBufferConstants.FLATBUFFERS_2_0_0(); }
  public static ItemDymmy GetRootAsItemDymmy(ByteBuffer _bb) { return GetRootAsItemDymmy(_bb, new ItemDymmy()); }
  public static ItemDymmy GetRootAsItemDymmy(ByteBuffer _bb, ItemDymmy obj) { return (obj.__assign(_bb.GetInt(_bb.Position) + _bb.Position, _bb)); }
  public void __init(int _i, ByteBuffer _bb) { __p = new Table(_i, _bb); }
  public ItemDymmy __assign(int _i, ByteBuffer _bb) { __init(_i, _bb); return this; }

  public int Id { get { int o = __p.__offset(4); return o != 0 ? __p.bb.GetInt(o + __p.bb_pos) : (int)0; } }
  public cfg.CostCost CostType { get { int o = __p.__offset(6); return o != 0 ? (cfg.CostCost)__p.bb.Get(o + __p.bb_pos) : cfg.CostCost.NONE; } }
  public TTable? Cost<TTable>() where TTable : struct, IFlatbufferObject { int o = __p.__offset(8); return o != 0 ? (TTable?)__p.__union<TTable>(o + __p.bb_pos) : null; }
  public cfg.CostCostCurrency CostAsCostCostCurrency() { return Cost<cfg.CostCostCurrency>().Value; }
  public cfg.CostCostCurrencies CostAsCostCostCurrencies() { return Cost<cfg.CostCostCurrencies>().Value; }
  public cfg.CostCostOneItem CostAsCostCostOneItem() { return Cost<cfg.CostCostOneItem>().Value; }
  public cfg.CostCostItem CostAsCostCostItem() { return Cost<cfg.CostCostItem>().Value; }
  public cfg.CostCostItems CostAsCostCostItems() { return Cost<cfg.CostCostItems>().Value; }

  public static Offset<cfg.ItemDymmy> CreateItemDymmy(FlatBufferBuilder builder,
      int id = 0,
      cfg.CostCost cost_type = cfg.CostCost.NONE,
      int costOffset = 0) {
    builder.StartTable(3);
    ItemDymmy.AddCost(builder, costOffset);
    ItemDymmy.AddId(builder, id);
    ItemDymmy.AddCostType(builder, cost_type);
    return ItemDymmy.EndItemDymmy(builder);
  }

  public static void StartItemDymmy(FlatBufferBuilder builder) { builder.StartTable(3); }
  public static void AddId(FlatBufferBuilder builder, int id) { builder.AddInt(0, id, 0); }
  public static void AddCostType(FlatBufferBuilder builder, cfg.CostCost costType) { builder.AddByte(1, (byte)costType, 0); }
  public static void AddCost(FlatBufferBuilder builder, int costOffset) { builder.AddOffset(2, costOffset, 0); }
  public static Offset<cfg.ItemDymmy> EndItemDymmy(FlatBufferBuilder builder) {
    int o = builder.EndTable();
    builder.Required(o, 8);  // cost
    return new Offset<cfg.ItemDymmy>(o);
  }
};


}
