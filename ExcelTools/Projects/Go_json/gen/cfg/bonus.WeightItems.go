//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

package cfg

import "errors"

type BonusWeightItems struct {
    ItemList []*BonusWeightItemInfo
}

const TypeId_BonusWeightItems = -356202311

func (*BonusWeightItems) GetTypeId() int32 {
    return -356202311
}

func (_v *BonusWeightItems)Deserialize(_buf map[string]interface{}) (err error) {
     {
                var _arr_ []interface{}
                var _ok_ bool
                if _arr_, _ok_ = _buf["item_list"].([]interface{}); !_ok_ { err = errors.New("item_list error"); return }

                _v.ItemList = make([]*BonusWeightItemInfo, 0, len(_arr_))
                
                for _, _e_ := range _arr_ {
                    var _list_v_ *BonusWeightItemInfo
                    { var _ok_ bool; var _x_ map[string]interface{}; if _x_, _ok_ = _e_.(map[string]interface{}); !_ok_ { err = errors.New("_list_v_ error"); return }; if _list_v_, err = DeserializeBonusWeightItemInfo(_x_); err != nil { return } }
                    _v.ItemList = append(_v.ItemList, _list_v_)
                }
            }

    return
}

func DeserializeBonusWeightItems(_buf map[string]interface{}) (*BonusWeightItems, error) {
    v := &BonusWeightItems{}
    if err := v.Deserialize(_buf); err == nil {
        return v, nil
    } else {
        return nil, err
    }
}
