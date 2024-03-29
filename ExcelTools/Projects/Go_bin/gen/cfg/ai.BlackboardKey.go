
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

type AiBlackboardKey struct {
    Name string
    Desc string
    IsStatic bool
    Type int32
    TypeClassName string
}

const TypeId_AiBlackboardKey = -511559886

func (*AiBlackboardKey) GetTypeId() int32 {
    return -511559886
}

func (_v *AiBlackboardKey)Serialize(_buf *serialization.ByteBuf) {
    // not support
}

func (_v *AiBlackboardKey)Deserialize(_buf *serialization.ByteBuf) (err error) {
    { if _v.Name, err = _buf.ReadString(); err != nil { err = errors.New("_v.Name error"); return } }
    { if _v.Desc, err = _buf.ReadString(); err != nil { err = errors.New("_v.Desc error"); return } }
    { if _v.IsStatic, err = _buf.ReadBool(); err != nil { err = errors.New("_v.IsStatic error"); err = errors.New("_v.IsStatic error"); return } }
    { if _v.Type, err = _buf.ReadInt(); err != nil { err = errors.New("_v.Type error"); return } }
    { if _v.TypeClassName, err = _buf.ReadString(); err != nil { err = errors.New("_v.TypeClassName error"); return } }
    return
}

func DeserializeAiBlackboardKey(_buf *serialization.ByteBuf) (*AiBlackboardKey, error) {
    v := &AiBlackboardKey{}
    if err := v.Deserialize(_buf); err == nil {
        return v, nil
    } else {
        return nil, err
    }
}
