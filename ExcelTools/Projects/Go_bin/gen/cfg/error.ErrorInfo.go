
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

type ErrorErrorInfo struct {
    Code string
    Desc string
    Style interface{}
}

const TypeId_ErrorErrorInfo = 1389347408

func (*ErrorErrorInfo) GetTypeId() int32 {
    return 1389347408
}

func (_v *ErrorErrorInfo)Serialize(_buf *serialization.ByteBuf) {
    // not support
}

func (_v *ErrorErrorInfo)Deserialize(_buf *serialization.ByteBuf) (err error) {
    { if _v.Code, err = _buf.ReadString(); err != nil { err = errors.New("_v.Code error"); return } }
    { if _v.Desc, err = _buf.ReadString(); err != nil { err = errors.New("_v.Desc error"); return } }
    { if _v.Style, err = DeserializeErrorErrorStyle(_buf); err != nil { err = errors.New("_v.Style error"); return } }
    return
}

func DeserializeErrorErrorInfo(_buf *serialization.ByteBuf) (*ErrorErrorInfo, error) {
    v := &ErrorErrorInfo{}
    if err := v.Deserialize(_buf); err == nil {
        return v, nil
    } else {
        return nil, err
    }
}
