
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

type ItemItem struct {
    Id int32
    Name string
    MajorType int32
    MinorType int32
    MaxPileNum int32
    Quality int32
    Icon string
    IconBackgroud string
    IconMask string
    Desc string
    ShowOrder int32
    Quantifier string
    ShowInBag bool
    MinShowLevel int32
    BatchUsable bool
    ProgressTimeWhenUse float32
    ShowHintWhenUse bool
    Droppable bool
    Price *int32
    UseType int32
    LevelUpId *int32
}

const TypeId_ItemItem = 2107285806

func (*ItemItem) GetTypeId() int32 {
    return 2107285806
}

func (_v *ItemItem)Serialize(_buf *serialization.ByteBuf) {
    // not support
}

func (_v *ItemItem)Deserialize(_buf *serialization.ByteBuf) (err error) {
    { if _v.Id, err = _buf.ReadInt(); err != nil { err = errors.New("_v.Id error"); return } }
    { if _v.Name, err = _buf.ReadString(); err != nil { err = errors.New("_v.Name error"); return } }
    { if _v.MajorType, err = _buf.ReadInt(); err != nil { err = errors.New("_v.MajorType error"); return } }
    { if _v.MinorType, err = _buf.ReadInt(); err != nil { err = errors.New("_v.MinorType error"); return } }
    { if _v.MaxPileNum, err = _buf.ReadInt(); err != nil { err = errors.New("_v.MaxPileNum error"); return } }
    { if _v.Quality, err = _buf.ReadInt(); err != nil { err = errors.New("_v.Quality error"); return } }
    { if _v.Icon, err = _buf.ReadString(); err != nil { err = errors.New("_v.Icon error"); return } }
    { if _v.IconBackgroud, err = _buf.ReadString(); err != nil { err = errors.New("_v.IconBackgroud error"); return } }
    { if _v.IconMask, err = _buf.ReadString(); err != nil { err = errors.New("_v.IconMask error"); return } }
    { if _v.Desc, err = _buf.ReadString(); err != nil { err = errors.New("_v.Desc error"); return } }
    { if _v.ShowOrder, err = _buf.ReadInt(); err != nil { err = errors.New("_v.ShowOrder error"); return } }
    { if _v.Quantifier, err = _buf.ReadString(); err != nil { err = errors.New("_v.Quantifier error"); return } }
    { if _v.ShowInBag, err = _buf.ReadBool(); err != nil { err = errors.New("_v.ShowInBag error"); err = errors.New("_v.ShowInBag error"); return } }
    { if _v.MinShowLevel, err = _buf.ReadInt(); err != nil { err = errors.New("_v.MinShowLevel error"); return } }
    { if _v.BatchUsable, err = _buf.ReadBool(); err != nil { err = errors.New("_v.BatchUsable error"); err = errors.New("_v.BatchUsable error"); return } }
    { if _v.ProgressTimeWhenUse, err = _buf.ReadFloat(); err != nil { err = errors.New("_v.ProgressTimeWhenUse error"); return } }
    { if _v.ShowHintWhenUse, err = _buf.ReadBool(); err != nil { err = errors.New("_v.ShowHintWhenUse error"); err = errors.New("_v.ShowHintWhenUse error"); return } }
    { if _v.Droppable, err = _buf.ReadBool(); err != nil { err = errors.New("_v.Droppable error"); err = errors.New("_v.Droppable error"); return } }
    { var __exists__ bool; if __exists__, err = _buf.ReadBool(); err != nil { return }; if __exists__ { var __x__ int32;  { if __x__, err = _buf.ReadInt(); err != nil { err = errors.New("__x__ error"); return } }; _v.Price = &__x__ }}
    { if _v.UseType, err = _buf.ReadInt(); err != nil { err = errors.New("_v.UseType error"); return } }
    { var __exists__ bool; if __exists__, err = _buf.ReadBool(); err != nil { return }; if __exists__ { var __x__ int32;  { if __x__, err = _buf.ReadInt(); err != nil { err = errors.New("__x__ error"); return } }; _v.LevelUpId = &__x__ }}
    return
}

func DeserializeItemItem(_buf *serialization.ByteBuf) (*ItemItem, error) {
    v := &ItemItem{}
    if err := v.Deserialize(_buf); err == nil {
        return v, nil
    } else {
        return nil, err
    }
}
