// <auto-generated>
//  automatically generated by the FlatBuffers compiler, do not modify
// </auto-generated>

namespace cfg
{

using global::System;
using global::System.Collections.Generic;
using global::FlatBuffers;

public struct CostCostCurrencies : IFlatbufferObject
{
  private Table __p;
  public ByteBuffer ByteBuffer { get { return __p.bb; } }
  public static void ValidateVersion() { FlatBufferConstants.FLATBUFFERS_2_0_0(); }
  public static CostCostCurrencies GetRootAsCostCostCurrencies(ByteBuffer _bb) { return GetRootAsCostCostCurrencies(_bb, new CostCostCurrencies()); }
  public static CostCostCurrencies GetRootAsCostCostCurrencies(ByteBuffer _bb, CostCostCurrencies obj) { return (obj.__assign(_bb.GetInt(_bb.Position) + _bb.Position, _bb)); }
  public void __init(int _i, ByteBuffer _bb) { __p = new Table(_i, _bb); }
  public CostCostCurrencies __assign(int _i, ByteBuffer _bb) { __init(_i, _bb); return this; }

  public cfg.CostCostCurrency? Currencies(int j) { int o = __p.__offset(4); return o != 0 ? (cfg.CostCostCurrency?)(new cfg.CostCostCurrency()).__assign(__p.__indirect(__p.__vector(o) + j * 4), __p.bb) : null; }
  public int CurrenciesLength { get { int o = __p.__offset(4); return o != 0 ? __p.__vector_len(o) : 0; } }

  public static Offset<cfg.CostCostCurrencies> CreateCostCostCurrencies(FlatBufferBuilder builder,
      VectorOffset currenciesOffset = default(VectorOffset)) {
    builder.StartTable(1);
    CostCostCurrencies.AddCurrencies(builder, currenciesOffset);
    return CostCostCurrencies.EndCostCostCurrencies(builder);
  }

  public static void StartCostCostCurrencies(FlatBufferBuilder builder) { builder.StartTable(1); }
  public static void AddCurrencies(FlatBufferBuilder builder, VectorOffset currenciesOffset) { builder.AddOffset(0, currenciesOffset.Value, 0); }
  public static VectorOffset CreateCurrenciesVector(FlatBufferBuilder builder, Offset<cfg.CostCostCurrency>[] data) { builder.StartVector(4, data.Length, 4); for (int i = data.Length - 1; i >= 0; i--) builder.AddOffset(data[i].Value); return builder.EndVector(); }
  public static VectorOffset CreateCurrenciesVectorBlock(FlatBufferBuilder builder, Offset<cfg.CostCostCurrency>[] data) { builder.StartVector(4, data.Length, 4); builder.Add(data); return builder.EndVector(); }
  public static void StartCurrenciesVector(FlatBufferBuilder builder, int numElems) { builder.StartVector(4, numElems, 4); }
  public static Offset<cfg.CostCostCurrencies> EndCostCostCurrencies(FlatBufferBuilder builder) {
    int o = builder.EndTable();
    builder.Required(o, 4);  // currencies
    return new Offset<cfg.CostCostCurrencies>(o);
  }
};


}
