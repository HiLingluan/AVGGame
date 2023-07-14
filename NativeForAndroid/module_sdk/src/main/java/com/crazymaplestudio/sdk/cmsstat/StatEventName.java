package com.crazymaplestudio.sdk.cmsstat;

/**
 * description 预置统计事件 key
 * Created by jin
 * Created date: 2021-07-29
 */
public class StatEventName {
    //应用安装激活 应用安装后第一次激活上报
    public static final String APP_INSTALL_EVENT        = "m_app_install";
    //应用启动 每次应用启动上报
    public static final String APP_START_EVENT          = "m_app_start";
    //应用退出
    public static final String APP_END_EVENT            = "m_app_end";

    //场景进入
    public static final String SCENE_ENTER_EVENT        = "m_scene_enter";
    //退出场景
    public static final String SCENE_EXIT_EVENT         = "m_scene_exit";

    //加载页面
    public static final String PAGE_ENTER_EVENT         = "m_page_enter";
    //退出页面
    public static final String PAGE_EXIT_EVENT          = "m_page_exit";

    //弹框展示
    public static final String DLG_SHOW_EVENT           = "m_dlg_show";
    //弹框关闭
    public static final String DLG_DISMISS_EVENT        = "m_dlg_dismiss";

    //新用户注册
    public static final String APP_SIGNUP_EVENT         = "m_user_signup";
    //用户登陆
    public static final String APP_LOGIN_EVENT          = "m_user_signin";
    //用户登出
    public static final String APP_LOGOUT_EVENT         = "m_user_logout";

    //心跳 应用启动后每30s上报一次
    public static final String APP_HEART_EVENT          = "m_app_heart";
    //元素点击
    public static final String APP_ELEMENT_CLICK_EVENT  = "m_app_element_click";

    //开始支付 客户端开始拉起支付sdk
    public static final String PAY_START_EVENT          = "m_app_paystart";
    //支付完成 客户端SDK支付完成（尚未开始验证订单）
    public static final String PAY_END_EVENT            = "m_app_payend";
    //支付验证完成 客户端支付完成（已通过订单验证）
    public static final String PAY_COMPLETE_EVENT       = "m_app_paycomplete";
    //支付失败 （各种错误或用户主动中断都算作失败）
    public static final String PAY_FAILED_EVENT         = "m_app_payfailed";

    //虚拟币获得 物品数量变化统计物品数量变化统计（虚拟币、道具等物品变化统计）
    public static final String ITEM_CHANGE_EVENT        = "m_item_change";
    //资源包下载
    public static final String RES_DOWNLOAD_EVENT       = "m_ResDL";
    //变现广告
    public static final String MONEY_AD_EVENT           = "m_MoneyAd";

    //书籍封面pv
    public static final String PV_ITEM_EVENT            = "m_PvItem";
    //书籍阅读
    public static final String BOOK_READ_EVENT          = "m_BookRead";
    //应用内分享
    public static final String SHARE_EVENT              = "m_EventShare";
    //通用事件
    public static final String COMMON_EVENT             = "m_CommonEvent";

    public static final String ROLE_CREATE_EVENT        = "m_role_create";
}
