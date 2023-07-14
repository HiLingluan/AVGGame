

package com.crazymaplestudio.sdk.tools;

import android.app.ActivityManager;
import android.content.Context;
import android.content.pm.ApplicationInfo;
import android.content.pm.PackageInfo;
import android.content.pm.PackageManager;
import android.os.Bundle;
import android.text.TextUtils;

import java.util.List;

public class AppInfoTool {

    private static Context _context;

    private static String mAppVersionName;
    public static int mAppVersionCode;
    private static Bundle mConfigBundle;

    //uuid 游戏端传入
    private static String  _uuid  = "";
    //渠道
    private static String  _channel  = "";

    //运行随机id
    private static int  _runnid  = 0;
    //当前安装随机id
    private static int  _installid  = 0;

    private static boolean _isFirstInstall = false;

    //当前代码版本号(热更版本号)
    private static String  _cversion  = "";

    //当前资源版本号
    private static String  _resVersion  = "";

    //当前app 语言（非设备）
    private static String  _appLanguage = "";



    /**
     * 获取 App 的 ApplicationId
     *
     * @return ApplicationId
     */
    public static void init(Context context) {
        _context = context;

        String r = LocalSaveTool.load(LocalSaveTool.INSTRALL_ID_KEY);
        if(r.isEmpty()){
            getInstallId();
            LocalSaveTool.save(LocalSaveTool.INSTRALL_ID_KEY,String.valueOf(_installid));

            _isFirstInstall = true;
        }else {
            _installid = Integer.parseInt(r);
        }
    }

    public static int getRunId(){
        if(_runnid==0)
        {
            _runnid = RandomTool.getNum(10000000, 99999999);
        }
        return _runnid;
    }
    /**
     * Read install id int.
     *
     * @return the int
     */
    //从本地读取安装id
    public static int getInstallId(){
        if(_installid==0){
            _installid = RandomTool.getNum(10000000, 99999999);
        }
        return _installid;
    }

    public static boolean getIsFirstInstall()
    {
        return _isFirstInstall;
    }

    /**
     * Gets uuid.
     *
     * @return the uuid
     */
    public static String getUuid() {
        return _uuid;
    }

    /**
     * Sets uuid.
     *
     * @param v the v
     */
    public static void setUuid(String v) {
        _uuid = v;
    }

    /**
     * Sets channel.
     *
     * @param v the v
     */
    public static void setChannel(String v) {
        _channel = v;
    }

    /**
     * Gets channel.
     *
     * @return the channel
     */
    public static String getChannel() {
        return _channel;
    }

    /**
     * Gets code version.
     *
     * @return the code version
     */
    public static String getCodeVersion() {
        return _cversion;
    }

    /**
     * Sets code version.
     *
     * @param v the v
     */
    public static void setCodeVersion(String v) {
        _cversion = v;
    }

    public static String getAppLanguage() {
        return _appLanguage;
    }

    public static void setAppLanguage(String lang) {
        AppInfoTool._appLanguage = lang;
    }

    public static String getResVersion() {
        return _resVersion;
    }

    public static void setResVersion(String _resVersion) {
        AppInfoTool._resVersion = _resVersion;
    }

    /**
     * 获取应用名称
     *
     * @param context Context
     * @return 应用名称
     */
    public static CharSequence getAppName(Context context) {
        if (context == null) return "";
        try {
            PackageManager packageManager = context.getPackageManager();
            ApplicationInfo appInfo = packageManager.getApplicationInfo(context.getPackageName(),
                    PackageManager.GET_META_DATA);
            return appInfo.loadLabel(packageManager);
        } catch (Exception e) {
            CMSLog.e("getAppName error:"+e.getMessage());
        }
        return "";
    }

    /**
     * 获取 App 的 ApplicationId
     *
     * @return ApplicationId
     */
    public static String getProcessName() {
        if (_context == null) return "";
        try {
            return _context.getApplicationInfo().processName;
        } catch (Exception e) {
            CMSLog.e("getProcessName error:"+e.getMessage());
        }
        return "";
    }

    /**
     * 获取 App 版本号
     *
     * @return App 的版本号
     */
    public static String getAppVersionName() {
        if (_context == null) return "";
        if (!TextUtils.isEmpty(mAppVersionName)) {
            return mAppVersionName;
        }
        try {
            PackageManager packageManager = _context.getPackageManager();
            PackageInfo packageInfo = packageManager.getPackageInfo(_context.getPackageName(), 0);
            mAppVersionName = packageInfo.versionName;
            mAppVersionCode = packageInfo.versionCode;
        } catch (Exception e) {
            CMSLog.e("getAppVersionName error:"+e.getMessage());
        }
        return mAppVersionName;
    }

    /**
     * 获取主进程的名称
     *
     * @param context Context
     * @return 主进程名称
     */
    public static String getMainProcessName(Context context) {
        if (context == null) {
            return "";
        }
        try {
            return context.getApplicationContext().getApplicationInfo().processName;
        } catch (Exception e) {
            CMSLog.e("getMainProcessName error:"+e.getMessage());
        }
        return "";
    }

    /**
     * 判断当前进程名称是否为主进程
     *
     * @param context Context
     * @param bundle Bundle
     * @return 是否主进程
     */
    public static boolean isMainProcess(Context context, Bundle bundle) {
        if (context == null) {
            return false;
        }

        String mainProcessName = com.crazymaplestudio.sdk.tools.AppInfoTool.getMainProcessName(context);
        if (TextUtils.isEmpty(mainProcessName) && bundle != null) {
            mainProcessName = bundle.getString("com.sensorsdata.analytics.android.MainProcessName");
        }

        if (TextUtils.isEmpty(mainProcessName)) {
            return true;
        }

        String currentProcess = getCurrentProcessName(context.getApplicationContext());
        return TextUtils.isEmpty(currentProcess) || mainProcessName.equals(currentProcess);
    }

    /**
     * 判断线程是否埋点执行线程
     * @return true，埋点执行线程；false，非埋点执行线程
     */
//    public static boolean isTaskExecuteThread() {
//        return TextUtils.equals(ThreadNameConstants.THREAD_TASK_EXECUTE, Thread.currentThread().getName());
//    }

    /**
     * 获取 Application 标签的 Bundle 对象
     * @param context Context
     * @return Bundle
     */
    public static Bundle getAppInfoBundle(Context context) {
        if (mConfigBundle == null) {
            try {
                final ApplicationInfo appInfo = context.getApplicationContext().getPackageManager()
                        .getApplicationInfo(context.getPackageName(), PackageManager.GET_META_DATA);
                mConfigBundle = appInfo.metaData;
            } catch (final PackageManager.NameNotFoundException e) {
                CMSLog.e("getAppInfoBundle error:"+e.getMessage());
            }
        }

        if (mConfigBundle == null) {
            return new Bundle();
        }
        return mConfigBundle;
    }

    /**
     * 获得当前进程的名字
     *
     * @param context Context
     * @return 进程名称
     */
    private static String getCurrentProcessName(Context context) {
        try {
            int pid = android.os.Process.myPid();
            ActivityManager activityManager = (ActivityManager) context.getSystemService(Context.ACTIVITY_SERVICE);
            if (activityManager == null) {
                return null;
            }

            List<ActivityManager.RunningAppProcessInfo> runningAppProcessInfoList = activityManager.getRunningAppProcesses();
            if (runningAppProcessInfoList != null) {
                for (ActivityManager.RunningAppProcessInfo appProcess : runningAppProcessInfoList) {
                    if (appProcess != null) {
                        if (appProcess.pid == pid) {
                            return appProcess.processName;
                        }
                    }
                }
            }
        } catch (Exception e) {
            CMSLog.e("getCurrentProcessName error:"+e.getMessage());
        }
        return null;
    }
}
