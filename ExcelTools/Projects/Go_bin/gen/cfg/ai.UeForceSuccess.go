
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

type AiUeForceSuccess struct {
    Id int32
    NodeName string
    FlowAbortMode int32
}

const TypeId_AiUeForceSuccess = 195054574

func (*AiUeForceSuccess) GetTypeId() int32 {
    return 195054574
}

func (_v *AiUeForceSuccess)Serialize(_buf *serialization.ByteBuf) {
    // not support
}

func (_v *AiUeForceSuccess)Deserialize(_buf *serialization.ByteBuf) (err error) {
    { if _v.Id, err = _buf.ReadInt(); err != nil { err = errors.New("_v.Id error"); return } }
    { if _v.NodeName, err = _buf.ReadString(); err != nil { err = errors.New("_v.NodeName error"); return } }
    { if _v.FlowAbortMode, err = _buf.ReadInt(); err != nil { err = errors.New("_v.FlowAbortMode error"); return } }
    return
}

func DeserializeAiUeForceSuccess(_buf *serialization.ByteBuf) (*AiUeForceSuccess, error) {
    v := &AiUeForceSuccess{}
    if err := v.Deserialize(_buf); err == nil {
        return v, nil
    } else {
        return nil, err
    }
}
