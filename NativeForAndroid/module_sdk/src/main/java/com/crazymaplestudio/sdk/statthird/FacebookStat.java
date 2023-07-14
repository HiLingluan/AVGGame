package com.crazymaplestudio.sdk.statthird;

import android.app.Application;
import android.content.Context;
import android.os.Bundle;

import com.crazymaplestudio.sdk.tools.ApplicationTool;
import com.crazymaplestudio.sdk.tools.CMSLog;
import com.facebook.FacebookSdk;
import com.facebook.appevents.AppEventsConstants;
import com.facebook.appevents.AppEventsLogger;

import java.math.BigDecimal;
import java.util.Currency;

/**
 * description
 * Created by jin
 * Created date: 2021-08-09
 */
public class FacebookStat {

    private static Context _context = null;

    // facebook 上报对象
    private static AppEventsLogger facebookLogger;

    /***
     * 在启动Application oncreate()  中调用
     */
    public static void init() {

        Application app = ApplicationTool.getApplication();
        AppEventsLogger.activateApp(app);
    }

    //在启动activity oncreate()  中调用
    public static void start(Context context) {
        _context = context;

        if (FacebookSdk.isInitialized()) {
            try {
                facebookLogger = AppEventsLogger.newLogger(_context);
                CMSLog.i("facebook stat initialed");
            } catch (Exception e) {
                CMSLog.i("facebook stat initial error:" + e.toString());
            }
        } else {
            CMSLog.i("facebook stat initial is not complete!!!");
            facebookLogger = null;
        }
    }

    /**
     * 付费完成
     * @param sku           计费点
     * @param priceCent     价格
     * @param transactionId 内部订单号
     * @param orderId       交易平台外部订单号
     */
    public static void payFinishEvent( String sku, float priceCent, String transactionId, String orderId) {
        if(facebookLogger==null) return;

        CMSLog.i( "facebookpayFinish===>" + sku + "===" + priceCent + "===" + transactionId + "===" + "===" + orderId );

        //官方事件统计
        Bundle params = new Bundle();
        params.putInt(AppEventsConstants.EVENT_PARAM_NUM_ITEMS, 1);
        params.putString(AppEventsConstants.EVENT_PARAM_CONTENT_TYPE, "product");
        params.putString(AppEventsConstants.EVENT_PARAM_CONTENT_ID, sku);
        params.putString(AppEventsConstants.EVENT_PARAM_CURRENCY, "USD");
        facebookLogger.logPurchase((BigDecimal.valueOf(priceCent)), Currency.getInstance("USD"), params);
        CMSLog.i("facebook stat purchase===》" + sku + "===" + priceCent + "===" + transactionId + "===" + "===" + orderId );
    }

    /**
     * facebook  普通统计事件
     *
     * @param eventName 事件名称
     */
    public static void logEvent(String eventName) {
        if(facebookLogger==null) return;

        try {
            Bundle bundle = new Bundle();
            facebookLogger.logEvent(eventName, bundle);

            CMSLog.i("facebook stat 通用事件统计===》" + eventName);
        } catch (Exception e) {
            CMSLog.i("facebook EventStat error===》" + e.toString());
        }
    }


}
