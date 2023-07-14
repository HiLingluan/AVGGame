
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

type ItemTreasureBox struct {
    Id int32
    KeyItemId *int32
    OpenLevel *ConditionMinLevel
    UseOnObtain bool
    DropIds []int32
    ChooseList []*ItemChooseOneBonus
}

const TypeId_ItemTreasureBox = 1494222369

func (*ItemTreasureBox) GetTypeId() int32 {
    return 1494222369
}

func (_v *ItemTreasureBox)Serialize(_buf *serialization.ByteBuf) {
    // not support
}

func (_v *ItemTreasureBox)Deserialize(_buf *serialization.ByteBuf) (err error) {
    { if _v.Id, err = _buf.ReadInt(); err != nil { err = errors.New("_v.Id error"); return } }
    { var __exists__ bool; if __exists__, err = _buf.ReadBool(); err != nil { return }; if __exists__ { var __x__ int32;  { if __x__, err = _buf.ReadInt(); err != nil { err = errors.New("__x__ error"); return } }; _v.KeyItemId = &__x__ }}
    { if _v.OpenLevel, err = DeserializeConditionMinLevel(_buf); err != nil { err = errors.New("_v.OpenLevel error"); return } }
    { if _v.UseOnObtain, err = _buf.ReadBool(); err != nil { err = errors.New("_v.UseOnObtain error"); err = errors.New("_v.UseOnObtain error"); return } }
    {_v.DropIds = make([]int32, 0); var _n_ int; if _n_, err = _buf.ReadSize(); err != nil { err = errors.New("_v.DropIds error"); return}; for i := 0 ; i < _n_ ; i++ { var _e_ int32; { if _e_, err = _buf.ReadInt(); err != nil { err = errors.New("_e_ error"); return } }; _v.DropIds = append(_v.DropIds, _e_) } }
    {_v.ChooseList = make([]*ItemChooseOneBonus, 0); var _n_ int; if _n_, err = _buf.ReadSize(); err != nil { err = errors.New("_v.ChooseList error"); return}; for i := 0 ; i < _n_ ; i++ { var _e_ *ItemChooseOneBonus; { if _e_, err = DeserializeItemChooseOneBonus(_buf); err != nil { err = errors.New("_e_ error"); return } }; _v.ChooseList = append(_v.ChooseList, _e_) } }
    return
}

func DeserializeItemTreasureBox(_buf *serialization.ByteBuf) (*ItemTreasureBox, error) {
    v := &ItemTreasureBox{}
    if err := v.Deserialize(_buf); err == nil {
        return v, nil
    } else {
        return nil, err
    }
}
