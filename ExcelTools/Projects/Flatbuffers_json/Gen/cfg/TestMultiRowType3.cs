// <auto-generated>
//  automatically generated by the FlatBuffers compiler, do not modify
// </auto-generated>

namespace cfg
{

using global::System;
using global::System.Collections.Generic;
using global::FlatBuffers;

public struct TestMultiRowType3 : IFlatbufferObject
{
  private Table __p;
  public ByteBuffer ByteBuffer { get { return __p.bb; } }
  public static void ValidateVersion() { FlatBufferConstants.FLATBUFFERS_2_0_0(); }
  public static TestMultiRowType3 GetRootAsTestMultiRowType3(ByteBuffer _bb) { return GetRootAsTestMultiRowType3(_bb, new TestMultiRowType3()); }
  public static TestMultiRowType3 GetRootAsTestMultiRowType3(ByteBuffer _bb, TestMultiRowType3 obj) { return (obj.__assign(_bb.GetInt(_bb.Position) + _bb.Position, _bb)); }
  public void __init(int _i, ByteBuffer _bb) { __p = new Table(_i, _bb); }
  public TestMultiRowType3 __assign(int _i, ByteBuffer _bb) { __init(_i, _bb); return this; }

  public int Id { get { int o = __p.__offset(4); return o != 0 ? __p.bb.GetInt(o + __p.bb_pos) : (int)0; } }
  public cfg.TestMultiRowType1? Items(int j) { int o = __p.__offset(6); return o != 0 ? (cfg.TestMultiRowType1?)(new cfg.TestMultiRowType1()).__assign(__p.__indirect(__p.__vector(o) + j * 4), __p.bb) : null; }
  public int ItemsLength { get { int o = __p.__offset(6); return o != 0 ? __p.__vector_len(o) : 0; } }

  public static Offset<cfg.TestMultiRowType3> CreateTestMultiRowType3(FlatBufferBuilder builder,
      int id = 0,
      VectorOffset itemsOffset = default(VectorOffset)) {
    builder.StartTable(2);
    TestMultiRowType3.AddItems(builder, itemsOffset);
    TestMultiRowType3.AddId(builder, id);
    return TestMultiRowType3.EndTestMultiRowType3(builder);
  }

  public static void StartTestMultiRowType3(FlatBufferBuilder builder) { builder.StartTable(2); }
  public static void AddId(FlatBufferBuilder builder, int id) { builder.AddInt(0, id, 0); }
  public static void AddItems(FlatBufferBuilder builder, VectorOffset itemsOffset) { builder.AddOffset(1, itemsOffset.Value, 0); }
  public static VectorOffset CreateItemsVector(FlatBufferBuilder builder, Offset<cfg.TestMultiRowType1>[] data) { builder.StartVector(4, data.Length, 4); for (int i = data.Length - 1; i >= 0; i--) builder.AddOffset(data[i].Value); return builder.EndVector(); }
  public static VectorOffset CreateItemsVectorBlock(FlatBufferBuilder builder, Offset<cfg.TestMultiRowType1>[] data) { builder.StartVector(4, data.Length, 4); builder.Add(data); return builder.EndVector(); }
  public static void StartItemsVector(FlatBufferBuilder builder, int numElems) { builder.StartVector(4, numElems, 4); }
  public static Offset<cfg.TestMultiRowType3> EndTestMultiRowType3(FlatBufferBuilder builder) {
    int o = builder.EndTable();
    builder.Required(o, 6);  // items
    return new Offset<cfg.TestMultiRowType3>(o);
  }
};


}
