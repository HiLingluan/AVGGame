package com.crazymaplestudio.sdk.firebase;

import android.app.Activity;
import android.content.Context;
import android.content.SharedPreferences;
import android.os.Bundle;

import com.crazymaplestudio.sdk.event.CMSEventConst;
import com.crazymaplestudio.sdk.event.CMSEventManager;
import com.crazymaplestudio.sdk.event.eventmodel.PayEvent;
import com.crazymaplestudio.sdk.tools.CMSLog;
import com.google.android.gms.tasks.OnSuccessListener;
import com.google.firebase.FirebaseApp;
import com.google.firebase.analytics.FirebaseAnalytics;
import com.google.firebase.crashlytics.FirebaseCrashlytics;
import com.google.firebase.iid.FirebaseInstanceId;
import com.google.firebase.iid.InstanceIdResult;

/**
 * description
 * Created by jin
 * Created date: 2021-05-28
 */
public class FirebaseHelper {

    private static Context _context = null;

    public static void init(Context context) {
        _context = context;

        //firebase initActivity
        if (FirebaseApp.getApps(context).isEmpty()) {
            FirebaseApp.initializeApp(context);
        }

        addFirebaseListener();
    }


    public static void addFirebaseListener() {
        try {
            FirebaseInstanceId.getInstance().getInstanceId().addOnSuccessListener(
                    new OnSuccessListener<InstanceIdResult>() {
                        @Override
                        public void onSuccess(InstanceIdResult instanceIdResult) {
                            String deviceToken = instanceIdResult.getToken();
                            //StatisticHelper.setNotifyToken(deviceToken);
                            setFCMToken(deviceToken);
                            CMSLog.i("set token  " + deviceToken);
                        }
                    }
            );
        } catch (Exception e) {
            CMSLog.i("FirebaseInstanceId error===>" + e.toString());
        }

    }

    /*
获取token
*/
    public static String getFCMToken(){
        String token = "unknown";
        try{
            token =  _context.getSharedPreferences("notify_token", Context.MODE_PRIVATE).getString("notify_token","unknown");
        } catch (Exception e){
            CMSLog.e(e.toString());
        }
        CMSLog.i("getFireBaseNotifyToken====>"+token);
        return token;
    }

    /*
        保存token
     */
    public static void setFCMToken(String token){
        if (token == null || "".equals(token)) {
            CMSLog.i("set token is null======>");
            return ;
        }
        try {
            //本地持久化保存
            SharedPreferences sp = _context.getSharedPreferences("notify_token", Activity.MODE_PRIVATE);
            SharedPreferences.Editor editor = sp.edit();
            editor.putString("notify_token",token.toString());
            editor.commit();
        } catch (Exception e) {
            CMSLog.e(e.toString());
        }
    }

    /**
     * FireBase 特殊事件上报
     *
     * @param eventName 一级事件名称
     */
    public static void logEvent(String eventName) {

        if ("".equals(eventName) ) {
            Bundle params = new Bundle();

            FirebaseAnalytics.getInstance(_context).logEvent(eventName, params);

            CMSLog.i("FireBase 通用事件统计===》" + eventName );
        }
    }


    /*
        FCM收到消息时调用此方法  通知游戏刷新消息列表
        info: FCM消息内容  暂未使用
     */
    public static void onFCMCall(final String info){
        if (info != null && _context!=null ){
            CMSLog.i("fcm callback===>"+info);
            ((Activity)_context).runOnUiThread(new Runnable() {
                @Override
                public void run() {
                    //这个地方需要用到unity 的回调
                    CMSEventManager.getInstance().postEvent(CMSEventConst.CMS_EVENT_FCM_CALL,info);
                    //UnityPlayer.UnitySendMessage("GameCenter","OnReceiveNotifyFromFirebase",info);
                }
            });
        }
    }


    //firebase unity和lua 报错上报
    public static void reportCrash( String errlog, String uuid, boolean isTestServer )
    {

        FirebaseCrashlytics.getInstance().setUserId("" + uuid);
        FirebaseCrashlytics.getInstance().setCustomKey("type","CS_LUA");
        FirebaseCrashlytics.getInstance().setCustomKey("isTestServer",isTestServer);

        Exception e = new Exception(errlog);
        FirebaseCrashlytics.getInstance().recordException(e);
    }

}
