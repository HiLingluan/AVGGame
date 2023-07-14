package com.crazymaplestudio.sdk.networkstate;

import android.content.Context;
import android.content.IntentFilter;
import android.net.ConnectivityManager;
import android.net.NetworkInfo;
import android.os.Build;
import android.telephony.TelephonyManager;
import android.util.Log;

import com.crazymaplestudio.sdk.CMSConstant;

/**
 * description
 * Created by jin
 * Created date: 2021-05-28
 */
public class NetworkStateHelper {

    private static Context _context = null;

    public static int curNetState = CMSConstant.NETWORK_CLASS_WIFI;

    private static NetBroadcastReceiver netBroadcastReceiver;

    public static void init(Context context) {
        _context = context;


        curNetState = getNetworkState();

        //增加网络变化监听
        //Android 7.0以上需要动态注册
        if (Build.VERSION.SDK_INT >= Build.VERSION_CODES.N) {
            //实例化IntentFilter对象
            IntentFilter filter = new IntentFilter();
            filter.addAction("android.net.conn.CONNECTIVITY_CHANGE");
            netBroadcastReceiver = new NetBroadcastReceiver();
            //注册广播接收
            context.registerReceiver(netBroadcastReceiver, filter);
        }
    }

    public static Context getContext() {
        return _context;
    }

    //获取网络状态及类型
    public static int getNetworkState() {
        //得到连接管理器对象
        if(_context!=null)
        {
            try {
                ConnectivityManager connectivityManager = (ConnectivityManager) _context.getSystemService(_context.CONNECTIVITY_SERVICE);

                NetworkInfo activeNetworkInfo = connectivityManager.getActiveNetworkInfo();

                //如果网络连接，判断该网络类型
                if (activeNetworkInfo != null && activeNetworkInfo.isAvailable() && activeNetworkInfo.isConnected()) {
                    if (activeNetworkInfo.getType() == (ConnectivityManager.TYPE_WIFI)) {
                        //wifi
                        return CMSConstant.NETWORK_CLASS_WIFI;
                    } else if (activeNetworkInfo.getType() == (ConnectivityManager.TYPE_MOBILE)) {
                        //mobile
                        try {
                            TelephonyManager telephonyManager = (TelephonyManager) _context.getSystemService(Context.TELEPHONY_SERVICE);
                            int networkType  = telephonyManager.getNetworkType();
                            switch (networkType)
                            {
                                case TelephonyManager.NETWORK_TYPE_GPRS:
                                    return CMSConstant.NETWORK_CLASS_2_G;
                                case TelephonyManager.NETWORK_TYPE_EDGE:
                                    return CMSConstant.NETWORK_CLASS_2_G;
                                case TelephonyManager.NETWORK_TYPE_CDMA:
                                    return CMSConstant.NETWORK_CLASS_2_G;
                                case TelephonyManager.NETWORK_TYPE_1xRTT:
                                    return CMSConstant.NETWORK_CLASS_2_G;
                                case TelephonyManager.NETWORK_TYPE_IDEN:
                                    return CMSConstant.NETWORK_CLASS_2_G;
                                case TelephonyManager.NETWORK_TYPE_UMTS:
                                    return CMSConstant.NETWORK_CLASS_3_G;
                                case TelephonyManager.NETWORK_TYPE_EVDO_0:
                                    return CMSConstant.NETWORK_CLASS_3_G;
                                case TelephonyManager.NETWORK_TYPE_EVDO_A:
                                    return CMSConstant.NETWORK_CLASS_3_G;
                                case TelephonyManager.NETWORK_TYPE_HSDPA:
                                    return CMSConstant.NETWORK_CLASS_3_G;
                                case TelephonyManager.NETWORK_TYPE_HSUPA:
                                    return CMSConstant.NETWORK_CLASS_3_G;
                                case TelephonyManager.NETWORK_TYPE_HSPA:
                                    return CMSConstant.NETWORK_CLASS_3_G;
                                case TelephonyManager.NETWORK_TYPE_EVDO_B:
                                    return CMSConstant.NETWORK_CLASS_3_G;
                                case TelephonyManager.NETWORK_TYPE_EHRPD:
                                    return CMSConstant.NETWORK_CLASS_3_G;
                                case TelephonyManager.NETWORK_TYPE_HSPAP:
                                    return CMSConstant.NETWORK_CLASS_3_G;
                                case TelephonyManager.NETWORK_TYPE_LTE:
                                    return CMSConstant.NETWORK_CLASS_4_G;
                                case TelephonyManager.NETWORK_TYPE_NR:
                                    return CMSConstant.NETWORK_CLASS_5_G;
                                case TelephonyManager.NETWORK_TYPE_GSM:
                                    return CMSConstant.NETWORK_CLASS_2_G;
                                case TelephonyManager.NETWORK_TYPE_TD_SCDMA:
                                    return CMSConstant.NETWORK_CLASS_3_G;
                                case TelephonyManager.NETWORK_TYPE_IWLAN:
                                    return CMSConstant.NETWORK_CLASS_4_G;
                                default:
                                    return CMSConstant.NETWORK_CLASS_UNKNOWN;
                            }
                        }catch (Exception e){
                            Log.e("spotlight","获取网络类型失败");
                        }
                        return CMSConstant.NETWORK_CLASS_UNKNOWN;
                    }
                } else {
                    //网络异常
                    return CMSConstant.NETWORK_CLASS_NONE;
                }
            }
            catch (Exception e)
            {
                return CMSConstant.NETWORK_CLASS_NONE;
            }
        }
        return CMSConstant.NETWORK_CLASS_NONE;
    }

    public static void onDestory() {
        if(_context!=null && netBroadcastReceiver!=null) {
            _context.unregisterReceiver(netBroadcastReceiver);
            netBroadcastReceiver = null;
        }

    }

    public static boolean isWifi() {
        return getNetworkState()==CMSConstant.NETWORK_CLASS_WIFI;
    }
}
