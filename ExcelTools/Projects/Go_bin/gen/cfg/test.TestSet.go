
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

type TestTestSet struct {
    Id int32
    X1 []int32
    X2 []int64
    X3 []string
    X4 []int32
}

const TypeId_TestTestSet = -543221516

func (*TestTestSet) GetTypeId() int32 {
    return -543221516
}

func (_v *TestTestSet)Serialize(_buf *serialization.ByteBuf) {
    // not support
}

func (_v *TestTestSet)Deserialize(_buf *serialization.ByteBuf) (err error) {
    { if _v.Id, err = _buf.ReadInt(); err != nil { err = errors.New("_v.Id error"); return } }
    {_v.X1 = make([]int32, 0); var _n_ int; if _n_, err = _buf.ReadSize(); err != nil { err = errors.New("_v.X1 error"); return}; for i := 0 ; i < _n_ ; i++ { var _e_ int32; { if _e_, err = _buf.ReadInt(); err != nil { err = errors.New("_e_ error"); return } }; _v.X1 = append(_v.X1, _e_) } }
    {_v.X2 = make([]int64, 0); var _n_ int; if _n_, err = _buf.ReadSize(); err != nil { err = errors.New("_v.X2 error"); return}; for i := 0 ; i < _n_ ; i++ { var _e_ int64; { if _e_, err = _buf.ReadLong(); err != nil { err = errors.New("_e_ error"); return } }; _v.X2 = append(_v.X2, _e_) } }
    {_v.X3 = make([]string, 0); var _n_ int; if _n_, err = _buf.ReadSize(); err != nil { err = errors.New("_v.X3 error"); return}; for i := 0 ; i < _n_ ; i++ { var _e_ string; { if _e_, err = _buf.ReadString(); err != nil { err = errors.New("_e_ error"); return } }; _v.X3 = append(_v.X3, _e_) } }
    {_v.X4 = make([]int32, 0); var _n_ int; if _n_, err = _buf.ReadSize(); err != nil { err = errors.New("_v.X4 error"); return}; for i := 0 ; i < _n_ ; i++ { var _e_ int32; { if _e_, err = _buf.ReadInt(); err != nil { err = errors.New("_e_ error"); return } }; _v.X4 = append(_v.X4, _e_) } }
    return
}

func DeserializeTestTestSet(_buf *serialization.ByteBuf) (*TestTestSet, error) {
    v := &TestTestSet{}
    if err := v.Deserialize(_buf); err == nil {
        return v, nil
    } else {
        return nil, err
    }
}
