
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

type TestSepBean1 struct {
    A int32
    B int32
    C string
}

const TypeId_TestSepBean1 = -1534339393

func (*TestSepBean1) GetTypeId() int32 {
    return -1534339393
}

func (_v *TestSepBean1)Serialize(_buf *serialization.ByteBuf) {
    // not support
}

func (_v *TestSepBean1)Deserialize(_buf *serialization.ByteBuf) (err error) {
    { if _v.A, err = _buf.ReadInt(); err != nil { err = errors.New("_v.A error"); return } }
    { if _v.B, err = _buf.ReadInt(); err != nil { err = errors.New("_v.B error"); return } }
    { if _v.C, err = _buf.ReadString(); err != nil { err = errors.New("_v.C error"); return } }
    return
}

func DeserializeTestSepBean1(_buf *serialization.ByteBuf) (*TestSepBean1, error) {
    v := &TestSepBean1{}
    if err := v.Deserialize(_buf); err == nil {
        return v, nil
    } else {
        return nil, err
    }
}
