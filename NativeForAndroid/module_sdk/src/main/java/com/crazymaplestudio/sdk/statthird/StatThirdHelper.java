package com.crazymaplestudio.sdk.statthird;

import android.content.Context;

import com.crazymaplestudio.sdk.firebase.FirebaseHelper;
import com.crazymaplestudio.sdk.tools.CMSLog;


/**
 * Created by green on 2017/8/4.
 * for lua java statistic sdk  report
 */

public  class StatThirdHelper {
    final static String LOG_TAG = "cms_stat";

    private static Context _context = null;

    //sdk开关 fb默认关闭（2021.7.1 facebook已经废弃）
    private static Boolean use_facebook = true;

    private static Boolean use_firebase = true;

    private static Boolean use_adjust = false;

    private static Boolean use_appsflyer = true;


    /**
     * 初始化方法   在启动Application oncreate()  中调用
     * @param adjustToken adjust 初始化key
     * @param appsflyerKey appsflyer 初始化key
     */
    public static void init(String adjustToken,String appsflyerKey) {

        CMSLog.i(LOG_TAG, "StatisticHelper init adjustToken:" + adjustToken);
        CMSLog.i(LOG_TAG, "StatisticHelper init appsflyerKey:" + appsflyerKey);

        if(use_facebook){
            FacebookStat.init();
        }
        if(use_adjust){
            AdjustStat.init(adjustToken,false);
        }
        if(use_appsflyer){
            AppsFlyerStat.init(appsflyerKey);
        }
        //firebase 无需初始化
    }

    //在启动activity oncreate()  中调用
    public static void start(Context context) {
        _context = context;

        if(use_facebook){
            FacebookStat.start(context);
        }
        if(use_adjust){
            AdjustStat.start(context);
        }
        if(use_appsflyer){
            AppsFlyerStat.start(context);
        }
    }

    /**
     * 是否开启facebook上报
     * @param ison 是否开启
     */
    public static void setFacebookOn(Boolean ison) {
        use_facebook = ison;
    }

    /**
     * 是否开启firebase上报
     * @param ison 是否开启
     */
    public static void setFirebaseOn(Boolean ison) {
        use_firebase = ison;
    }

    /**
     * 是否开启adjust上报
     * @param ison 是否开启
     */
    public static void setAdjustOn(Boolean ison) {
        use_adjust = ison;
    }

    /**
     * 是否开启appsflyer上报
     * @param ison 是否开启
     */
    public static void setAppsflyerOn(Boolean ison) {
        use_appsflyer = ison;
    }


    /**
     * 是否开启appsflyer上报
     * @param token fcm推送token
     */
    public static void setNotifyToken(String token) {
        
        if(use_appsflyer){
            AppsFlyerStat.setNotifyToken(token);
        }
    }


    /**
     * 付费完成
     * @param adjustCode    adjust code
     * @param sku           计费点
     * @param priceCent     价格
     * @param transactionId 内部订单号
     * @param orderId       交易平台外部订单号
     */
    public static void payFinishEvent( String sku, float priceCent, String transactionId, String orderId,String adjustCode) {
        CMSLog.i(LOG_TAG, "payFinish===>" + sku + "===" + priceCent + "===" + transactionId + "===" + "===" + orderId );

        if(use_facebook){
            FacebookStat.payFinishEvent(sku,priceCent,transactionId,orderId);
        }
        if(use_adjust){
            //AdjustStat.payFinishEvent(adjustCode,sku,priceCent,transactionId,orderId);
        }
        if(use_appsflyer){
            AppsFlyerStat.payFinishEvent(sku,priceCent,transactionId,orderId);
        }
        //firebase 会自动收集
    }

    /**
     * 通用事件
     * @param eventName   事件名 或 adjust的 key
     * */
    public static void logEvent(String eventName) {
        if(use_facebook){
            FacebookStat.logEvent(eventName);
        }
        if(use_firebase){
            FirebaseHelper.logEvent(eventName);
        }
        if(use_adjust){
            AdjustStat.logEvent(eventName);
        }
        if(use_appsflyer){
            AppsFlyerStat.logEvent(eventName);
        }
    }
}

