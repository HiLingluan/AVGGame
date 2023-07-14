// <auto-generated>
//  automatically generated by the FlatBuffers compiler, do not modify
// </auto-generated>

namespace cfg
{

using global::System;
using global::System.Collections.Generic;
using global::FlatBuffers;

public struct ConditionTimeRange : IFlatbufferObject
{
  private Table __p;
  public ByteBuffer ByteBuffer { get { return __p.bb; } }
  public static void ValidateVersion() { FlatBufferConstants.FLATBUFFERS_2_0_0(); }
  public static ConditionTimeRange GetRootAsConditionTimeRange(ByteBuffer _bb) { return GetRootAsConditionTimeRange(_bb, new ConditionTimeRange()); }
  public static ConditionTimeRange GetRootAsConditionTimeRange(ByteBuffer _bb, ConditionTimeRange obj) { return (obj.__assign(_bb.GetInt(_bb.Position) + _bb.Position, _bb)); }
  public void __init(int _i, ByteBuffer _bb) { __p = new Table(_i, _bb); }
  public ConditionTimeRange __assign(int _i, ByteBuffer _bb) { __init(_i, _bb); return this; }

  public cfg.CommonDateTimeRange? DateTimeRange { get { int o = __p.__offset(4); return o != 0 ? (cfg.CommonDateTimeRange?)(new cfg.CommonDateTimeRange()).__assign(__p.__indirect(o + __p.bb_pos), __p.bb) : null; } }

  public static Offset<cfg.ConditionTimeRange> CreateConditionTimeRange(FlatBufferBuilder builder,
      Offset<cfg.CommonDateTimeRange> date_time_rangeOffset = default(Offset<cfg.CommonDateTimeRange>)) {
    builder.StartTable(1);
    ConditionTimeRange.AddDateTimeRange(builder, date_time_rangeOffset);
    return ConditionTimeRange.EndConditionTimeRange(builder);
  }

  public static void StartConditionTimeRange(FlatBufferBuilder builder) { builder.StartTable(1); }
  public static void AddDateTimeRange(FlatBufferBuilder builder, Offset<cfg.CommonDateTimeRange> dateTimeRangeOffset) { builder.AddOffset(0, dateTimeRangeOffset.Value, 0); }
  public static Offset<cfg.ConditionTimeRange> EndConditionTimeRange(FlatBufferBuilder builder) {
    int o = builder.EndTable();
    builder.Required(o, 4);  // date_time_range
    return new Offset<cfg.ConditionTimeRange>(o);
  }
};


}
