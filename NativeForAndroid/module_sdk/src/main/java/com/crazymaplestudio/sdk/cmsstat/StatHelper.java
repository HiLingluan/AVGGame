package com.crazymaplestudio.sdk.cmsstat;

import android.content.Context;
import android.text.TextUtils;

import com.appsflyer.AFInAppEventParameterName;
import com.crazymaplestudio.sdk.CMSConstant;
import com.crazymaplestudio.sdk.CMSHelper;
import com.crazymaplestudio.sdk.networkstate.NetworkStateHelper;
import com.crazymaplestudio.sdk.sqlitedb.DbConstant;
import com.crazymaplestudio.sdk.sqlitedb.DbManager;
import com.crazymaplestudio.sdk.statthird.AppsFlyerStat;
import com.crazymaplestudio.sdk.tools.AppInfoTool;
import com.crazymaplestudio.sdk.tools.CMSLog;
import com.crazymaplestudio.sdk.tools.DeviceTool;
import com.crazymaplestudio.sdk.tools.LocalSaveTool;
import com.crazymaplestudio.sdk.tools.TimeTool;

import org.json.JSONException;
import org.json.JSONObject;

import java.util.HashMap;
import java.util.Map;

/**
 * The type Stat 35 util.
 */
public class StatHelper {

    private static Context _context = null;

    private static HashMap<String,Object> _deviceInfoMap;


    /**
     * Init.
     *
     * @param context the context
     */
    public static void init(Context context)
    {

        _context = context;

        if(CMSHelper.getConfig().getCmsStatUrl()!=null && !CMSHelper.getConfig().getCmsStatUrl().isEmpty()){
            StatRequest.setRequestUrl(CMSHelper.getConfig().getCmsStatUrl());
        }

        String r = LocalSaveTool.load("_guanjin_id");
        if(r.isEmpty()){
            STAT_ID = 0;
        }else {
            STAT_ID = Integer.parseInt(r);
        }
        //数据库初始化
        DbManager.getInstance(_context);

        initDeviceInfo();

        StatRequest.refreshAllRecords();

        //首次安装统计
        if(AppInfoTool.getIsFirstInstall()){
            appInstallReport();//SDK端上报的是m_app_install,unity端报的是m_app_install,可以区分
        }
//        appStartReport();//这个不在SDK报
    }


    //初始化设备相关信息 避免重复计算
    public static void initDeviceInfo(){
        _deviceInfoMap = new HashMap<String,Object>();

        String osVersion = DeviceTool.exec(CMSConstant.COMMAND_HARMONYOS_VERSION);
        if (!TextUtils.isEmpty(osVersion)) {
            _deviceInfoMap.put(StatConstant.KEY_OS_TYPE, "HarmonyOS");
            _deviceInfoMap.put(StatConstant.KEY_OS_VERSION, osVersion);
        } else {
            _deviceInfoMap.put(StatConstant.KEY_OS_TYPE, "android");
            _deviceInfoMap.put(StatConstant.KEY_OS_VERSION, DeviceTool.getOSVersion());
        }

        _deviceInfoMap.put(StatConstant.KEY_APP_ID, "cm1007");
        _deviceInfoMap.put(StatConstant.Key_PackageName,"com.maplehouse.gp.magic");

        //_deviceInfoMap.put(StatConstant.KEY_APP_CHANNEL_ID, AppInfoTool.getChannel());
        _deviceInfoMap.put(StatConstant.KEY_APP_VERSION, AppInfoTool.getAppVersionName());

//        _deviceInfoMap.put(StatConstant.KEY_APP_GAME_VERSION, "");
//        _deviceInfoMap.put(StatConstant.KEY_APP_RES_VERSION, "");
//        _deviceInfoMap.put(StatConstant.KEY_APP_INSTALL_ID, "");
        _deviceInfoMap.put(StatConstant.KEY_MANUFACTURER, DeviceTool.getManufacturer());
        _deviceInfoMap.put(StatConstant.KEY_BRAND, DeviceTool.getBrand());
        _deviceInfoMap.put(StatConstant.KEY_MODEL, DeviceTool.getModel());
        _deviceInfoMap.put(StatConstant.KEY_SCREEN_HEIGHT, DeviceTool.getDeviceHeight());
        _deviceInfoMap.put(StatConstant.KEY_SCREEN_WIDTH, DeviceTool.getDeviceWidth());
        _deviceInfoMap.put(StatConstant.KEY_DEVICE_LANG, DeviceTool.getSystemLanguage());


        String carrier = DeviceTool.getCarrier();
        if (!TextUtils.isEmpty(carrier)) {
            _deviceInfoMap.put(StatConstant.KEY_CARRIER, carrier);
        }

        String mAndroidId = DeviceTool.getDeviceId();
        if ( !TextUtils.isEmpty(mAndroidId)) {
            _deviceInfoMap.put(StatConstant.KEY_DEVICE_ID, mAndroidId);

        }

        String zone_offset = TimeTool.getOffsetFormat();
        _deviceInfoMap.put(StatConstant.KEY_TIMEZONE_OFFSET, zone_offset);

    }

    //通过key获取设备基础参数,后续可以使用这个接口,lua不用再传参数到原生
    public  static  String getDeviceInfo(String key)
    {
        Object param = _deviceInfoMap.get(key);
        if (param != null)
        {
            return  param.toString();
        }
        else{
            return "";
        }
    }

    //获取基础参数字符串
    public static String getStatBaseDataStr()
    {
        try {
            JSONObject obj = new JSONObject();

            //设备固定的参数 直接从初始化过的hashmap获取
            obj.put(StatConstant.KEY_OS,  _deviceInfoMap.get(StatConstant.KEY_OS));
            obj.put(StatConstant.KEY_OS_TYPE,  _deviceInfoMap.get(StatConstant.KEY_OS_TYPE));
            obj.put(StatConstant.KEY_OS_VERSION,  _deviceInfoMap.get(StatConstant.KEY_OS_VERSION));

            obj.put(StatConstant.KEY_APP_ID, _deviceInfoMap.get(StatConstant.KEY_APP_ID));
            obj.put(StatConstant.KEY_APP_CHANNEL_ID, AppInfoTool.getChannel());
//            obj.put(StatConstant.KEY_APP_VERSION, _deviceInfoMap.get(StatConstant.KEY_APP_VERSION));

            obj.put(StatConstant.Key_PackageName, _deviceInfoMap.get(StatConstant.Key_PackageName));

//            obj.put(StatConstant.KEY_MANUFACTURER, _deviceInfoMap.get(StatConstant.KEY_MANUFACTURER));
//            obj.put(StatConstant.KEY_BRAND, _deviceInfoMap.get(StatConstant.KEY_BRAND));
//            obj.put(StatConstant.KEY_MODEL, _deviceInfoMap.get(StatConstant.KEY_MODEL));
//            obj.put(StatConstant.KEY_DEVICE_LVL, _deviceInfoMap.get(StatConstant.KEY_DEVICE_LVL));
//            obj.put(StatConstant.KEY_SCREEN_HEIGHT, _deviceInfoMap.get(StatConstant.KEY_SCREEN_HEIGHT));
//            obj.put(StatConstant.KEY_SCREEN_WIDTH, _deviceInfoMap.get(StatConstant.KEY_SCREEN_WIDTH));

            obj.put(StatConstant.KEY_DEVICE_LANG, _deviceInfoMap.get(StatConstant.KEY_DEVICE_LANG));
            obj.put(StatConstant.KEY_CARRIER, _deviceInfoMap.get(StatConstant.KEY_CARRIER));
            obj.put(StatConstant.KEY_DEVICE_ID, _deviceInfoMap.get(StatConstant.KEY_DEVICE_ID));
            obj.put(StatConstant.KEY_TIMEZONE_OFFSET, _deviceInfoMap.get(StatConstant.KEY_TIMEZONE_OFFSET));


            //需要实时获取的参数
            obj.put(StatConstant.KEY_APP_VERSION, AppInfoTool.getAppVersionName());
            obj.put(StatConstant.KEY_OS_TIMESTAMP, TimeTool.getNowTimestamp());
            obj.put(StatConstant.KEY_NETWORK_TYPE, NetworkStateHelper.getNetworkState());
            obj.put(StatConstant.KEY_APP_LANG, AppInfoTool.getAppLanguage());
            obj.put(StatConstant.KEY_APP_GAME_VERSION, AppInfoTool.getCodeVersion());
            obj.put(StatConstant.KEY_APP_RES_VERSION, AppInfoTool.getResVersion());

            obj.put(StatConstant.KEY_APP_INSTALL_ID, ""+AppInfoTool.getInstallId());
            obj.put(StatConstant.KEY_APP_ACTIVATE_ID, ""+AppInfoTool.getRunId());
            //obj.put(StatConstant.KEY_AD_ID, DeviceTool.getADIDStr());
            obj.put(StatConstant.KEY_AD_ID, "");
            obj.put(StatConstant.KEY_ANDROID_ID, DeviceTool.getAndroidId());
            obj.put(StatConstant.KEY_IDFV, "");
            obj.put(StatConstant.KEY_APP_USER_ID, AppInfoTool.getUuid());
            return obj.toString();

        } catch (JSONException e) {
            e.printStackTrace();
        }
        return "";
    }

    /**
     * Gets base data.
     * @param eventName 事件名称
     * @return the base data
     */
    private static JSONObject getBaseData(String eventName)
    {
        try {
            JSONObject obj = new JSONObject();

            //设备固定的参数 直接从初始化过的hashmap获取
            obj.put(StatConstant.KEY_OS,  _deviceInfoMap.get(StatConstant.KEY_OS));
            obj.put(StatConstant.KEY_OS_TYPE,  _deviceInfoMap.get(StatConstant.KEY_OS_TYPE));
            obj.put(StatConstant.KEY_OS_VERSION,  _deviceInfoMap.get(StatConstant.KEY_OS_VERSION));

            obj.put(StatConstant.KEY_APP_ID, _deviceInfoMap.get(StatConstant.KEY_APP_ID));
            obj.put(StatConstant.KEY_APP_CHANNEL_ID, AppInfoTool.getChannel());
//            obj.put(StatConstant.KEY_APP_VERSION, _deviceInfoMap.get(StatConstant.KEY_APP_VERSION));

            obj.put(StatConstant.Key_PackageName, _deviceInfoMap.get(StatConstant.Key_PackageName));

//            obj.put(StatConstant.KEY_MANUFACTURER, _deviceInfoMap.get(StatConstant.KEY_MANUFACTURER));
//            obj.put(StatConstant.KEY_BRAND, _deviceInfoMap.get(StatConstant.KEY_BRAND));
//            obj.put(StatConstant.KEY_MODEL, _deviceInfoMap.get(StatConstant.KEY_MODEL));
//            obj.put(StatConstant.KEY_DEVICE_LVL, _deviceInfoMap.get(StatConstant.KEY_DEVICE_LVL));
//            obj.put(StatConstant.KEY_SCREEN_HEIGHT, _deviceInfoMap.get(StatConstant.KEY_SCREEN_HEIGHT));
//            obj.put(StatConstant.KEY_SCREEN_WIDTH, _deviceInfoMap.get(StatConstant.KEY_SCREEN_WIDTH));

            obj.put(StatConstant.KEY_DEVICE_LANG, _deviceInfoMap.get(StatConstant.KEY_DEVICE_LANG));
            obj.put(StatConstant.KEY_CARRIER, _deviceInfoMap.get(StatConstant.KEY_CARRIER));
            obj.put(StatConstant.KEY_DEVICE_ID, _deviceInfoMap.get(StatConstant.KEY_DEVICE_ID));
            obj.put(StatConstant.KEY_TIMEZONE_OFFSET, _deviceInfoMap.get(StatConstant.KEY_TIMEZONE_OFFSET));


            //需要实时获取的参数
            obj.put(StatConstant.KEY_APP_VERSION, AppInfoTool.getAppVersionName());
            obj.put(StatConstant.KEY_OS_TIMESTAMP, TimeTool.getNowTimestamp());
            obj.put(StatConstant.KEY_NETWORK_TYPE, NetworkStateHelper.getNetworkState());
            obj.put(StatConstant.KEY_APP_LANG, AppInfoTool.getAppLanguage());
            obj.put(StatConstant.KEY_APP_GAME_VERSION, AppInfoTool.getCodeVersion());
            obj.put(StatConstant.KEY_APP_RES_VERSION, AppInfoTool.getResVersion());
            
            obj.put(StatConstant.KEY_APP_INSTALL_ID, ""+AppInfoTool.getInstallId());
            obj.put(StatConstant.KEY_APP_ACTIVATE_ID, ""+AppInfoTool.getRunId());
            //obj.put(StatConstant.KEY_AD_ID, DeviceTool.getADIDStr());
            obj.put(StatConstant.KEY_AD_ID, "");
            obj.put(StatConstant.KEY_ANDROID_ID, DeviceTool.getAndroidId());
            obj.put(StatConstant.KEY_IDFV, "");
            obj.put(StatConstant.KEY_APP_USER_ID, AppInfoTool.getUuid());

            obj.put(StatConstant.KEY_EVENT_NAME, eventName);

            return obj;

        } catch (JSONException e) {
            e.printStackTrace();
        }

        return null;
    }

    private static int STAT_ID = 0;
    /**
     * 基础事件
     */
    public static void sendbaseEvent(String eventname, Map<String,Object> map)
    {
        try {
            JSONObject data = getBaseData(eventname);

            if(data!=null){

                JSONObject properties = new JSONObject();
                int stat_id = STAT_ID++;
                properties.put("_guanjin_id", stat_id);

                for (String key:map.keySet()) {
                    Object value = map.get(key);

                    properties.put(key,value);
                }

                //properties.put(StatConstant.KEY_APP_USER_ID,AppInfoTool.getUuid());

                data.put(StatConstant.KEY_PROPERTIES,properties);
                data.put(StatConstant.KEY_SUB_EVENT_NAME,"");
                data.put(DbConstant.KEY_ID, stat_id);
                StatRequest.addRequest(data.toString(),stat_id);
                LocalSaveTool.save("_guanjin_id",String.valueOf(stat_id));
            }
        } catch (JSONException e) {
            e.printStackTrace();
        }
    }

    public static void sendEventByJson(String eventname, String jsonProperties , String subEventName)
    {
        try {
            JSONObject data = getBaseData(eventname);
            if(data!=null){
                JSONObject properties = new JSONObject(jsonProperties);
                int stat_id = STAT_ID++;

                properties.put("_guanjin_id" , stat_id);
                data.put(StatConstant.KEY_PROPERTIES,properties);
                data.put(StatConstant.KEY_SUB_EVENT_NAME,subEventName);
                data.put(DbConstant.KEY_ID, stat_id);
                StatRequest.addRequest(data.toString(),stat_id);
                LocalSaveTool.save("_guanjin_id",String.valueOf(stat_id));
            }

        }catch (JSONException e) {
            e.printStackTrace();
        }
    }


    /**
     * 应用第一次启动  第一次安装 只上报一次
     */
    public static void appInstallReport()
    {
        Map<String, Object> obj = new HashMap<String, Object>();
        obj.put(StatConstant.KEY_MANUFACTURER, _deviceInfoMap.get(StatConstant.KEY_MANUFACTURER));
        obj.put(StatConstant.KEY_BRAND, _deviceInfoMap.get(StatConstant.KEY_BRAND));
        obj.put(StatConstant.KEY_MODEL, _deviceInfoMap.get(StatConstant.KEY_MODEL));
        //obj.put(StatConstant.KEY_DEVICE_LVL, _deviceInfoMap.get(StatConstant.KEY_DEVICE_LVL));
        obj.put(StatConstant.KEY_SCREEN_HEIGHT, _deviceInfoMap.get(StatConstant.KEY_SCREEN_HEIGHT));
        obj.put(StatConstant.KEY_SCREEN_WIDTH, _deviceInfoMap.get(StatConstant.KEY_SCREEN_WIDTH));
        obj.put(StatConstant.KEY_APP_LANG, _deviceInfoMap.get(StatConstant.KEY_APP_LANG));

        sendbaseEvent(StatEventName.APP_INSTALL_EVENT,obj);
    }
    
    /**
     * 应用启动
     */
    public static void appStartReport()
    {
        Map<String, Object> obj = new HashMap<String, Object>();
        obj.put(StatConstant.KEY_MANUFACTURER, _deviceInfoMap.get(StatConstant.KEY_MANUFACTURER));
        obj.put(StatConstant.KEY_BRAND, _deviceInfoMap.get(StatConstant.KEY_BRAND));
        obj.put(StatConstant.KEY_MODEL, _deviceInfoMap.get(StatConstant.KEY_MODEL));
        obj.put(StatConstant.KEY_SCREEN_HEIGHT, _deviceInfoMap.get(StatConstant.KEY_SCREEN_HEIGHT));
        obj.put(StatConstant.KEY_SCREEN_WIDTH, _deviceInfoMap.get(StatConstant.KEY_SCREEN_WIDTH));

        obj.put(StatConstant.KEY_CONVERSION_RAW, AppsFlyerStat.getConversionData());

        sendbaseEvent(StatEventName.APP_START_EVENT,obj);
    }

    /**
     * 应用结束
     */
    public static void appEndReport()
    {

    }

    /**
     *  测试
     */
    public static void testReport()
    {
        Map<String, Object> obj = new HashMap<String, Object>();
        obj.put(StatConstant.KEY_SCENE_NAME, "sdk_test");

        sendbaseEvent("cms_stat_test",obj);
    }

    /**
     *  测试
     */
    public static void testCheckList(){
        StatRequest.refreshAllRecords();
    }

}
