//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

package cfg

import "errors"

type TestMultiRowType2 struct {
    Id int32
    X int32
    Y float32
}

const TypeId_TestMultiRowType2 = 540474971

func (*TestMultiRowType2) GetTypeId() int32 {
    return 540474971
}

func (_v *TestMultiRowType2)Deserialize(_buf map[string]interface{}) (err error) {
    { var _ok_ bool; var _tempNum_ float64; if _tempNum_, _ok_ = _buf["id"].(float64); !_ok_ { err = errors.New("id error"); return }; _v.Id = int32(_tempNum_) }
    { var _ok_ bool; var _tempNum_ float64; if _tempNum_, _ok_ = _buf["x"].(float64); !_ok_ { err = errors.New("x error"); return }; _v.X = int32(_tempNum_) }
    { var _ok_ bool; var _tempNum_ float64; if _tempNum_, _ok_ = _buf["y"].(float64); !_ok_ { err = errors.New("y error"); return }; _v.Y = float32(_tempNum_) }
    return
}

func DeserializeTestMultiRowType2(_buf map[string]interface{}) (*TestMultiRowType2, error) {
    v := &TestMultiRowType2{}
    if err := v.Deserialize(_buf); err == nil {
        return v, nil
    } else {
        return nil, err
    }
}