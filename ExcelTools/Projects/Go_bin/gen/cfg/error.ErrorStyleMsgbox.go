
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

type ErrorErrorStyleMsgbox struct {
    BtnName string
    Operation int32
}

const TypeId_ErrorErrorStyleMsgbox = -1920482343

func (*ErrorErrorStyleMsgbox) GetTypeId() int32 {
    return -1920482343
}

func (_v *ErrorErrorStyleMsgbox)Serialize(_buf *serialization.ByteBuf) {
    // not support
}

func (_v *ErrorErrorStyleMsgbox)Deserialize(_buf *serialization.ByteBuf) (err error) {
    { if _v.BtnName, err = _buf.ReadString(); err != nil { err = errors.New("_v.BtnName error"); return } }
    { if _v.Operation, err = _buf.ReadInt(); err != nil { err = errors.New("_v.Operation error"); return } }
    return
}

func DeserializeErrorErrorStyleMsgbox(_buf *serialization.ByteBuf) (*ErrorErrorStyleMsgbox, error) {
    v := &ErrorErrorStyleMsgbox{}
    if err := v.Deserialize(_buf); err == nil {
        return v, nil
    } else {
        return nil, err
    }
}
