
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

type AiIsAtLocation struct {
    Id int32
    NodeName string
    FlowAbortMode int32
    AcceptableRadius float32
    KeyboardKey string
    InverseCondition bool
}

const TypeId_AiIsAtLocation = 1255972344

func (*AiIsAtLocation) GetTypeId() int32 {
    return 1255972344
}

func (_v *AiIsAtLocation)Serialize(_buf *serialization.ByteBuf) {
    // not support
}

func (_v *AiIsAtLocation)Deserialize(_buf *serialization.ByteBuf) (err error) {
    { if _v.Id, err = _buf.ReadInt(); err != nil { err = errors.New("_v.Id error"); return } }
    { if _v.NodeName, err = _buf.ReadString(); err != nil { err = errors.New("_v.NodeName error"); return } }
    { if _v.FlowAbortMode, err = _buf.ReadInt(); err != nil { err = errors.New("_v.FlowAbortMode error"); return } }
    { if _v.AcceptableRadius, err = _buf.ReadFloat(); err != nil { err = errors.New("_v.AcceptableRadius error"); return } }
    { if _v.KeyboardKey, err = _buf.ReadString(); err != nil { err = errors.New("_v.KeyboardKey error"); return } }
    { if _v.InverseCondition, err = _buf.ReadBool(); err != nil { err = errors.New("_v.InverseCondition error"); err = errors.New("_v.InverseCondition error"); return } }
    return
}

func DeserializeAiIsAtLocation(_buf *serialization.ByteBuf) (*AiIsAtLocation, error) {
    v := &AiIsAtLocation{}
    if err := v.Deserialize(_buf); err == nil {
        return v, nil
    } else {
        return nil, err
    }
}
