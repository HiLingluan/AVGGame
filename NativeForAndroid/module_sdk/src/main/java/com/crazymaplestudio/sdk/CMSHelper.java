package com.crazymaplestudio.sdk;

import android.app.Activity;
import android.app.Application;
import android.content.Context;
import android.content.Intent;

import androidx.annotation.NonNull;

import com.crazymaplestudio.sdk.ads.AdsHelper;
import com.crazymaplestudio.sdk.deeplink.DeeplinkHelper;
import com.crazymaplestudio.sdk.event.CMSEventListener;
import com.crazymaplestudio.sdk.event.CMSEventManager;
import com.crazymaplestudio.sdk.firebase.FirebaseHelper;
import com.crazymaplestudio.sdk.iap.PayHelper;
import com.crazymaplestudio.sdk.inputview.InputHelper;
import com.crazymaplestudio.sdk.login.FacebookLogin;
import com.crazymaplestudio.sdk.login.GoogleLogin;
import com.crazymaplestudio.sdk.login.LoginHelper;
import com.crazymaplestudio.sdk.networkstate.NetworkStateHelper;
import com.crazymaplestudio.sdk.cmsstat.StatHelper;
import com.crazymaplestudio.sdk.statthird.AppsFlyerStat;
import com.crazymaplestudio.sdk.statthird.StatThirdHelper;
import com.crazymaplestudio.sdk.tools.AppInfoTool;
import com.crazymaplestudio.sdk.tools.ApplicationTool;
import com.crazymaplestudio.sdk.tools.CMSLog;
import com.crazymaplestudio.sdk.tools.DeviceTool;
import com.crazymaplestudio.sdk.tools.FileTool;
import com.crazymaplestudio.sdk.tools.GooglePlayTool;
import com.crazymaplestudio.sdk.tools.ImageTool;
import com.crazymaplestudio.sdk.tools.LocalSaveTool;
import com.crazymaplestudio.sdk.tools.NotchTool;
import com.crazymaplestudio.sdk.tools.PermissionRequest;
import com.crazymaplestudio.sdk.tools.PermissionUtil;
import com.crazymaplestudio.sdk.tools.ShareTool;
import com.crazymaplestudio.sdk.tools.SystemTool;

import java.net.URISyntaxException;

/**
 * sdk通用管理类
 * Created by jin
 * Created date: 2021-04-01
 */
public class CMSHelper {

    private static Context _context = null;


    private static CMSConfig _config = null;

    public static CMSConfig getConfig() {
        return _config;
    }

    public static void initApplication(Application app, CMSConfig config) {

        _config = config;

        CMSLog.setLogEnable(config.getIsLogOn());

        ApplicationTool.init(app);

        //第三方统计 开关配置
        StatThirdHelper.setFacebookOn(config.getUseFacebookStat());
        StatThirdHelper.setFirebaseOn(config.getUseFirebaseStat());
        StatThirdHelper.setAdjustOn(config.getUseAdjustStat());
        StatThirdHelper.setAppsflyerOn(config.getUseAppsflyerStat());
        //第三方统计初始化
        StatThirdHelper.init(config.getAdjustInitToken(), config.getAppsflyerKey());
    }


    public static void initActivity(Context context) {
        _context = context;

        if (_config == null) {
            CMSLog.i("CMSConfig not init");
        }
        String pkg = context.getApplicationContext().getPackageName();
        FileTool.packageName = pkg;
        //初始化
        //本地存取记录
        LocalSaveTool.init(context);
        DeviceTool.init(context);
        SystemTool.init(context);
        AppInfoTool.init(context);

        //需要优先初始化权限
        PermissionRequest.init((Activity) context);
        PermissionUtil.init((Activity) context);

        //自家统计
        StatHelper.init(context);

        //第三方统计
        StatThirdHelper.start(context);

        //广告  J2项目不介入广告   暂时屏蔽
//        AdsHelper.init((Activity) context, _config.getAdunitInit(), _config.getInterAdunit1(), _config.getInterAdunit2());

        //图片工具
        ImageTool.init((Activity) context);

        //google play
        GooglePlayTool.init((Activity) context);

        //支付
        PayHelper.init((Activity) context);

        //分享
        ShareTool.init(context);

        //刘海屏+导航栏
        NotchTool.init(context);

        //自定义输入框
        InputHelper.init(context);

        //firebase 初始化 必需放在登录初始化之前
        FirebaseHelper.init(context);

        //登录
        LoginHelper.init(context, _config.getGoogleLoginKey());

        //facebook link
        DeeplinkHelper.init(context);

        //网络状态变化
        NetworkStateHelper.init(context);

        //appsflyer 延迟初始化
        AppsFlyerStat.start(context);


    }

    //获取当前cms sdk版本号
    public static String getSdkVersion() {
        return BuildConfig.SDK_VERSION;
    }

    public static void onNewIntent(Intent intent) {
        DeeplinkHelper.checkLink(intent);

    }

    // Quit
    public static void onDestroy() {
        PayHelper.onDestory();
        NetworkStateHelper.onDestory();
    }

    // Pause
    public static void onPause() {
        InputHelper.hideInputArea();

    }

    // Resume
    public static void onResume() {

    }

    //低内存检测
    public static void onLowMemory() {

    }

    /**
     * 请求权限回调函数     需要根据requestCode和grantResults做后续处理
     *
     * @param requestCode  请求码   例：写入权限WRITE_EXTERNAL_STORAGE_REQUEST_CODE
     * @param grantResults 授权结果
     */
    public static void onRequestPermissionsResult(int requestCode, @NonNull String[] permissions, @NonNull int[] grantResults) {
        PermissionRequest.onRequestPermissionsResult(requestCode, permissions, grantResults);
    }

    /**
     * 界面之间跳转和传递数据的回调
     *
     * @param requestCode 请求码    根据不同的请求码，设置不同的传递内容  例：A跳向B，B返回A
     * @param resultCode  返回码    也就是在B中设置的int的数值，这个是得到返回的内容的标识
     * @param data        是Intent数据    B中的setResult方法中传递了一些数据，在A中就可以通过解析Intent的内容来获得传递过来的数据
     */
    public static void onActivityResult(int requestCode, int resultCode, Intent data) {
        CMSLog.i("onActivityResult requestCode" + requestCode);

        //google login
        GoogleLogin.onActivityResult(requestCode, resultCode, data);
        //facebook login
        FacebookLogin.onActivityResult(requestCode, resultCode, data);

        //图片筛选器
        try {
            ImageTool.onActivityResult(requestCode, resultCode, data);
        } catch (URISyntaxException e) {
            e.printStackTrace();
        }

        //支付回调
        //PayHelper.onActivityResult(requestCode, resultCode, data);
    }


    public static void addEventListener(CMSEventListener listener) {
        CMSEventManager.getInstance().addListener(listener);
    }

    public static void removeListener(CMSEventListener listener) {
        CMSEventManager.getInstance().removeListener(listener);
    }

    public static void postEvent(String eventName, Object eventObject) {
        CMSEventManager.getInstance().postEvent(eventName, eventObject);
    }
}
