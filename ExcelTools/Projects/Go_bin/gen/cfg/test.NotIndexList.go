
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

type TestNotIndexList struct {
    X int32
    Y int32
}

const TypeId_TestNotIndexList = -50446599

func (*TestNotIndexList) GetTypeId() int32 {
    return -50446599
}

func (_v *TestNotIndexList)Serialize(_buf *serialization.ByteBuf) {
    // not support
}

func (_v *TestNotIndexList)Deserialize(_buf *serialization.ByteBuf) (err error) {
    { if _v.X, err = _buf.ReadInt(); err != nil { err = errors.New("_v.X error"); return } }
    { if _v.Y, err = _buf.ReadInt(); err != nil { err = errors.New("_v.Y error"); return } }
    return
}

func DeserializeTestNotIndexList(_buf *serialization.ByteBuf) (*TestNotIndexList, error) {
    v := &TestNotIndexList{}
    if err := v.Deserialize(_buf); err == nil {
        return v, nil
    } else {
        return nil, err
    }
}
