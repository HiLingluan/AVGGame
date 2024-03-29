// <auto-generated>
//  automatically generated by the FlatBuffers compiler, do not modify
// </auto-generated>

namespace cfg
{

using global::System;
using global::System.Collections.Generic;
using global::FlatBuffers;

public struct TestDefineFromExcelOne : IFlatbufferObject
{
  private Table __p;
  public ByteBuffer ByteBuffer { get { return __p.bb; } }
  public static void ValidateVersion() { FlatBufferConstants.FLATBUFFERS_2_0_0(); }
  public static TestDefineFromExcelOne GetRootAsTestDefineFromExcelOne(ByteBuffer _bb) { return GetRootAsTestDefineFromExcelOne(_bb, new TestDefineFromExcelOne()); }
  public static TestDefineFromExcelOne GetRootAsTestDefineFromExcelOne(ByteBuffer _bb, TestDefineFromExcelOne obj) { return (obj.__assign(_bb.GetInt(_bb.Position) + _bb.Position, _bb)); }
  public void __init(int _i, ByteBuffer _bb) { __p = new Table(_i, _bb); }
  public TestDefineFromExcelOne __assign(int _i, ByteBuffer _bb) { __init(_i, _bb); return this; }

  public int UnlockEquip { get { int o = __p.__offset(4); return o != 0 ? __p.bb.GetInt(o + __p.bb_pos) : (int)0; } }
  public int UnlockHero { get { int o = __p.__offset(6); return o != 0 ? __p.bb.GetInt(o + __p.bb_pos) : (int)0; } }
  public string DefaultAvatar { get { int o = __p.__offset(8); return o != 0 ? __p.__string(o + __p.bb_pos) : null; } }
#if ENABLE_SPAN_T
  public Span<byte> GetDefaultAvatarBytes() { return __p.__vector_as_span<byte>(8, 1); }
#else
  public ArraySegment<byte>? GetDefaultAvatarBytes() { return __p.__vector_as_arraysegment(8); }
#endif
  public byte[] GetDefaultAvatarArray() { return __p.__vector_as_array<byte>(8); }
  public string DefaultItem { get { int o = __p.__offset(10); return o != 0 ? __p.__string(o + __p.bb_pos) : null; } }
#if ENABLE_SPAN_T
  public Span<byte> GetDefaultItemBytes() { return __p.__vector_as_span<byte>(10, 1); }
#else
  public ArraySegment<byte>? GetDefaultItemBytes() { return __p.__vector_as_arraysegment(10); }
#endif
  public byte[] GetDefaultItemArray() { return __p.__vector_as_array<byte>(10); }

  public static Offset<cfg.TestDefineFromExcelOne> CreateTestDefineFromExcelOne(FlatBufferBuilder builder,
      int unlock_equip = 0,
      int unlock_hero = 0,
      StringOffset default_avatarOffset = default(StringOffset),
      StringOffset default_itemOffset = default(StringOffset)) {
    builder.StartTable(4);
    TestDefineFromExcelOne.AddDefaultItem(builder, default_itemOffset);
    TestDefineFromExcelOne.AddDefaultAvatar(builder, default_avatarOffset);
    TestDefineFromExcelOne.AddUnlockHero(builder, unlock_hero);
    TestDefineFromExcelOne.AddUnlockEquip(builder, unlock_equip);
    return TestDefineFromExcelOne.EndTestDefineFromExcelOne(builder);
  }

  public static void StartTestDefineFromExcelOne(FlatBufferBuilder builder) { builder.StartTable(4); }
  public static void AddUnlockEquip(FlatBufferBuilder builder, int unlockEquip) { builder.AddInt(0, unlockEquip, 0); }
  public static void AddUnlockHero(FlatBufferBuilder builder, int unlockHero) { builder.AddInt(1, unlockHero, 0); }
  public static void AddDefaultAvatar(FlatBufferBuilder builder, StringOffset defaultAvatarOffset) { builder.AddOffset(2, defaultAvatarOffset.Value, 0); }
  public static void AddDefaultItem(FlatBufferBuilder builder, StringOffset defaultItemOffset) { builder.AddOffset(3, defaultItemOffset.Value, 0); }
  public static Offset<cfg.TestDefineFromExcelOne> EndTestDefineFromExcelOne(FlatBufferBuilder builder) {
    int o = builder.EndTable();
    return new Offset<cfg.TestDefineFromExcelOne>(o);
  }
};


}
