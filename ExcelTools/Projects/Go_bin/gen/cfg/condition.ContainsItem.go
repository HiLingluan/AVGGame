
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

type ConditionContainsItem struct {
    ItemId int32
    Num int32
    Reverse bool
}

const TypeId_ConditionContainsItem = 1961145317

func (*ConditionContainsItem) GetTypeId() int32 {
    return 1961145317
}

func (_v *ConditionContainsItem)Serialize(_buf *serialization.ByteBuf) {
    // not support
}

func (_v *ConditionContainsItem)Deserialize(_buf *serialization.ByteBuf) (err error) {
    { if _v.ItemId, err = _buf.ReadInt(); err != nil { err = errors.New("_v.ItemId error"); return } }
    { if _v.Num, err = _buf.ReadInt(); err != nil { err = errors.New("_v.Num error"); return } }
    { if _v.Reverse, err = _buf.ReadBool(); err != nil { err = errors.New("_v.Reverse error"); err = errors.New("_v.Reverse error"); return } }
    return
}

func DeserializeConditionContainsItem(_buf *serialization.ByteBuf) (*ConditionContainsItem, error) {
    v := &ConditionContainsItem{}
    if err := v.Deserialize(_buf); err == nil {
        return v, nil
    } else {
        return nil, err
    }
}
