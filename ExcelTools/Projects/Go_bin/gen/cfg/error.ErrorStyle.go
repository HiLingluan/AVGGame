
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

type ErrorErrorStyle struct {
}

const TypeId_ErrorErrorStyle = 129528911

func (*ErrorErrorStyle) GetTypeId() int32 {
    return 129528911
}

func (_v *ErrorErrorStyle)Serialize(_buf *serialization.ByteBuf) {
    // not support
}

func (_v *ErrorErrorStyle)Deserialize(_buf *serialization.ByteBuf) (err error) {
    return
}

func DeserializeErrorErrorStyle(_buf *serialization.ByteBuf) (interface{}, error) {
    var id int32
    var err error
    if id, err = _buf.ReadInt() ; err != nil {
        return nil, err
    }
    switch id {
        case 1915239884: _v := &ErrorErrorStyleTip{}; if err = _v.Deserialize(_buf); err != nil { return nil, errors.New("error.ErrorStyleTip") } else { return _v, nil }
        case -1920482343: _v := &ErrorErrorStyleMsgbox{}; if err = _v.Deserialize(_buf); err != nil { return nil, errors.New("error.ErrorStyleMsgbox") } else { return _v, nil }
        case -2010134516: _v := &ErrorErrorStyleDlgOk{}; if err = _v.Deserialize(_buf); err != nil { return nil, errors.New("error.ErrorStyleDlgOk") } else { return _v, nil }
        case 971221414: _v := &ErrorErrorStyleDlgOkCancel{}; if err = _v.Deserialize(_buf); err != nil { return nil, errors.New("error.ErrorStyleDlgOkCancel") } else { return _v, nil }
        default: return nil, errors.New("unknown type id")
    }
}

