//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

package cfg

import "errors"

type ConditionRoleCondition struct {
}

const TypeId_ConditionRoleCondition = -1632338542

func (*ConditionRoleCondition) GetTypeId() int32 {
    return -1632338542
}

func (_v *ConditionRoleCondition)Deserialize(_buf map[string]interface{}) (err error) {
    return
}

func DeserializeConditionRoleCondition(_buf map[string]interface{}) (interface{}, error) {
    var id string
    var _ok_ bool
    if id, _ok_ = _buf["$type"].(string) ; !_ok_ {
        return nil, errors.New("type id missing")
    }
    switch id {
        case "MultiRoleCondition": _v := &ConditionMultiRoleCondition{}; if err := _v.Deserialize(_buf); err != nil { return nil, errors.New("condition.MultiRoleCondition") } else { return _v, nil }
        case "GenderLimit": _v := &ConditionGenderLimit{}; if err := _v.Deserialize(_buf); err != nil { return nil, errors.New("condition.GenderLimit") } else { return _v, nil }
        case "MinLevel": _v := &ConditionMinLevel{}; if err := _v.Deserialize(_buf); err != nil { return nil, errors.New("condition.MinLevel") } else { return _v, nil }
        case "MaxLevel": _v := &ConditionMaxLevel{}; if err := _v.Deserialize(_buf); err != nil { return nil, errors.New("condition.MaxLevel") } else { return _v, nil }
        case "MinMaxLevel": _v := &ConditionMinMaxLevel{}; if err := _v.Deserialize(_buf); err != nil { return nil, errors.New("condition.MinMaxLevel") } else { return _v, nil }
        case "ClothesPropertyScoreGreaterThan": _v := &ConditionClothesPropertyScoreGreaterThan{}; if err := _v.Deserialize(_buf); err != nil { return nil, errors.New("condition.ClothesPropertyScoreGreaterThan") } else { return _v, nil }
        case "ContainsItem": _v := &ConditionContainsItem{}; if err := _v.Deserialize(_buf); err != nil { return nil, errors.New("condition.ContainsItem") } else { return _v, nil }
        default: return nil, errors.New("unknown type id")
    }
}