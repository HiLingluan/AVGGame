
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

type TestDemoType2 struct {
    X4 int32
    X1 bool
    X2 byte
    X3 int16
    X5 int64
    X6 float32
    X7 float64
    X80 int16
    X8 int32
    X9 int64
    X10 string
    X12 *TestDemoType1
    X13 int32
    X14 interface{}
    S1 string
    V2 serialization.Vector2
    V3 serialization.Vector3
    V4 serialization.Vector4
    T1 int32
    K1 []int32
    K2 []int32
    K5 []int32
    K8 map[int32]int32
    K9 []*TestDemoE2
    K15 []interface{}
}

const TypeId_TestDemoType2 = -367048295

func (*TestDemoType2) GetTypeId() int32 {
    return -367048295
}

func (_v *TestDemoType2)Serialize(_buf *serialization.ByteBuf) {
    // not support
}

func (_v *TestDemoType2)Deserialize(_buf *serialization.ByteBuf) (err error) {
    { if _v.X4, err = _buf.ReadInt(); err != nil { err = errors.New("_v.X4 error"); return } }
    { if _v.X1, err = _buf.ReadBool(); err != nil { err = errors.New("_v.X1 error"); err = errors.New("_v.X1 error"); return } }
    { if _v.X2, err = _buf.ReadByte(); err != nil { err = errors.New("_v.X2 error"); return } }
    { if _v.X3, err = _buf.ReadShort(); err != nil { err = errors.New("_v.X3 error"); return } }
    { if _v.X5, err = _buf.ReadLong(); err != nil { err = errors.New("_v.X5 error"); return } }
    { if _v.X6, err = _buf.ReadFloat(); err != nil { err = errors.New("_v.X6 error"); return } }
    { if _v.X7, err = _buf.ReadDouble(); err != nil { err = errors.New("_v.X7 error"); return } }
    { if _v.X80, err = _buf.ReadFshort(); err != nil { err = errors.New("_v.X80 error"); return } }
    { if _v.X8, err = _buf.ReadFint(); err != nil { err = errors.New("_v.X8 error"); return } }
    { if _v.X9, err = _buf.ReadFlong(); err != nil { err = errors.New("_v.X9 error"); return } }
    { if _v.X10, err = _buf.ReadString(); err != nil { err = errors.New("_v.X10 error"); return } }
    { if _v.X12, err = DeserializeTestDemoType1(_buf); err != nil { err = errors.New("_v.X12 error"); return } }
    { if _v.X13, err = _buf.ReadInt(); err != nil { err = errors.New("_v.X13 error"); return } }
    { if _v.X14, err = DeserializeTestDemoDynamic(_buf); err != nil { err = errors.New("_v.X14 error"); return } }
    { if _, err = _buf.ReadString(); err != nil { return }; if _v.S1, err = _buf.ReadString(); err != nil { err = errors.New("_v.S1 error"); return } }
    { if _v.V2, err = _buf.ReadVector2(); err != nil { err = errors.New("_v.V2 error"); return } }
    { if _v.V3, err = _buf.ReadVector3(); err != nil { err = errors.New("_v.V3 error"); return } }
    { if _v.V4, err = _buf.ReadVector4(); err != nil { err = errors.New("_v.V4 error"); return } }
    { if _v.T1, err = _buf.ReadInt(); err != nil { err = errors.New("_v.T1 error"); return } }
    {_v.K1 = make([]int32, 0); var _n_ int; if _n_, err = _buf.ReadSize(); err != nil { err = errors.New("_v.K1 error"); return}; for i := 0 ; i < _n_ ; i++ { var _e_ int32; { if _e_, err = _buf.ReadInt(); err != nil { err = errors.New("_e_ error"); return } }; _v.K1 = append(_v.K1, _e_) } }
    {_v.K2 = make([]int32, 0); var _n_ int; if _n_, err = _buf.ReadSize(); err != nil { err = errors.New("_v.K2 error"); return}; for i := 0 ; i < _n_ ; i++ { var _e_ int32; { if _e_, err = _buf.ReadInt(); err != nil { err = errors.New("_e_ error"); return } }; _v.K2 = append(_v.K2, _e_) } }
    {_v.K5 = make([]int32, 0); var _n_ int; if _n_, err = _buf.ReadSize(); err != nil { err = errors.New("_v.K5 error"); return}; for i := 0 ; i < _n_ ; i++ { var _e_ int32; { if _e_, err = _buf.ReadInt(); err != nil { err = errors.New("_e_ error"); return } }; _v.K5 = append(_v.K5, _e_) } }
    { _v.K8 = make(map[int32]int32); var _n_ int; if _n_, err = _buf.ReadSize(); err != nil { err = errors.New("_v.K8 error"); return}; for i := 0 ; i < _n_ ; i++ { var _key_ int32; { if _key_, err = _buf.ReadInt(); err != nil { err = errors.New("_key_ error"); return } }; var _value_ int32; { if _value_, err = _buf.ReadInt(); err != nil { err = errors.New("_value_ error"); return } }; _v.K8[_key_] = _value_} }
    {_v.K9 = make([]*TestDemoE2, 0); var _n_ int; if _n_, err = _buf.ReadSize(); err != nil { err = errors.New("_v.K9 error"); return}; for i := 0 ; i < _n_ ; i++ { var _e_ *TestDemoE2; { if _e_, err = DeserializeTestDemoE2(_buf); err != nil { err = errors.New("_e_ error"); return } }; _v.K9 = append(_v.K9, _e_) } }
    {_v.K15 = make([]interface{}, 0); var _n_ int; if _n_, err = _buf.ReadSize(); err != nil { err = errors.New("_v.K15 error"); return}; for i := 0 ; i < _n_ ; i++ { var _e_ interface{}; { if _e_, err = DeserializeTestDemoDynamic(_buf); err != nil { err = errors.New("_e_ error"); return } }; _v.K15 = append(_v.K15, _e_) } }
    return
}

func DeserializeTestDemoType2(_buf *serialization.ByteBuf) (*TestDemoType2, error) {
    v := &TestDemoType2{}
    if err := v.Deserialize(_buf); err == nil {
        return v, nil
    } else {
        return nil, err
    }
}
