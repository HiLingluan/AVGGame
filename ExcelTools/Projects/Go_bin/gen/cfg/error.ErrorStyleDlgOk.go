
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

type ErrorErrorStyleDlgOk struct {
    BtnName string
}

const TypeId_ErrorErrorStyleDlgOk = -2010134516

func (*ErrorErrorStyleDlgOk) GetTypeId() int32 {
    return -2010134516
}

func (_v *ErrorErrorStyleDlgOk)Serialize(_buf *serialization.ByteBuf) {
    // not support
}

func (_v *ErrorErrorStyleDlgOk)Deserialize(_buf *serialization.ByteBuf) (err error) {
    { if _v.BtnName, err = _buf.ReadString(); err != nil { err = errors.New("_v.BtnName error"); return } }
    return
}

func DeserializeErrorErrorStyleDlgOk(_buf *serialization.ByteBuf) (*ErrorErrorStyleDlgOk, error) {
    v := &ErrorErrorStyleDlgOk{}
    if err := v.Deserialize(_buf); err == nil {
        return v, nil
    } else {
        return nil, err
    }
}
