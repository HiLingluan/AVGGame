
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

type CommonGlobalConfig struct {
    BagCapacity int32
    BagCapacitySpecial int32
    BagTempExpendableCapacity int32
    BagTempToolCapacity int32
    BagInitCapacity int32
    QuickBagCapacity int32
    ClothBagCapacity int32
    ClothBagInitCapacity int32
    ClothBagCapacitySpecial int32
    BagInitItemsDropId *int32
    MailBoxCapacity int32
    DamageParamC float32
    DamageParamE float32
    DamageParamF float32
    DamageParamD float32
    RoleSpeed float32
    MonsterSpeed float32
    InitEnergy int32
    InitViality int32
    MaxViality int32
    PerVialityRecoveryTime int32
}

const TypeId_CommonGlobalConfig = -848234488

func (*CommonGlobalConfig) GetTypeId() int32 {
    return -848234488
}

func (_v *CommonGlobalConfig)Serialize(_buf *serialization.ByteBuf) {
    // not support
}

func (_v *CommonGlobalConfig)Deserialize(_buf *serialization.ByteBuf) (err error) {
    { if _v.BagCapacity, err = _buf.ReadInt(); err != nil { err = errors.New("_v.BagCapacity error"); return } }
    { if _v.BagCapacitySpecial, err = _buf.ReadInt(); err != nil { err = errors.New("_v.BagCapacitySpecial error"); return } }
    { if _v.BagTempExpendableCapacity, err = _buf.ReadInt(); err != nil { err = errors.New("_v.BagTempExpendableCapacity error"); return } }
    { if _v.BagTempToolCapacity, err = _buf.ReadInt(); err != nil { err = errors.New("_v.BagTempToolCapacity error"); return } }
    { if _v.BagInitCapacity, err = _buf.ReadInt(); err != nil { err = errors.New("_v.BagInitCapacity error"); return } }
    { if _v.QuickBagCapacity, err = _buf.ReadInt(); err != nil { err = errors.New("_v.QuickBagCapacity error"); return } }
    { if _v.ClothBagCapacity, err = _buf.ReadInt(); err != nil { err = errors.New("_v.ClothBagCapacity error"); return } }
    { if _v.ClothBagInitCapacity, err = _buf.ReadInt(); err != nil { err = errors.New("_v.ClothBagInitCapacity error"); return } }
    { if _v.ClothBagCapacitySpecial, err = _buf.ReadInt(); err != nil { err = errors.New("_v.ClothBagCapacitySpecial error"); return } }
    { var __exists__ bool; if __exists__, err = _buf.ReadBool(); err != nil { return }; if __exists__ { var __x__ int32;  { if __x__, err = _buf.ReadInt(); err != nil { err = errors.New("__x__ error"); return } }; _v.BagInitItemsDropId = &__x__ }}
    { if _v.MailBoxCapacity, err = _buf.ReadInt(); err != nil { err = errors.New("_v.MailBoxCapacity error"); return } }
    { if _v.DamageParamC, err = _buf.ReadFloat(); err != nil { err = errors.New("_v.DamageParamC error"); return } }
    { if _v.DamageParamE, err = _buf.ReadFloat(); err != nil { err = errors.New("_v.DamageParamE error"); return } }
    { if _v.DamageParamF, err = _buf.ReadFloat(); err != nil { err = errors.New("_v.DamageParamF error"); return } }
    { if _v.DamageParamD, err = _buf.ReadFloat(); err != nil { err = errors.New("_v.DamageParamD error"); return } }
    { if _v.RoleSpeed, err = _buf.ReadFloat(); err != nil { err = errors.New("_v.RoleSpeed error"); return } }
    { if _v.MonsterSpeed, err = _buf.ReadFloat(); err != nil { err = errors.New("_v.MonsterSpeed error"); return } }
    { if _v.InitEnergy, err = _buf.ReadInt(); err != nil { err = errors.New("_v.InitEnergy error"); return } }
    { if _v.InitViality, err = _buf.ReadInt(); err != nil { err = errors.New("_v.InitViality error"); return } }
    { if _v.MaxViality, err = _buf.ReadInt(); err != nil { err = errors.New("_v.MaxViality error"); return } }
    { if _v.PerVialityRecoveryTime, err = _buf.ReadInt(); err != nil { err = errors.New("_v.PerVialityRecoveryTime error"); return } }
    return
}

func DeserializeCommonGlobalConfig(_buf *serialization.ByteBuf) (*CommonGlobalConfig, error) {
    v := &CommonGlobalConfig{}
    if err := v.Deserialize(_buf); err == nil {
        return v, nil
    } else {
        return nil, err
    }
}
