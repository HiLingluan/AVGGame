package com.crazymaplestudio.sdk.statthird;

import android.app.Application;
import android.content.Context;

import androidx.annotation.NonNull;
import androidx.core.util.Consumer;

import com.appsflyer.AFInAppEventParameterName;
import com.appsflyer.AFInAppEventType;
import com.appsflyer.AppsFlyerConversionListener;
import com.appsflyer.AppsFlyerLib;
import com.appsflyer.adrevenue.AppsFlyerAdRevenue;
import com.appsflyer.adrevenue.adnetworks.AppsFlyerAdRevenueWrapperType;
import com.appsflyer.adrevenue.data.model.AppsFlyerAdEvent;
import com.appsflyer.deeplink.DeepLink;
import com.appsflyer.deeplink.DeepLinkListener;
import com.appsflyer.deeplink.DeepLinkResult;
import com.crazymaplestudio.sdk.deeplink.DeeplinkHelper;
import com.crazymaplestudio.sdk.tools.ApplicationTool;
import com.crazymaplestudio.sdk.tools.CMSLog;

import org.json.JSONException;
import org.json.JSONObject;

import java.text.DecimalFormat;
import java.text.DecimalFormatSymbols;
import java.util.HashMap;
import java.util.Iterator;
import java.util.Locale;
import java.util.Map;

/**
 * description
 * Created by jin
 * Created date: 2021-08-06
 */
public class AppsFlyerStat {

    private static Context _context = null;
    private static final String LOG_TAG = "cms_appsflyer";

    private static String _deeplink = null;


    private static String _conversionData = "";
    private static int _conversionFlag = 0;

    private static String _attributionData = "";
    private static int _attributionFlag = 0;

    //在启动Application oncreate()  中调用
    public static void init(String af_dev_key) {

        addDeeplinkListener();
        addAdRevenueListener();

        AppsFlyerConversionListener conversionListener = new AppsFlyerConversionListener() {
            @Override
            public void onConversionDataSuccess(Map<String, Object> conversionData) {

                try {
                    //首次安装之后的转化数据
                    JSONObject jsonObj = new JSONObject();
                    for (String attrName : conversionData.keySet()) {
                        CMSLog.i(LOG_TAG, "Conversion attribute: " + attrName + " = " + conversionData.get(attrName));

                        jsonObj.put(attrName,conversionData.get(attrName));
                    }
                    _conversionFlag = 1;
                    _conversionData = jsonObj.toString();

                } catch (JSONException e) {
                    e.printStackTrace();
                }
//                if (conversionData.containsKey("is_first_launch")) {
//                    //是否首次登录
//                    Boolean is_first_launch = Boolean.valueOf(conversionData.get("is_first_launch").toString());
//
//                }
            }

            @Override
            public void onConversionDataFail(String errorMessage) {
                CMSLog.i(LOG_TAG, "error getting conversion data: " + errorMessage);
                _conversionFlag = 2;
                _conversionData = errorMessage;
            }

            @Override
            public void onAppOpenAttribution(Map<String, String> attributionData) {

                // Legacy methods for deep linking and deferred deep linking
                for (String attrName : attributionData.keySet()) {
                    String deepLinkAttrStr = attrName + " = " + attributionData.get(attrName);
                    CMSLog.i(LOG_TAG, "onAppOpenAttribution attribute: " + deepLinkAttrStr);
                }

                try {
                    //首次安装之后的转化数据
                    JSONObject jsonObj = new JSONObject();
                    for (String attrName : attributionData.keySet()) {
                        String deepLinkAttrStr = attrName + " = " + attributionData.get(attrName);
                        CMSLog.i(LOG_TAG, "onAppOpenAttribution attribute: " + deepLinkAttrStr);

                        jsonObj.put(attrName,attributionData.get(attrName));
                    }
                    _attributionFlag = 1;
                    _attributionData = jsonObj.toString();

                } catch (JSONException e) {
                    e.printStackTrace();
                }

                // 这种方式任何android版本都可以获取
//                if (!attributionData.containsKey("is_first_launch")) {
//                    CMSLog.i(LOG_TAG, "onAppOpenAttribution: This is direct deep linking(直接)");
//                }else {
//                    CMSLog.i(LOG_TAG, "onAppOpenAttribution: This is deferred deep linking(延时)");
//                }


//
//                if(attributionData.containsKey("deep_link_value")){
//                    CMSLog.i(LOG_TAG, "onAppOpenAttribution: Deep linking deep_link_value: " + attributionData.get("deep_link_value"));
//
//                    _deeplink = attributionData.get("deep_link_value");
//
//                    callActivityLink();
//                }
            }

            @Override
            public void onAttributionFailure(String errorMessage) {
                CMSLog.i(LOG_TAG, "error onAttributionFailure : " + errorMessage);
                _attributionFlag = 2;
                _attributionData = errorMessage;
            }
        };

        Application app = ApplicationTool.getApplication();
        AppsFlyerLib.getInstance().init(af_dev_key, conversionListener, app);
        AppsFlyerLib.getInstance().start(app);
    }

    /***
     * 添加Deeplink 事件监听
     */
    public static void addDeeplinkListener() {

        DeepLinkListener deeplinklistener = new DeepLinkListener(){
            @Override
            public void onDeepLinking(@NonNull DeepLinkResult deepLinkResult) {
                DeepLinkResult.Status dlStatus = deepLinkResult.getStatus();
                if (dlStatus == DeepLinkResult.Status.FOUND) {
                    CMSLog.i(LOG_TAG, "appsflyer Deep link found");
                } else if (dlStatus == DeepLinkResult.Status.NOT_FOUND) {
                    CMSLog.i(LOG_TAG, "appsflyer Deep link not found");
                    return;
                } else {
                    // dlStatus == DeepLinkResult.Status.ERROR
                    DeepLinkResult.Error dlError = deepLinkResult.getError();
                    CMSLog.i(LOG_TAG, "get appsflyer Deep Link error: " + dlError.toString());
                    return;
                }

                DeepLink deepLinkObj = deepLinkResult.getDeepLink();
                try {
                    CMSLog.i(LOG_TAG, "appsflyer DeepLink data is: " + deepLinkObj.toString());
                } catch (Exception e) {
                    CMSLog.i(LOG_TAG, "appsflyer DeepLink data is null");
                    return;
                }
                // 判断是不是延时深度链接
                if (deepLinkObj.isDeferred()) {
                    CMSLog.i(LOG_TAG, "appsflyer DeepLink This is a deferred deep link(延时)");
                } else {
                    CMSLog.i(LOG_TAG, "appsflyer DeepLink This is a direct deep link(直接)");
                }



                try {
                    _deeplink = deepLinkObj.getDeepLinkValue();
                    CMSLog.i(LOG_TAG, "appsflyer DeepLink  : " + _deeplink);
                    callActivityLink();

                } catch (Exception e) {
                    CMSLog.i(LOG_TAG, "appsflyer param fruit_name was not found in DeepLink data");
                    return;
                }

            }
        };

        AppsFlyerLib.getInstance().subscribeForDeepLink(deeplinklistener);
    }

    /***
     * 添加广告收入监听
     */
    public static void addAdRevenueListener() {
        Application app = ApplicationTool.getApplication();
        AppsFlyerAdRevenue.Builder afRevnueBuilder = new AppsFlyerAdRevenue.Builder(app);
        afRevnueBuilder.addNetworks(AppsFlyerAdRevenueWrapperType.MOPUB);

        //Optional
        afRevnueBuilder.adEventListener(new Consumer<AppsFlyerAdEvent>() {
            @Override
            public void accept(AppsFlyerAdEvent appsFlyerAdEvent) {
                appsFlyerAdEvent.getAdNetworkEventType();
                appsFlyerAdEvent.getAdNetworkName();
                appsFlyerAdEvent.getAdNetworkPayload();
                appsFlyerAdEvent.getAdNetworkActionName();

                CMSLog.i(LOG_TAG, "AppsFlyer addAdRevenue:"+appsFlyerAdEvent.toHashMap().toString());
            }
        });

        AppsFlyerAdRevenue.initialize(afRevnueBuilder.build());
        AppsFlyerAdRevenue.moPubWrapper().recordImpressionData();
    }





    //在启动activity oncreate()  中调用
    public static void start(Context context) {
        _context = context;

        //callActivityLink();
    }

    public static void callActivityLink() {
        if(_deeplink!=null && _context!=null){
            //Uri link = Uri.parse(_deeplink);
            if(!_deeplink.isEmpty()) {
                DeeplinkHelper.CacheDepplink(_deeplink) ;
                //((MainActivity)_context).CacheDepplink(link);

                CMSLog.i("AppsFlyer deeplink callActivityLink: " + _deeplink);
            }
        }
    }

    /**
     * 设置通知toketn
     */
    public static void setNotifyToken(String token) {

    }

    /**
     * 设置用户uuid
     */
    public static void setUserId(String uuid) {
        AppsFlyerLib.getInstance().setCustomerUserId(uuid);
    }

    /**
     * 付费完成
     * @param sku           计费点
     * @param priceCent     价格 美分
     * @param transactionId 内部订单号
     * @param orderId       交易平台外部订单号
     */
    public static void payFinishEvent( String sku, float priceCent, String transactionId, String orderId) {

        DecimalFormatSymbols symbols= new DecimalFormatSymbols(Locale.US);
        DecimalFormat df = new DecimalFormat("#.00",symbols);
        //float priceDeci = Float.parseFloat(df.format((Float.valueOf(priceCent / 100.00f))));

        Map<String, Object> eventValue = new HashMap<String, Object>();
        eventValue.put(AFInAppEventParameterName.REVENUE, priceCent);
        eventValue.put(AFInAppEventParameterName.CURRENCY, "USD");
        eventValue.put(AFInAppEventParameterName.QUANTITY, 1);
        eventValue.put(AFInAppEventParameterName.CONTENT_ID, sku);
        eventValue.put(AFInAppEventParameterName.ORDER_ID, orderId);
        eventValue.put("af_transitionid", transactionId);
        AppsFlyerLib.getInstance().logEvent(_context,AFInAppEventType.PURCHASE, eventValue);

        CMSLog.i("jin Appsflyer  payFinishReport===》sku:" + sku + " === priceDeci:" + priceCent + " === transactionId:" + transactionId + " === orderId:" + orderId );
    }

    /**
     * 获取转化数据
     */
    public static String getAppsflyerData() {
        try {
            //首次安装之后的转化数据
            JSONObject jsonObj = new JSONObject();

            jsonObj.put("conversionFlag",_conversionFlag);
            jsonObj.put("conversionData",_conversionData);

            jsonObj.put("attributionFlag",_attributionFlag);
            jsonObj.put("attributionData",_attributionData);

            return jsonObj.toString();
        } catch (JSONException e) {
            e.printStackTrace();
        }
        return "";
    }

    public static String getConversionData() {
        return _conversionData;
    }

    /**
     * 普通统计事件
     *
     * @param eventName 事件名称
     */
    public static void logEvent( String eventName) {
        Map<String, Object> eventValue = new HashMap<String, Object>();
        AppsFlyerLib.getInstance().logEvent(_context,eventName, eventValue);

        CMSLog.i("jin Appsflyer logEvent "+eventName);
    }

    /**
     * 普通统计事件
     *
     * @param eventName 事件名称
     * @param eventMap 事件map
     */
    public static void logEventWithMap( String eventName,Map<String, Object> eventMap) {
        AppsFlyerLib.getInstance().logEvent(_context,eventName, eventMap);

        CMSLog.i("jin Appsflyer logEventWithMap "+eventName);
    }

    /**
     * 普通统计事件
     *
     * @param eventName 事件名称
     * @param jsondata json数据
     */
    public static void logEventWithJson( String eventName,String jsondata) {

        CMSLog.i("jin Appsflyer logEventWithJson eventName:"+eventName+" ,jsondata:"+jsondata);

        try {
            Map<String, Object> eventMap = new HashMap<>();

            JSONObject jsonInfo = new JSONObject(jsondata);
            Iterator iter = jsonInfo.keys();
            while (iter.hasNext()) {
                String key = iter.next().toString();
                Object value =  jsonInfo.get(key);
                eventMap.put(key,value);
            }

            AppsFlyerLib.getInstance().logEvent(_context, eventName, eventMap);
        } catch (JSONException e) {
            e.printStackTrace();
        }
    }





}
