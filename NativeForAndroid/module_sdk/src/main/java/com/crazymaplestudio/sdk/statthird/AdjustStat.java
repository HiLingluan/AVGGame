package com.crazymaplestudio.sdk.statthird;

import android.app.Application;
import android.content.Context;
import android.net.Uri;

import com.adjust.sdk.Adjust;
import com.adjust.sdk.AdjustAttribution;
import com.adjust.sdk.AdjustConfig;
import com.adjust.sdk.AdjustEvent;
import com.adjust.sdk.AdjustEventFailure;
import com.adjust.sdk.AdjustEventSuccess;
import com.adjust.sdk.AdjustSessionFailure;
import com.adjust.sdk.AdjustSessionSuccess;
import com.adjust.sdk.LogLevel;
import com.adjust.sdk.OnAttributionChangedListener;
import com.adjust.sdk.OnDeeplinkResponseListener;
import com.adjust.sdk.OnEventTrackingFailedListener;
import com.adjust.sdk.OnEventTrackingSucceededListener;
import com.adjust.sdk.OnSessionTrackingFailedListener;
import com.adjust.sdk.OnSessionTrackingSucceededListener;
import com.crazymaplestudio.sdk.deeplink.DeeplinkHelper;
import com.crazymaplestudio.sdk.tools.ApplicationTool;
import com.crazymaplestudio.sdk.tools.CMSLog;

import java.text.DecimalFormat;
import java.text.DecimalFormatSymbols;
import java.util.Locale;

/**
 * description
 * Created by jin
 * Created date: 2021-08-09
 */
public class AdjustStat {

    private static Context _content = null;

    /***
     * 在启动Application oncreate()  中调用
     * @param initToken
     * @param isSandBox
     */
    public static void init(String initToken,Boolean isSandBox) {

        Application app = ApplicationTool.getApplication();
        //Adjust
        String environment = isSandBox ? AdjustConfig.ENVIRONMENT_SANDBOX : AdjustConfig.ENVIRONMENT_PRODUCTION;
        AdjustConfig config = new AdjustConfig(app, initToken, environment);
        config.setLogLevel(LogLevel.VERBOSE);

        if (isSandBox) {
            config.setOnAttributionChangedListener(new OnAttributionChangedListener() {
                @Override
                public void onAttributionChanged(AdjustAttribution attribution) {
                    CMSLog.i("adjust attribution:"+attribution.toString());
                }
            });
            config.setOnEventTrackingSucceededListener(new OnEventTrackingSucceededListener() {
                @Override
                public void onFinishedEventTrackingSucceeded(AdjustEventSuccess eventSuccessResponseData) {
                    CMSLog.i("adjust finish evnet tracking success:"+eventSuccessResponseData.toString());
                }
            });
            config.setOnEventTrackingFailedListener(new OnEventTrackingFailedListener() {
                @Override
                public void onFinishedEventTrackingFailed(AdjustEventFailure eventFailureResponseData) {
                    CMSLog.i("adjust finish event tracking failed:"+eventFailureResponseData.toString());
                }
            });
            config.setOnSessionTrackingSucceededListener(new OnSessionTrackingSucceededListener() {
                @Override
                public void onFinishedSessionTrackingSucceeded(AdjustSessionSuccess sessionSuccessResponseData) {
                    CMSLog.i("adjust session tracking success:"+sessionSuccessResponseData.toString());
                }
            });
            config.setOnSessionTrackingFailedListener(new OnSessionTrackingFailedListener() {
                @Override
                public void onFinishedSessionTrackingFailed(AdjustSessionFailure sessionFailureResponseData) {
                    CMSLog.i("adjust session tracking failed:"+sessionFailureResponseData.toString());
                }
            });
            // Evaluate deferred deep link to be launched.
        }

        //adjust deeplink
        config.setOnDeeplinkResponseListener(new OnDeeplinkResponseListener() {
            @Override
            public boolean launchReceivedDeeplink(Uri deeplink) {
                if (deeplink!=null)
                {
                    DeeplinkHelper.CacheDepplink(deeplink.toString());
                    //this is deeplink callback .
                    //�����ӳ�����
                    //_content.CacheDepplink(deeplink);
                }
                return true;
            }
        });


        Adjust.onCreate(config);
        app.registerActivityLifecycleCallbacks(new AdjustLifecycleCallbacks());
    }

    //在启动activity oncreate()  中调用
    public static void start(Context context) {
        _content = context;
    }


    /**
     * 设置通知toketn
     *
     */
    public static void setNotifyToken(String token) {
        try {
            Adjust.setPushToken(token, _content);
        }catch (Exception e){
            CMSLog.i("Adjust setNotifyToken error.");
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
    public static void payFinishEvent(String adjustCode, String sku, int priceCent, String transactionId, String orderId) {
        try{
            //避免采用默认的地区格式化浮点数，导致无法解析含有逗号的字符传
            DecimalFormatSymbols symbols= new DecimalFormatSymbols(Locale.US);
            DecimalFormat df = new DecimalFormat("#.00",symbols);
            Double priceDeci = Double.parseDouble(df.format((new Double(priceCent / 100.00f))));

            AdjustEvent event = new AdjustEvent(adjustCode);
            event.setRevenue(priceDeci, "USD");
            event.setOrderId(orderId);

            //必须设置！！！   否则不会向第三方传递数据
            event.addPartnerParameter("price", priceDeci.toString());
            event.addPartnerParameter("orderid", orderId);
            event.addPartnerParameter("paycode", sku);
            event.addPartnerParameter("transactionid", transactionId);
            event.addPartnerParameter("currency", "USD");

            event.addCallbackParameter("price", priceDeci.toString());
            event.addCallbackParameter("orderid", orderId);
            event.addCallbackParameter("paycode", sku);
            event.addCallbackParameter("transactionid", transactionId);
            event.addCallbackParameter("currency", "USD");

            Adjust.trackEvent(event);
            CMSLog.i("adjust purchase===》" + sku + "===" + priceCent + "===" + transactionId + "===" + "===" + orderId );

        }catch (final Exception msg)
        {

        }
    }

    /**
     * adjust   普通统计事件
     *
     * @param eventName 事件名称
     */
    public static void logEvent(String eventName) {
            AdjustEvent event = new AdjustEvent(eventName);
            Adjust.trackEvent(event);

            CMSLog.i("adjust 通用事件统计===》" + eventName);
    }

}
