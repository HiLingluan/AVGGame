
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

type TestDemoE1 struct {
    X1 int32
    X3 int32
    X4 int32
}

const TypeId_TestDemoE1 = -2138341717

func (*TestDemoE1) GetTypeId() int32 {
    return -2138341717
}

func (_v *TestDemoE1)Serialize(_buf *serialization.ByteBuf) {
    // not support
}

func (_v *TestDemoE1)Deserialize(_buf *serialization.ByteBuf) (err error) {
    { if _v.X1, err = _buf.ReadInt(); err != nil { err = errors.New("_v.X1 error"); return } }
    { if _v.X3, err = _buf.ReadInt(); err != nil { err = errors.New("_v.X3 error"); return } }
    { if _v.X4, err = _buf.ReadInt(); err != nil { err = errors.New("_v.X4 error"); return } }
    return
}

func DeserializeTestDemoE1(_buf *serialization.ByteBuf) (*TestDemoE1, error) {
    v := &TestDemoE1{}
    if err := v.Deserialize(_buf); err == nil {
        return v, nil
    } else {
        return nil, err
    }
}
