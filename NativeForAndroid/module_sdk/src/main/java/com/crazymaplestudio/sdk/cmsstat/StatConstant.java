package com.crazymaplestudio.sdk.cmsstat;

public class StatConstant {

    //常量
    //实时统计请求
    public static final int STAT_REALTIME  = 1;
    //可延时请求
    public static final int STAT_DELAY = 2;

    //##############################################################################################
    //事件名称
    public static final String KEY_EVENT_NAME       = "_event_name";
    //二级事件名
    public static final String KEY_EVENT_SUB_NAME   = "_event_sub_name";
    //事件持续时间
    public static final String KEY_EVENT_DURATION   = "_event_duration";
    //url链接
    public static final String KEY_URL              = "_url";
    //场景名称
    public static final String KEY_SCENE_NAME       = "_scene_name";
    //页面名
    public static final String KEY_PAGE_NAME        = "_page_name";
    //弹框名
    public static final String KEY_DLG_NAME         = "_dlg_name";
    //元素id
    public static final String KEY_ELEMENT_ID       = "_element_id";
    //元素名称
    public static final String KEY_ELEMENT_NAME     = "_element_name";
    //元素内容
    public static final String KEY_ELEMENT_CONTENT  = "_element_content";
    //元素种类
    public static final String KEY_ELEMENT_TYPE     = "_element_type";
    //元素位置  分辨率Y轴
    public static final String KEY_ELEMENT_POSITIONY= "_element_positionY";
    //元素位置 分辨率X轴
    public static final String KEY_ELEMENT_POSITIONX= "_element_positionX";
    //书架id
    public static final String KEY_SHELF_ID         = "_shelf_id";
    //书籍id
    public static final String KEY_BOOK_ID          = "_book_id";
    //章节id
    public static final String KEY_CHAP_ID          = "_chap_id";
    //对话id
    public static final String KEY_CHAT_ID          = "_chat_id";
    //选项id
    public static final String KEY_CHOICE_ID        = "_choice_id";
    //选项类型
    public static final String KEY_CHOICE_TYPE      = "_choice_type";

    //=====================================================================
    //应用订单id(应用订单流水号)
    public static final String KEY_APP_ORDERID      = "_app_orderid";
    //平台订单id (第三方支付平台生成)
    public static final String KEY_CHANNEL_ORDERID  = "_channel_orderid";
    //应用商品ID
    public static final String KEY_APP_SKU          = "_app_sku";
    //平台商品ID
    public static final String KEY_CHANNEL_SKU      = "_channel_sku";
    //实际支付订单价格 (单位:分) int
    public static final String KEY_ORDER_AMOUNT     = "_order_amount";
    // 订单支付货币类型（USD）
    public static final String KEY_ORDER_CURRENCY_TYPE  = "_order_currency_type";
    //错误码 
    public static final String KEY_ERROR_CODE       = "_err_code";
    //错误信息
    public static final String KEY_ERROR_INFO       = "_err_info";
    //物品ID  (详见物品ID列表)
    public static final String KEY_ITEM_ID          = "_item_id";
    //int 变化数量
    public static final String KEY_CHANGE_AMOUNT    = "_change_amount";
    // int 变化后数量
    public static final String KEY_LATTER_AMOUNT    = "_latter_amount";
    //变化原因
    public static final String KEY_CHANGE_REASON    = "_change_reason";

    //角色ID
    public static final String KEY_ROLE_ID          = "_role_id";
    //角色性别
    public static final String KEY_ROLE_GENDER      = "_role_gender";
    //角色名称
    public static final String KEY_ROLE_NAME        = "_role_name";
    //角色等级(当前等级)
    public static final String KEY_ROLE_LVL         = "_role_lvl";

    //=====================================================================
    //应用唯一标识
    public static final String KEY_APP_ID           = "_app_id";
    //APP包名
    public static final String Key_PackageName = "_package_name";
    //渠道id
    public static final String KEY_APP_CHANNEL_ID   = "_app_channel_id";
    //APP的应用版本(包体内置版本号 versionname)
    public static final String KEY_APP_VERSION      = "_app_version";
    //应用游戏版本号 
    public static final String KEY_APP_GAME_VERSION = "_app_game_version";
    //应用资源版本号
    public static final String KEY_APP_RES_VERSION  = "_app_res_version";
    //用安装后第一次启动随机生成 随机取值范围[10000000,99999999]  缓存在磁盘中
    public static final String KEY_APP_INSTALL_ID   = "_app_install_id";
    //应用每次冷启动启动随机生成 随机取值范围[10000000,99999999] int  缓存在内存中
    public static final String KEY_APP_ACTIVATE_ID  = "_app_activate_id";
    //应用语言代码
    public static final String KEY_APP_LANG         = "_app_lang";
    //账户类型  游客=vistor  facebook登录=fb  google登录=gp  苹果登录=apple
    public static final String KEY_APP_ACCOUNT_BINDTYPE  = "_app_account_bindtype";
    //用户id
    public static final String KEY_APP_USER_ID      = "_app_user_id";
    //SDK版本
    //public static final String KEY_LIB_VERSION      = "_app_sdk_version";

    //设备ID
    public static final String KEY_DEVICE_ID        = "_device_id";
    //广告id
    public static final String KEY_AD_ID            = "_ad_id";
    //android id
    public static final String KEY_ANDROID_ID       = "_androidid";
    //ios idfv
    public static final String KEY_IDFV             = "_idfv";
    //设备制造商
    public static final String KEY_MANUFACTURER     = "_device_manufacturer";
    //设备品牌
    public static final String KEY_BRAND            = "_device_brand";
    //设备型号
    public static final String KEY_MODEL            = "_device_model";
    //设备等级
    public static final String KEY_DEVICE_LVL       = "_device_lvl";
    //屏幕高度
    public static final String KEY_SCREEN_HEIGHT    = "_device_screen_h";
    //屏幕宽度
    public static final String KEY_SCREEN_WIDTH     = "_device_screen_w";
    //运营商名称
    public static final String KEY_CARRIER          = "_device_carrier";
    //网络类型
    public static final String KEY_NETWORK_TYPE     = "_device_network_type";
    //设备语言代码
    public static final String KEY_DEVICE_LANG      = "_device_lang";
    //操作系统
    public static final String KEY_OS               = "_os";
    //操作系统类型 （android/ios/macos/windows/...）
    public static final String KEY_OS_TYPE          = "_os_type";
    //操作系统版本
    public static final String KEY_OS_VERSION       = "_os_version";
    //时间戳(秒)
    public static final String KEY_OS_TIMESTAMP     = "_os_timestamp";
    //时区偏移量
    public static final String KEY_TIMEZONE_OFFSET  = "_os_timezone_offset";

    //其他数据 json
    public static final String KEY_PROPERTIES       = "properties";

    public static final String KEY_SUB_EVENT_NAME       = "_sub_event_name";

    //归因数据
    public static final String KEY_CONVERSION_RAW   = "_af_conversion_raw";

//    //网络IP
//    public static final String KEY_IP               = "_ip";
//    //国家
//    public static final String KEY_COUNTRY          = "_country";
//    //省份
//    public static final String KEY_PROVINCE         = "_province";
//    //城市
//    public static final String KEY_CITY             = "_city";

}
