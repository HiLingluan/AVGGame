
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

type BlueprintParamInfo struct {
    Name string
    Type string
    IsRef bool
}

const TypeId_BlueprintParamInfo = -729799392

func (*BlueprintParamInfo) GetTypeId() int32 {
    return -729799392
}

func (_v *BlueprintParamInfo)Serialize(_buf *serialization.ByteBuf) {
    // not support
}

func (_v *BlueprintParamInfo)Deserialize(_buf *serialization.ByteBuf) (err error) {
    { if _v.Name, err = _buf.ReadString(); err != nil { err = errors.New("_v.Name error"); return } }
    { if _v.Type, err = _buf.ReadString(); err != nil { err = errors.New("_v.Type error"); return } }
    { if _v.IsRef, err = _buf.ReadBool(); err != nil { err = errors.New("_v.IsRef error"); err = errors.New("_v.IsRef error"); return } }
    return
}

func DeserializeBlueprintParamInfo(_buf *serialization.ByteBuf) (*BlueprintParamInfo, error) {
    v := &BlueprintParamInfo{}
    if err := v.Deserialize(_buf); err == nil {
        return v, nil
    } else {
        return nil, err
    }
}
