package com.crazymaplestudio.sdk.event;

/**
 * description
 * Created by jin
 * Created date: 2021-06-21
 */
public class CMSEventConst {

    //监听所有回调
    public static final String CMS_EVENT_ALL_EVENT = "allEvent";
    //region 其他回调
    //相机or相册处理完回调.
    public static final String CMS_EVENT_IMAGE_BACK = "onImageBack";
    //On network change back.
    public static final String CMS_EVENT_NETWORK_CHANGE = "onNetworkChangeBack";
    //endregion
    //fcm回调到Unity
    public static final String CMS_EVENT_FCM_CALL = "OnFcmCallBack";

    //region 支付回调
    // 支付成功回调
    public static final String CMS_EVENT_PAY_SUCCESS = "OnPaySuccessV2";
    //支付失败回调
    public static final String CMS_EVENT_PAY_FAILED = "OnPayFailedV2";
    //支付取消回调
    public static final String CMS_EVENT_PAY_CANCELED = "OnPayCanceledV2";
    //计费点消费失败回调
    public static final String CMS_EVENT_PAY_CONSUME_FAIL = "onConsumeFailBack";
    //计费点消费成功回调.
    public static final String CMS_EVENT_PAY_CONSUME_SUCCESS = "onConsumeSucessBack";
    //计费点确认成功回调
    public static final String CMS_EVENT_PAY_ACKNOWLEDGE_FAIL = "onAcknowledgeFailBack";
    //计费点确认失败回调.
    public static final String CMS_EVENT_PAY_ACKNOWLEDGE_SUCCESS = "onAcknowledgeSucessBack";
    //获取订阅信息回调.
    public static final String CMS_EVENT_PAY_SUBSCRIBE_INFO = "OnGetSubScribeCallback";
    // 计费点查询成功
    public static final String CMS_EVENT_PAY_QUERY_SUCCESS = "onSkuQuerySuccess";
    // 计费点查询失败
    public static final String CMS_EVENT_PAY_QUERY_FAIL = "onSkuQueryFail";
    //endregion

    //region 广告-插屏回调
    //广告 sdk初始化成功
    public static final String CMS_EVENT_AD_INIT = "onAdInitBack";
    //插屏广告加载成功
    public static final String CMS_EVENT_AD_INTER_LOAD_SUCCESS = "onAdInterLoadSuccess";
    //插屏广告加载失败
    public static final String CMS_EVENT_AD_INTER_LOAD_FAIL = "onAdInterLoadFail";
    //插屏广告播放
    public static final String CMS_EVENT_AD_INTER_PLAY = "onAdInterPlay";
    //插屏广告点击
    public static final String CMS_EVENT_AD_INTER_CLICK = "onAdInterClick";
    //插屏广告完成
    public static final String CMS_EVENT_AD_INTER_COMPLETE = "onAdInterPlayComplete";
    //endregion

    //region 广告-激励回调
    //激励广告加载成功
    public static final String CMS_EVENT_AD_REWARD_LOAD_SUCCESS = "onAdRewardLoadSuccess";
    //激励广告加载失败
    public static final String CMS_EVENT_AD_REWARD_LOAD_FAIL = "onAdRewardLoadFail";
    //激励广告播放
    public static final String CMS_EVENT_AD_REWARD_PLAY = "onAdRewardPlay";
    //激励广告点击
    public static final String CMS_EVENT_AD_REWARD_CLICK = "onAdRewardClick";
    //激励广告播放失败
    public static final String CMS_EVENT_AD_REWARD_PLAY_FAIL = "onAdRewardPlayFailed";
    //激励广告播放成功
    public static final String CMS_EVENT_AD_REWARD_PLAY_SUCCESS = "onAdRewardPlaySuccess";
    //endregion

      //region 输入框回调
    //输入框出现
    public static final String CMS_EVENT_INPUT_KEYBOARD_SHOW = "onKeyBoardShow";
    //输入框隐藏
    public static final String CMS_EVENT_INPUT_KEYBOARD_HIDE = "onKeyBoardHide";
    //输入完成
    public static final String CMS_EVENT_INPUT_EDIT_BACK = "onEditInfoBack";
    //输入框文本变化
    public static final String CMS_EVENT_INPUT_GET_CHANGE = "onGetChangeContent";
    //endregion

    //region 第三方登录回调
    //第三方登录开始
    public static final String CMS_EVENT_LOGIN_START = "onThirdLoginStart";
    //第三方登录完成
    public static final String CMS_EVENT_LOGIN_FINISH = "onThirdLoginFinish";
    //第三方登录失败
    public static final String CMS_EVENT_LOGIN_FAILED = "onThirdLoginFailed";
    //endregion

    //region 权限回调
    //权限申请成功回调
    public static final String CMS_EVENT_PERMISSION_SUCCESS = "onPermissionRequestSuccess";
    //权限申请失败回调
    public static final String CMS_EVENT_PERMISSION_FAILED = "onPermissionRequestFailed";
    //没权限提示
    public static final String CMS_EVENT_PERMISSION_TIPS = "onNonePermissionTips";
    //endregion
}

