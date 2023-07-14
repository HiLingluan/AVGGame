package com.crazymaplestudio.sdk;

/**
 * 存放一些常量
 */
public class CMSConstant {

    public static final String COMMAND_HARMONYOS_VERSION = "getprop hw_sc.build.platform.version";

    //登录类型 google
    public static final String LOGIN_TYPE_GOOGLE = "google";
    //登录类型 facebook
    public static final String LOGIN_TYPE_FACEBOOK = "facebook";
    //登录类型 apple
    public static final String LOGIN_TYPE_APPLE = "apple";

    //JNI   通用接口API标识
    final static String JLUA_API_NAME_CONSUME_FAILED = "CONSUME_FAILED";
    final static String JLUA_API_NAME_CONSUME_SUCCESS = "CONSUME_SUCCESS";
    final static String JLUA_API_NAME_V1_PAY_FAILED = "V1_PAY_FAILED";
    final static String JLUA_API_NAME_V1_PAY_ERROR = "V1_PAY_ERROR";


    final static String PLAY_STORE_URL = "https://play.google.com/store/apps/details?id=com.crazymaplestudio.sdk";//香港地区

    //无网络
    public static final int NETWORK_CLASS_NONE = 0;
    //wifi
    public static final int NETWORK_CLASS_WIFI = 1;
    //未知移动网络.
    public static final int NETWORK_CLASS_UNKNOWN = 0;
    //2G
    public static final int NETWORK_CLASS_2_G = 2;
    //3G
    public static final int NETWORK_CLASS_3_G = 3;
    //4G
    public static final int NETWORK_CLASS_4_G = 4;
    //5G
    public static final int NETWORK_CLASS_5_G = 5;

    //google 登录标记
    public static final int REQUEST_CODE_GOOGLE_SIGN = 9001;
}
