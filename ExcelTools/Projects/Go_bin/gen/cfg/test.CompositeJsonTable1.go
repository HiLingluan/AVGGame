
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

type TestCompositeJsonTable1 struct {
    Id int32
    X string
}

const TypeId_TestCompositeJsonTable1 = 1566207894

func (*TestCompositeJsonTable1) GetTypeId() int32 {
    return 1566207894
}

func (_v *TestCompositeJsonTable1)Serialize(_buf *serialization.ByteBuf) {
    // not support
}

func (_v *TestCompositeJsonTable1)Deserialize(_buf *serialization.ByteBuf) (err error) {
    { if _v.Id, err = _buf.ReadInt(); err != nil { err = errors.New("_v.Id error"); return } }
    { if _v.X, err = _buf.ReadString(); err != nil { err = errors.New("_v.X error"); return } }
    return
}

func DeserializeTestCompositeJsonTable1(_buf *serialization.ByteBuf) (*TestCompositeJsonTable1, error) {
    v := &TestCompositeJsonTable1{}
    if err := v.Deserialize(_buf); err == nil {
        return v, nil
    } else {
        return nil, err
    }
}
