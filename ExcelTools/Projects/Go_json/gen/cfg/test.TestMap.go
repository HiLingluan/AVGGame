//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

package cfg

import "errors"

type TestTestMap struct {
    Id int32
    X1 map[int32]int32
    X2 map[int64]int32
    X3 map[string]int32
    X4 map[int32]int32
}

const TypeId_TestTestMap = -543227410

func (*TestTestMap) GetTypeId() int32 {
    return -543227410
}

func (_v *TestTestMap)Deserialize(_buf map[string]interface{}) (err error) {
    { var _ok_ bool; var _tempNum_ float64; if _tempNum_, _ok_ = _buf["id"].(float64); !_ok_ { err = errors.New("id error"); return }; _v.Id = int32(_tempNum_) }
    {
                var _arr_ []interface{}
                var _ok_ bool
                if _arr_, _ok_ = _buf["x1"].([]interface{}); !_ok_ { err = errors.New("x1 error"); return }

                _v.X1 = make(map[int32]int32)
                
                for _, _e_ := range _arr_ {
                    var _kv_ []interface{}
                    if _kv_, _ok_ = _e_.([]interface{}); !_ok_ || len(_kv_) != 2 { err = errors.New("x1 error"); return }
                    var _key_ int32
                    { var _ok_ bool; var _x_ float64; if _x_, _ok_ = _kv_[0].(float64); !_ok_ { err = errors.New("_key_ error"); return }; _key_ = int32(_x_) }
                    var _value_ int32
                    { var _ok_ bool; var _x_ float64; if _x_, _ok_ = _kv_[1].(float64); !_ok_ { err = errors.New("_value_ error"); return }; _value_ = int32(_x_) }
                    _v.X1[_key_] = _value_
                }
                }
    {
                var _arr_ []interface{}
                var _ok_ bool
                if _arr_, _ok_ = _buf["x2"].([]interface{}); !_ok_ { err = errors.New("x2 error"); return }

                _v.X2 = make(map[int64]int32)
                
                for _, _e_ := range _arr_ {
                    var _kv_ []interface{}
                    if _kv_, _ok_ = _e_.([]interface{}); !_ok_ || len(_kv_) != 2 { err = errors.New("x2 error"); return }
                    var _key_ int64
                    { var _ok_ bool; var _x_ float64; if _x_, _ok_ = _kv_[0].(float64); !_ok_ { err = errors.New("_key_ error"); return }; _key_ = int64(_x_) }
                    var _value_ int32
                    { var _ok_ bool; var _x_ float64; if _x_, _ok_ = _kv_[1].(float64); !_ok_ { err = errors.New("_value_ error"); return }; _value_ = int32(_x_) }
                    _v.X2[_key_] = _value_
                }
                }
    {
                var _arr_ []interface{}
                var _ok_ bool
                if _arr_, _ok_ = _buf["x3"].([]interface{}); !_ok_ { err = errors.New("x3 error"); return }

                _v.X3 = make(map[string]int32)
                
                for _, _e_ := range _arr_ {
                    var _kv_ []interface{}
                    if _kv_, _ok_ = _e_.([]interface{}); !_ok_ || len(_kv_) != 2 { err = errors.New("x3 error"); return }
                    var _key_ string
                    {  if _key_, _ok_ = _kv_[0].(string); !_ok_ { err = errors.New("_key_ error"); return } }
                    var _value_ int32
                    { var _ok_ bool; var _x_ float64; if _x_, _ok_ = _kv_[1].(float64); !_ok_ { err = errors.New("_value_ error"); return }; _value_ = int32(_x_) }
                    _v.X3[_key_] = _value_
                }
                }
    {
                var _arr_ []interface{}
                var _ok_ bool
                if _arr_, _ok_ = _buf["x4"].([]interface{}); !_ok_ { err = errors.New("x4 error"); return }

                _v.X4 = make(map[int32]int32)
                
                for _, _e_ := range _arr_ {
                    var _kv_ []interface{}
                    if _kv_, _ok_ = _e_.([]interface{}); !_ok_ || len(_kv_) != 2 { err = errors.New("x4 error"); return }
                    var _key_ int32
                    { var _ok_ bool; var _x_ float64; if _x_, _ok_ = _kv_[0].(float64); !_ok_ { err = errors.New("_key_ error"); return }; _key_ = int32(_x_) }
                    var _value_ int32
                    { var _ok_ bool; var _x_ float64; if _x_, _ok_ = _kv_[1].(float64); !_ok_ { err = errors.New("_value_ error"); return }; _value_ = int32(_x_) }
                    _v.X4[_key_] = _value_
                }
                }
    return
}

func DeserializeTestTestMap(_buf map[string]interface{}) (*TestTestMap, error) {
    v := &TestTestMap{}
    if err := v.Deserialize(_buf); err == nil {
        return v, nil
    } else {
        return nil, err
    }
}