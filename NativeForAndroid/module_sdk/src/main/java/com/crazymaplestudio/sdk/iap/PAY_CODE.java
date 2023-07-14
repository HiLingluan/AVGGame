package com.crazymaplestudio.sdk.iap;

//SDK支付流程错误码(整合sdk原始错误码)
public class PAY_CODE {
    //超时(sdk)
    public final static int SERVICE_TIMEOUT = -3;
    //不支持次特性(sdk)
    public final static int FEATURE_NOT_SUPPORTED = -2;
    //play服务链接已断开(sdk)
    public final static int SERVICE_DISCONNECTED = -1;
    //play服务不可用(sdk)
    public final static int SERVICE_UNAVAILABLE = 2;
    //支付不可用(sdk)
    public final static int BILLING_UNAVAILABLE = 3;
    //商品无效（已下架）(sdk)
    public final static int ITEM_UNAVAILABLE = 4;
    //开发者错误(sdk)
    public final static int DEVELOPER_ERROR = 5;
    //其他错误(sdk)
    public final static int ERROR = 6;
    //商品已经购买（未消耗）(sdk)
    public final static int ITEM_ALREADY_OWNED = 7;
    //商品未购买(sdk)
    public final static int ITEM_NOT_OWNED = 8;
    //支付sdk未初始化
    public final static int SDK_NOT_READY = 51;
    //缓存列表未找到计费点
    public final static int SKU_NOT_FIND = 52;
    //**************************************
    //支付成功
    public final static int PAY_SUCCCESS = 100;
    //用户主动取消
    public final static int PAY_USER_CANCEL = 101;
    // 拉起支付SDK错误
    public final static int PAY_FLOW_FAIL = 102;
    // 拉起支付 时获取sdk信息失败
    public final static int PAY_SKUDETAIL_FAIL = 103;
    //订单消耗成功
    public final static int CONSUME_SUCCESS = 200;
    //消耗-获取订单token失败
    public final static int CONSUME_TOKEN_FAIL = 201;
    //订阅确认成功
    public final static int ACKNOWLEDGE_SUCCESS = 300;
    //确认失败
    public final static int ACKNOWLEDGE_FAIL = 301;
    //计费点查询成功
    public final static int QUERY_SUCCESS = 400;
}
