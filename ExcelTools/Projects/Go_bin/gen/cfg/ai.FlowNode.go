
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

type AiFlowNode struct {
    Id int32
    NodeName string
    Decorators []interface{}
    Services []interface{}
}

const TypeId_AiFlowNode = -1109240970

func (*AiFlowNode) GetTypeId() int32 {
    return -1109240970
}

func (_v *AiFlowNode)Serialize(_buf *serialization.ByteBuf) {
    // not support
}

func (_v *AiFlowNode)Deserialize(_buf *serialization.ByteBuf) (err error) {
    { if _v.Id, err = _buf.ReadInt(); err != nil { err = errors.New("_v.Id error"); return } }
    { if _v.NodeName, err = _buf.ReadString(); err != nil { err = errors.New("_v.NodeName error"); return } }
    {_v.Decorators = make([]interface{}, 0); var _n_ int; if _n_, err = _buf.ReadSize(); err != nil { err = errors.New("_v.Decorators error"); return}; for i := 0 ; i < _n_ ; i++ { var _e_ interface{}; { if _e_, err = DeserializeAiDecorator(_buf); err != nil { err = errors.New("_e_ error"); return } }; _v.Decorators = append(_v.Decorators, _e_) } }
    {_v.Services = make([]interface{}, 0); var _n_ int; if _n_, err = _buf.ReadSize(); err != nil { err = errors.New("_v.Services error"); return}; for i := 0 ; i < _n_ ; i++ { var _e_ interface{}; { if _e_, err = DeserializeAiService(_buf); err != nil { err = errors.New("_e_ error"); return } }; _v.Services = append(_v.Services, _e_) } }
    return
}

func DeserializeAiFlowNode(_buf *serialization.ByteBuf) (interface{}, error) {
    var id int32
    var err error
    if id, err = _buf.ReadInt() ; err != nil {
        return nil, err
    }
    switch id {
        case -1789006105: _v := &AiSequence{}; if err = _v.Deserialize(_buf); err != nil { return nil, errors.New("ai.Sequence") } else { return _v, nil }
        case -1946981627: _v := &AiSelector{}; if err = _v.Deserialize(_buf); err != nil { return nil, errors.New("ai.Selector") } else { return _v, nil }
        case -1952582529: _v := &AiSimpleParallel{}; if err = _v.Deserialize(_buf); err != nil { return nil, errors.New("ai.SimpleParallel") } else { return _v, nil }
        case -512994101: _v := &AiUeWait{}; if err = _v.Deserialize(_buf); err != nil { return nil, errors.New("ai.UeWait") } else { return _v, nil }
        case 1215378271: _v := &AiUeWaitBlackboardTime{}; if err = _v.Deserialize(_buf); err != nil { return nil, errors.New("ai.UeWaitBlackboardTime") } else { return _v, nil }
        case 514987779: _v := &AiMoveToTarget{}; if err = _v.Deserialize(_buf); err != nil { return nil, errors.New("ai.MoveToTarget") } else { return _v, nil }
        case -918812268: _v := &AiChooseSkill{}; if err = _v.Deserialize(_buf); err != nil { return nil, errors.New("ai.ChooseSkill") } else { return _v, nil }
        case -2140042998: _v := &AiMoveToRandomLocation{}; if err = _v.Deserialize(_buf); err != nil { return nil, errors.New("ai.MoveToRandomLocation") } else { return _v, nil }
        case -969953113: _v := &AiMoveToLocation{}; if err = _v.Deserialize(_buf); err != nil { return nil, errors.New("ai.MoveToLocation") } else { return _v, nil }
        case 1357409728: _v := &AiDebugPrint{}; if err = _v.Deserialize(_buf); err != nil { return nil, errors.New("ai.DebugPrint") } else { return _v, nil }
        default: return nil, errors.New("unknown type id")
    }
}

