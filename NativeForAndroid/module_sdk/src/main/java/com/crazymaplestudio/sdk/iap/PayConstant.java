package com.crazymaplestudio.sdk.iap;

/**
 * description
 * Created by jin
 * Created date: 2021-06-30
 */
public class PayConstant {

}


// 普通订单状态（是否有消费记录）
class PURCHASE_STATE{
    final static int HAS  = 1; //有未消费记录
    final static int NONE = 2; //无未消费记录
}

// 订阅订单状态（是否有确认）
class SUB_STATE{
    final static int NORECORD = 0; //无记录
    final static int UNACKNOWLEDGED = 1; //未确认
    final static int ACKNOWLEDGED = 2; //已确认
}


