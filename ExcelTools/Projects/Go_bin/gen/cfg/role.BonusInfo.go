
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

type RoleBonusInfo struct {
    Type int32
    Coefficient float32
}

const TypeId_RoleBonusInfo = -1354421803

func (*RoleBonusInfo) GetTypeId() int32 {
    return -1354421803
}

func (_v *RoleBonusInfo)Serialize(_buf *serialization.ByteBuf) {
    // not support
}

func (_v *RoleBonusInfo)Deserialize(_buf *serialization.ByteBuf) (err error) {
    { if _v.Type, err = _buf.ReadInt(); err != nil { err = errors.New("_v.Type error"); return } }
    { if _v.Coefficient, err = _buf.ReadFloat(); err != nil { err = errors.New("_v.Coefficient error"); return } }
    return
}

func DeserializeRoleBonusInfo(_buf *serialization.ByteBuf) (*RoleBonusInfo, error) {
    v := &RoleBonusInfo{}
    if err := v.Deserialize(_buf); err == nil {
        return v, nil
    } else {
        return nil, err
    }
}