
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

package cfg

import (
    "luban_examples/go_bin/bright/serialization"
)

import "errors"

type CostCostCurrencies struct {
    Currencies []*CostCostCurrency
}

const TypeId_CostCostCurrencies = 103084157

func (*CostCostCurrencies) GetTypeId() int32 {
    return 103084157
}

func (_v *CostCostCurrencies)Serialize(_buf *serialization.ByteBuf) {
    // not support
}

func (_v *CostCostCurrencies)Deserialize(_buf *serialization.ByteBuf) (err error) {
    {_v.Currencies = make([]*CostCostCurrency, 0); var _n_ int; if _n_, err = _buf.ReadSize(); err != nil { err = errors.New("_v.Currencies error"); return}; for i := 0 ; i < _n_ ; i++ { var _e_ *CostCostCurrency; { if _e_, err = DeserializeCostCostCurrency(_buf); err != nil { err = errors.New("_e_ error"); return } }; _v.Currencies = append(_v.Currencies, _e_) } }
    return
}

func DeserializeCostCostCurrencies(_buf *serialization.ByteBuf) (*CostCostCurrencies, error) {
    v := &CostCostCurrencies{}
    if err := v.Deserialize(_buf); err == nil {
        return v, nil
    } else {
        return nil, err
    }
}
