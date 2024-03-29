
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

type AiDistanceLessThan struct {
    Id int32
    NodeName string
    FlowAbortMode int32
    Actor1Key string
    Actor2Key string
    Distance float32
    ReverseResult bool
}

const TypeId_AiDistanceLessThan = -1207170283

func (*AiDistanceLessThan) GetTypeId() int32 {
    return -1207170283
}

func (_v *AiDistanceLessThan)Serialize(_buf *serialization.ByteBuf) {
    // not support
}

func (_v *AiDistanceLessThan)Deserialize(_buf *serialization.ByteBuf) (err error) {
    { if _v.Id, err = _buf.ReadInt(); err != nil { err = errors.New("_v.Id error"); return } }
    { if _v.NodeName, err = _buf.ReadString(); err != nil { err = errors.New("_v.NodeName error"); return } }
    { if _v.FlowAbortMode, err = _buf.ReadInt(); err != nil { err = errors.New("_v.FlowAbortMode error"); return } }
    { if _v.Actor1Key, err = _buf.ReadString(); err != nil { err = errors.New("_v.Actor1Key error"); return } }
    { if _v.Actor2Key, err = _buf.ReadString(); err != nil { err = errors.New("_v.Actor2Key error"); return } }
    { if _v.Distance, err = _buf.ReadFloat(); err != nil { err = errors.New("_v.Distance error"); return } }
    { if _v.ReverseResult, err = _buf.ReadBool(); err != nil { err = errors.New("_v.ReverseResult error"); err = errors.New("_v.ReverseResult error"); return } }
    return
}

func DeserializeAiDistanceLessThan(_buf *serialization.ByteBuf) (*AiDistanceLessThan, error) {
    v := &AiDistanceLessThan{}
    if err := v.Deserialize(_buf); err == nil {
        return v, nil
    } else {
        return nil, err
    }
}
