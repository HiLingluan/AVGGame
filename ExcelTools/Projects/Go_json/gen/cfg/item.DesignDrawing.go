//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

package cfg

import "errors"

type ItemDesignDrawing struct {
    Id int32
    LearnComponentId []int32
}

const TypeId_ItemDesignDrawing = -1679179579

func (*ItemDesignDrawing) GetTypeId() int32 {
    return -1679179579
}

func (_v *ItemDesignDrawing)Deserialize(_buf map[string]interface{}) (err error) {
    { var _ok_ bool; var _tempNum_ float64; if _tempNum_, _ok_ = _buf["id"].(float64); !_ok_ { err = errors.New("id error"); return }; _v.Id = int32(_tempNum_) }
     {
                var _arr_ []interface{}
                var _ok_ bool
                if _arr_, _ok_ = _buf["learn_component_id"].([]interface{}); !_ok_ { err = errors.New("learn_component_id error"); return }

                _v.LearnComponentId = make([]int32, 0, len(_arr_))
                
                for _, _e_ := range _arr_ {
                    var _list_v_ int32
                    { var _ok_ bool; var _x_ float64; if _x_, _ok_ = _e_.(float64); !_ok_ { err = errors.New("_list_v_ error"); return }; _list_v_ = int32(_x_) }
                    _v.LearnComponentId = append(_v.LearnComponentId, _list_v_)
                }
            }

    return
}

func DeserializeItemDesignDrawing(_buf map[string]interface{}) (*ItemDesignDrawing, error) {
    v := &ItemDesignDrawing{}
    if err := v.Deserialize(_buf); err == nil {
        return v, nil
    } else {
        return nil, err
    }
}
