
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

package cfg

import "luban_examples/go_bin/bright/serialization"

type TestTbCompositeJsonTable1 struct {
    _dataMap map[int32]*TestCompositeJsonTable1
    _dataList []*TestCompositeJsonTable1
}

func NewTestTbCompositeJsonTable1(_buf *serialization.ByteBuf) (*TestTbCompositeJsonTable1, error) {
	if size, err := _buf.ReadSize() ; err != nil {
		return nil, err
	} else {
		_dataList := make([]*TestCompositeJsonTable1, 0, size)
		dataMap := make(map[int32]*TestCompositeJsonTable1)

		for i := 0 ; i < size ; i++ {
			if _v, err2 := DeserializeTestCompositeJsonTable1(_buf); err2 != nil {
				return nil, err2
			} else {
				_dataList = append(_dataList, _v)
				dataMap[_v.Id] = _v
			}
		}
		return &TestTbCompositeJsonTable1{_dataList:_dataList, _dataMap:dataMap}, nil
	}
}

func (table *TestTbCompositeJsonTable1) GetDataMap() map[int32]*TestCompositeJsonTable1 {
    return table._dataMap
}

func (table *TestTbCompositeJsonTable1) GetDataList() []*TestCompositeJsonTable1 {
    return table._dataList
}

func (table *TestTbCompositeJsonTable1) Get(key int32) *TestCompositeJsonTable1 {
    return table._dataMap[key]
}

