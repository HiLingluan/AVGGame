
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

package cfg

import "luban_examples/go_bin/bright/serialization"

type TestTbTestSep struct {
    _dataMap map[int32]*TestTestSep
    _dataList []*TestTestSep
}

func NewTestTbTestSep(_buf *serialization.ByteBuf) (*TestTbTestSep, error) {
	if size, err := _buf.ReadSize() ; err != nil {
		return nil, err
	} else {
		_dataList := make([]*TestTestSep, 0, size)
		dataMap := make(map[int32]*TestTestSep)

		for i := 0 ; i < size ; i++ {
			if _v, err2 := DeserializeTestTestSep(_buf); err2 != nil {
				return nil, err2
			} else {
				_dataList = append(_dataList, _v)
				dataMap[_v.Id] = _v
			}
		}
		return &TestTbTestSep{_dataList:_dataList, _dataMap:dataMap}, nil
	}
}

func (table *TestTbTestSep) GetDataMap() map[int32]*TestTestSep {
    return table._dataMap
}

func (table *TestTbTestSep) GetDataList() []*TestTestSep {
    return table._dataList
}

func (table *TestTbTestSep) Get(key int32) *TestTestSep {
    return table._dataMap[key]
}

