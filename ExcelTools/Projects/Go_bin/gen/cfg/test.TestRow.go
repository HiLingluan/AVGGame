
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

type TestTestRow struct {
    X int32
    Y bool
    Z string
    A *TestTest3
    B []int32
}

const TypeId_TestTestRow = -543222164

func (*TestTestRow) GetTypeId() int32 {
    return -543222164
}

func (_v *TestTestRow)Serialize(_buf *serialization.ByteBuf) {
    // not support
}

func (_v *TestTestRow)Deserialize(_buf *serialization.ByteBuf) (err error) {
    { if _v.X, err = _buf.ReadInt(); err != nil { err = errors.New("_v.X error"); return } }
    { if _v.Y, err = _buf.ReadBool(); err != nil { err = errors.New("_v.Y error"); err = errors.New("_v.Y error"); return } }
    { if _v.Z, err = _buf.ReadString(); err != nil { err = errors.New("_v.Z error"); return } }
    { if _v.A, err = DeserializeTestTest3(_buf); err != nil { err = errors.New("_v.A error"); return } }
    {_v.B = make([]int32, 0); var _n_ int; if _n_, err = _buf.ReadSize(); err != nil { err = errors.New("_v.B error"); return}; for i := 0 ; i < _n_ ; i++ { var _e_ int32; { if _e_, err = _buf.ReadInt(); err != nil { err = errors.New("_e_ error"); return } }; _v.B = append(_v.B, _e_) } }
    return
}

func DeserializeTestTestRow(_buf *serialization.ByteBuf) (*TestTestRow, error) {
    v := &TestTestRow{}
    if err := v.Deserialize(_buf); err == nil {
        return v, nil
    } else {
        return nil, err
    }
}
