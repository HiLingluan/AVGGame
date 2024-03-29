// <auto-generated>
//  automatically generated by the FlatBuffers compiler, do not modify
// </auto-generated>

namespace cfg
{

using global::System;
using global::System.Collections.Generic;
using global::FlatBuffers;

public struct ItemItem : IFlatbufferObject
{
  private Table __p;
  public ByteBuffer ByteBuffer { get { return __p.bb; } }
  public static void ValidateVersion() { FlatBufferConstants.FLATBUFFERS_2_0_0(); }
  public static ItemItem GetRootAsItemItem(ByteBuffer _bb) { return GetRootAsItemItem(_bb, new ItemItem()); }
  public static ItemItem GetRootAsItemItem(ByteBuffer _bb, ItemItem obj) { return (obj.__assign(_bb.GetInt(_bb.Position) + _bb.Position, _bb)); }
  public void __init(int _i, ByteBuffer _bb) { __p = new Table(_i, _bb); }
  public ItemItem __assign(int _i, ByteBuffer _bb) { __init(_i, _bb); return this; }

  public int Id { get { int o = __p.__offset(4); return o != 0 ? __p.bb.GetInt(o + __p.bb_pos) : (int)0; } }
  public string Name { get { int o = __p.__offset(6); return o != 0 ? __p.__string(o + __p.bb_pos) : null; } }
#if ENABLE_SPAN_T
  public Span<byte> GetNameBytes() { return __p.__vector_as_span<byte>(6, 1); }
#else
  public ArraySegment<byte>? GetNameBytes() { return __p.__vector_as_arraysegment(6); }
#endif
  public byte[] GetNameArray() { return __p.__vector_as_array<byte>(6); }
  public cfg.ItemEMajorType MajorType { get { int o = __p.__offset(8); return o != 0 ? (cfg.ItemEMajorType)__p.bb.GetInt(o + __p.bb_pos) : cfg.ItemEMajorType.__GENERATE_DEFAULT_VALUE; } }
  public cfg.ItemEMinorType MinorType { get { int o = __p.__offset(10); return o != 0 ? (cfg.ItemEMinorType)__p.bb.GetInt(o + __p.bb_pos) : cfg.ItemEMinorType.__GENERATE_DEFAULT_VALUE; } }
  public int MaxPileNum { get { int o = __p.__offset(12); return o != 0 ? __p.bb.GetInt(o + __p.bb_pos) : (int)0; } }
  public cfg.ItemEItemQuality Quality { get { int o = __p.__offset(14); return o != 0 ? (cfg.ItemEItemQuality)__p.bb.GetInt(o + __p.bb_pos) : cfg.ItemEItemQuality.ItemEItemQuality_WHITE; } }
  public string Icon { get { int o = __p.__offset(16); return o != 0 ? __p.__string(o + __p.bb_pos) : null; } }
#if ENABLE_SPAN_T
  public Span<byte> GetIconBytes() { return __p.__vector_as_span<byte>(16, 1); }
#else
  public ArraySegment<byte>? GetIconBytes() { return __p.__vector_as_arraysegment(16); }
#endif
  public byte[] GetIconArray() { return __p.__vector_as_array<byte>(16); }
  public string IconBackgroud { get { int o = __p.__offset(18); return o != 0 ? __p.__string(o + __p.bb_pos) : null; } }
#if ENABLE_SPAN_T
  public Span<byte> GetIconBackgroudBytes() { return __p.__vector_as_span<byte>(18, 1); }
#else
  public ArraySegment<byte>? GetIconBackgroudBytes() { return __p.__vector_as_arraysegment(18); }
#endif
  public byte[] GetIconBackgroudArray() { return __p.__vector_as_array<byte>(18); }
  public string IconMask { get { int o = __p.__offset(20); return o != 0 ? __p.__string(o + __p.bb_pos) : null; } }
#if ENABLE_SPAN_T
  public Span<byte> GetIconMaskBytes() { return __p.__vector_as_span<byte>(20, 1); }
#else
  public ArraySegment<byte>? GetIconMaskBytes() { return __p.__vector_as_arraysegment(20); }
#endif
  public byte[] GetIconMaskArray() { return __p.__vector_as_array<byte>(20); }
  public string Desc { get { int o = __p.__offset(22); return o != 0 ? __p.__string(o + __p.bb_pos) : null; } }
#if ENABLE_SPAN_T
  public Span<byte> GetDescBytes() { return __p.__vector_as_span<byte>(22, 1); }
#else
  public ArraySegment<byte>? GetDescBytes() { return __p.__vector_as_arraysegment(22); }
#endif
  public byte[] GetDescArray() { return __p.__vector_as_array<byte>(22); }
  public int ShowOrder { get { int o = __p.__offset(24); return o != 0 ? __p.bb.GetInt(o + __p.bb_pos) : (int)0; } }
  public string Quantifier { get { int o = __p.__offset(26); return o != 0 ? __p.__string(o + __p.bb_pos) : null; } }
#if ENABLE_SPAN_T
  public Span<byte> GetQuantifierBytes() { return __p.__vector_as_span<byte>(26, 1); }
#else
  public ArraySegment<byte>? GetQuantifierBytes() { return __p.__vector_as_arraysegment(26); }
#endif
  public byte[] GetQuantifierArray() { return __p.__vector_as_array<byte>(26); }
  public bool ShowInBag { get { int o = __p.__offset(28); return o != 0 ? 0!=__p.bb.Get(o + __p.bb_pos) : (bool)false; } }
  public int MinShowLevel { get { int o = __p.__offset(30); return o != 0 ? __p.bb.GetInt(o + __p.bb_pos) : (int)0; } }
  public bool BatchUsable { get { int o = __p.__offset(32); return o != 0 ? 0!=__p.bb.Get(o + __p.bb_pos) : (bool)false; } }
  public float ProgressTimeWhenUse { get { int o = __p.__offset(34); return o != 0 ? __p.bb.GetFloat(o + __p.bb_pos) : (float)0.0f; } }
  public bool ShowHintWhenUse { get { int o = __p.__offset(36); return o != 0 ? 0!=__p.bb.Get(o + __p.bb_pos) : (bool)false; } }
  public bool Droppable { get { int o = __p.__offset(38); return o != 0 ? 0!=__p.bb.Get(o + __p.bb_pos) : (bool)false; } }
  public int Price { get { int o = __p.__offset(40); return o != 0 ? __p.bb.GetInt(o + __p.bb_pos) : (int)0; } }
  public cfg.ItemEUseType UseType { get { int o = __p.__offset(42); return o != 0 ? (cfg.ItemEUseType)__p.bb.GetInt(o + __p.bb_pos) : cfg.ItemEUseType.ItemEUseType_MANUAL; } }
  public int LevelUpId { get { int o = __p.__offset(44); return o != 0 ? __p.bb.GetInt(o + __p.bb_pos) : (int)0; } }

  public static Offset<cfg.ItemItem> CreateItemItem(FlatBufferBuilder builder,
      int id = 0,
      StringOffset nameOffset = default(StringOffset),
      cfg.ItemEMajorType major_type = cfg.ItemEMajorType.__GENERATE_DEFAULT_VALUE,
      cfg.ItemEMinorType minor_type = cfg.ItemEMinorType.__GENERATE_DEFAULT_VALUE,
      int max_pile_num = 0,
      cfg.ItemEItemQuality quality = cfg.ItemEItemQuality.ItemEItemQuality_WHITE,
      StringOffset iconOffset = default(StringOffset),
      StringOffset icon_backgroudOffset = default(StringOffset),
      StringOffset icon_maskOffset = default(StringOffset),
      StringOffset descOffset = default(StringOffset),
      int show_order = 0,
      StringOffset quantifierOffset = default(StringOffset),
      bool show_in_bag = false,
      int min_show_level = 0,
      bool batch_usable = false,
      float progress_time_when_use = 0.0f,
      bool show_hint_when_use = false,
      bool droppable = false,
      int price = 0,
      cfg.ItemEUseType use_type = cfg.ItemEUseType.ItemEUseType_MANUAL,
      int level_up_id = 0) {
    builder.StartTable(21);
    ItemItem.AddLevelUpId(builder, level_up_id);
    ItemItem.AddUseType(builder, use_type);
    ItemItem.AddPrice(builder, price);
    ItemItem.AddProgressTimeWhenUse(builder, progress_time_when_use);
    ItemItem.AddMinShowLevel(builder, min_show_level);
    ItemItem.AddQuantifier(builder, quantifierOffset);
    ItemItem.AddShowOrder(builder, show_order);
    ItemItem.AddDesc(builder, descOffset);
    ItemItem.AddIconMask(builder, icon_maskOffset);
    ItemItem.AddIconBackgroud(builder, icon_backgroudOffset);
    ItemItem.AddIcon(builder, iconOffset);
    ItemItem.AddQuality(builder, quality);
    ItemItem.AddMaxPileNum(builder, max_pile_num);
    ItemItem.AddMinorType(builder, minor_type);
    ItemItem.AddMajorType(builder, major_type);
    ItemItem.AddName(builder, nameOffset);
    ItemItem.AddId(builder, id);
    ItemItem.AddDroppable(builder, droppable);
    ItemItem.AddShowHintWhenUse(builder, show_hint_when_use);
    ItemItem.AddBatchUsable(builder, batch_usable);
    ItemItem.AddShowInBag(builder, show_in_bag);
    return ItemItem.EndItemItem(builder);
  }

  public static void StartItemItem(FlatBufferBuilder builder) { builder.StartTable(21); }
  public static void AddId(FlatBufferBuilder builder, int id) { builder.AddInt(0, id, 0); }
  public static void AddName(FlatBufferBuilder builder, StringOffset nameOffset) { builder.AddOffset(1, nameOffset.Value, 0); }
  public static void AddMajorType(FlatBufferBuilder builder, cfg.ItemEMajorType majorType) { builder.AddInt(2, (int)majorType, 0); }
  public static void AddMinorType(FlatBufferBuilder builder, cfg.ItemEMinorType minorType) { builder.AddInt(3, (int)minorType, 0); }
  public static void AddMaxPileNum(FlatBufferBuilder builder, int maxPileNum) { builder.AddInt(4, maxPileNum, 0); }
  public static void AddQuality(FlatBufferBuilder builder, cfg.ItemEItemQuality quality) { builder.AddInt(5, (int)quality, 0); }
  public static void AddIcon(FlatBufferBuilder builder, StringOffset iconOffset) { builder.AddOffset(6, iconOffset.Value, 0); }
  public static void AddIconBackgroud(FlatBufferBuilder builder, StringOffset iconBackgroudOffset) { builder.AddOffset(7, iconBackgroudOffset.Value, 0); }
  public static void AddIconMask(FlatBufferBuilder builder, StringOffset iconMaskOffset) { builder.AddOffset(8, iconMaskOffset.Value, 0); }
  public static void AddDesc(FlatBufferBuilder builder, StringOffset descOffset) { builder.AddOffset(9, descOffset.Value, 0); }
  public static void AddShowOrder(FlatBufferBuilder builder, int showOrder) { builder.AddInt(10, showOrder, 0); }
  public static void AddQuantifier(FlatBufferBuilder builder, StringOffset quantifierOffset) { builder.AddOffset(11, quantifierOffset.Value, 0); }
  public static void AddShowInBag(FlatBufferBuilder builder, bool showInBag) { builder.AddBool(12, showInBag, false); }
  public static void AddMinShowLevel(FlatBufferBuilder builder, int minShowLevel) { builder.AddInt(13, minShowLevel, 0); }
  public static void AddBatchUsable(FlatBufferBuilder builder, bool batchUsable) { builder.AddBool(14, batchUsable, false); }
  public static void AddProgressTimeWhenUse(FlatBufferBuilder builder, float progressTimeWhenUse) { builder.AddFloat(15, progressTimeWhenUse, 0.0f); }
  public static void AddShowHintWhenUse(FlatBufferBuilder builder, bool showHintWhenUse) { builder.AddBool(16, showHintWhenUse, false); }
  public static void AddDroppable(FlatBufferBuilder builder, bool droppable) { builder.AddBool(17, droppable, false); }
  public static void AddPrice(FlatBufferBuilder builder, int price) { builder.AddInt(18, price, 0); }
  public static void AddUseType(FlatBufferBuilder builder, cfg.ItemEUseType useType) { builder.AddInt(19, (int)useType, 0); }
  public static void AddLevelUpId(FlatBufferBuilder builder, int levelUpId) { builder.AddInt(20, levelUpId, 0); }
  public static Offset<cfg.ItemItem> EndItemItem(FlatBufferBuilder builder) {
    int o = builder.EndTable();
    return new Offset<cfg.ItemItem>(o);
  }
};


}
