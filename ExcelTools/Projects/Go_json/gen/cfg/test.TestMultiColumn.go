//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

package cfg

import "errors"

type TestTestMultiColumn struct {
    Id int32
    A *TestFoo
    B *TestFoo
    C *TestFoo
}

const TypeId_TestTestMultiColumn = -294473599

func (*TestTestMultiColumn) GetTypeId() int32 {
    return -294473599
}

func (_v *TestTestMultiColumn)Deserialize(_buf map[string]interface{}) (err error) {
    { var _ok_ bool; var _tempNum_ float64; if _tempNum_, _ok_ = _buf["id"].(float64); !_ok_ { err = errors.New("id error"); return }; _v.Id = int32(_tempNum_) }
    { var _ok_ bool; var _x_ map[string]interface{}; if _x_, _ok_ = _buf["a"].(map[string]interface{}); !_ok_ { err = errors.New("a error"); return }; if _v.A, err = DeserializeTestFoo(_x_); err != nil { return } }
    { var _ok_ bool; var _x_ map[string]interface{}; if _x_, _ok_ = _buf["b"].(map[string]interface{}); !_ok_ { err = errors.New("b error"); return }; if _v.B, err = DeserializeTestFoo(_x_); err != nil { return } }
    { var _ok_ bool; var _x_ map[string]interface{}; if _x_, _ok_ = _buf["c"].(map[string]interface{}); !_ok_ { err = errors.New("c error"); return }; if _v.C, err = DeserializeTestFoo(_x_); err != nil { return } }
    return
}

func DeserializeTestTestMultiColumn(_buf map[string]interface{}) (*TestTestMultiColumn, error) {
    v := &TestTestMultiColumn{}
    if err := v.Deserialize(_buf); err == nil {
        return v, nil
    } else {
        return nil, err
    }
}
