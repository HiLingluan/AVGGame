//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

package cfg

import "errors"

type Test2Rectangle struct {
    Width float32
    Height float32
}

const TypeId_Test2Rectangle = 694982337

func (*Test2Rectangle) GetTypeId() int32 {
    return 694982337
}

func (_v *Test2Rectangle)Deserialize(_buf map[string]interface{}) (err error) {
    { var _ok_ bool; var _tempNum_ float64; if _tempNum_, _ok_ = _buf["width"].(float64); !_ok_ { err = errors.New("width error"); return }; _v.Width = float32(_tempNum_) }
    { var _ok_ bool; var _tempNum_ float64; if _tempNum_, _ok_ = _buf["height"].(float64); !_ok_ { err = errors.New("height error"); return }; _v.Height = float32(_tempNum_) }
    return
}

func DeserializeTest2Rectangle(_buf map[string]interface{}) (*Test2Rectangle, error) {
    v := &Test2Rectangle{}
    if err := v.Deserialize(_buf); err == nil {
        return v, nil
    } else {
        return nil, err
    }
}
