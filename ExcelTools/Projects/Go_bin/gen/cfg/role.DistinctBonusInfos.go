
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

type RoleDistinctBonusInfos struct {
    EffectiveLevel int32
    BonusInfo []*RoleBonusInfo
}

const TypeId_RoleDistinctBonusInfos = -854361766

func (*RoleDistinctBonusInfos) GetTypeId() int32 {
    return -854361766
}

func (_v *RoleDistinctBonusInfos)Serialize(_buf *serialization.ByteBuf) {
    // not support
}

func (_v *RoleDistinctBonusInfos)Deserialize(_buf *serialization.ByteBuf) (err error) {
    { if _v.EffectiveLevel, err = _buf.ReadInt(); err != nil { err = errors.New("_v.EffectiveLevel error"); return } }
    {_v.BonusInfo = make([]*RoleBonusInfo, 0); var _n_ int; if _n_, err = _buf.ReadSize(); err != nil { err = errors.New("_v.BonusInfo error"); return}; for i := 0 ; i < _n_ ; i++ { var _e_ *RoleBonusInfo; { if _e_, err = DeserializeRoleBonusInfo(_buf); err != nil { err = errors.New("_e_ error"); return } }; _v.BonusInfo = append(_v.BonusInfo, _e_) } }
    return
}

func DeserializeRoleDistinctBonusInfos(_buf *serialization.ByteBuf) (*RoleDistinctBonusInfos, error) {
    v := &RoleDistinctBonusInfos{}
    if err := v.Deserialize(_buf); err == nil {
        return v, nil
    } else {
        return nil, err
    }
}
