//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

package cfg

import "errors"

type TestMultiRowTitle struct {
    Id int32
    Name string
    X1 *TestH1
    X20 *TestH2
    X2 []*TestH2
    X3 []*TestH2
    X4 []*TestH2
}

const TypeId_TestMultiRowTitle = 540002427

func (*TestMultiRowTitle) GetTypeId() int32 {
    return 540002427
}

func (_v *TestMultiRowTitle)Deserialize(_buf map[string]interface{}) (err error) {
    { var _ok_ bool; var _tempNum_ float64; if _tempNum_, _ok_ = _buf["id"].(float64); !_ok_ { err = errors.New("id error"); return }; _v.Id = int32(_tempNum_) }
    { var _ok_ bool; if _v.Name, _ok_ = _buf["name"].(string); !_ok_ { err = errors.New("name error"); return } }
    { var _ok_ bool; var _x_ map[string]interface{}; if _x_, _ok_ = _buf["x1"].(map[string]interface{}); !_ok_ { err = errors.New("x1 error"); return }; if _v.X1, err = DeserializeTestH1(_x_); err != nil { return } }
    { var _ok_ bool; var __json_x2_0__ interface{}; if __json_x2_0__, _ok_ = _buf["x2_0"]; !_ok_ || __json_x2_0__ == nil { _v.X20 = nil } else { var __x__ *TestH2;  { var _ok_ bool; var _x_ map[string]interface{}; if _x_, _ok_ = __json_x2_0__.(map[string]interface{}); !_ok_ { err = errors.New("__x__ error"); return }; if __x__, err = DeserializeTestH2(_x_); err != nil { return } }; _v.X20 = __x__ }}
     {
                var _arr_ []interface{}
                var _ok_ bool
                if _arr_, _ok_ = _buf["x2"].([]interface{}); !_ok_ { err = errors.New("x2 error"); return }

                _v.X2 = make([]*TestH2, 0, len(_arr_))
                
                for _, _e_ := range _arr_ {
                    var _list_v_ *TestH2
                    { var _ok_ bool; var _x_ map[string]interface{}; if _x_, _ok_ = _e_.(map[string]interface{}); !_ok_ { err = errors.New("_list_v_ error"); return }; if _list_v_, err = DeserializeTestH2(_x_); err != nil { return } }
                    _v.X2 = append(_v.X2, _list_v_)
                }
            }

     {
                var _arr_ []interface{}
                var _ok_ bool
                if _arr_, _ok_ = _buf["x3"].([]interface{}); !_ok_ { err = errors.New("x3 error"); return }

                _v.X3 = make([]*TestH2, 0, len(_arr_))
                
                for _, _e_ := range _arr_ {
                    var _list_v_ *TestH2
                    { var _ok_ bool; var _x_ map[string]interface{}; if _x_, _ok_ = _e_.(map[string]interface{}); !_ok_ { err = errors.New("_list_v_ error"); return }; if _list_v_, err = DeserializeTestH2(_x_); err != nil { return } }
                    _v.X3 = append(_v.X3, _list_v_)
                }
            }

     {
                var _arr_ []interface{}
                var _ok_ bool
                if _arr_, _ok_ = _buf["x4"].([]interface{}); !_ok_ { err = errors.New("x4 error"); return }

                _v.X4 = make([]*TestH2, 0, len(_arr_))
                
                for _, _e_ := range _arr_ {
                    var _list_v_ *TestH2
                    { var _ok_ bool; var _x_ map[string]interface{}; if _x_, _ok_ = _e_.(map[string]interface{}); !_ok_ { err = errors.New("_list_v_ error"); return }; if _list_v_, err = DeserializeTestH2(_x_); err != nil { return } }
                    _v.X4 = append(_v.X4, _list_v_)
                }
            }

    return
}

func DeserializeTestMultiRowTitle(_buf map[string]interface{}) (*TestMultiRowTitle, error) {
    v := &TestMultiRowTitle{}
    if err := v.Deserialize(_buf); err == nil {
        return v, nil
    } else {
        return nil, err
    }
}
