package com.crazymaplestudio.sdk.tools;

import android.app.Activity;
import android.content.Context;
import android.os.Build;
import android.util.DisplayMetrics;
import android.view.Display;
import android.view.WindowManager;

import com.crazymaplestudio.sdk.notchtools.NotchTools;
import com.crazymaplestudio.sdk.notchtools.core.NotchProperty;
import com.crazymaplestudio.sdk.notchtools.core.OnNotchCallBack;
import com.crazymaplestudio.sdk.notchtools.helper.NotchStatusBarUtils;

/**
 * 刘海屏和导航栏工具
 * Created by jin
 * Created date: 2021-05-28
 */
public class NotchTool {

    private static Context _context = null;

    private static int notchHeight = 0;

    //主acvity 调用
    public static void init(Context context) {
        _context = context;

        NotchStatusBarUtils.setFullScreenWithSystemUi(((Activity)context).getWindow(), false);
    }

    //计算刘海屏高度调用
    //unity的话最好等unity初始化之后再调用
    public static void initNotchScreen() {
        NotchTools.getFullScreenTools().fullScreenDontUseStatus((Activity) _context, new OnNotchCallBack() {
            @Override
            public void onNotchPropertyCallback(NotchProperty notchProperty) {
                notchHeight = notchProperty.geNotchHeight();

                CMSLog.i("Notch notchHeight 高度： " + notchHeight);
            }
        });
    }


    //刘海屏高度
    public static int GetNotchHeight()
    {
        return notchHeight;
    }


    /**
     * 是否存在导航栏
     *
     * @return the boolean
     */
    public static boolean isNavigationBarExist() {
        WindowManager windowManager = ((Activity) _context).getWindowManager();
        Display d = windowManager.getDefaultDisplay();

        DisplayMetrics realDisplayMetrics = new DisplayMetrics();
        if (Build.VERSION.SDK_INT >= Build.VERSION_CODES.JELLY_BEAN_MR1) {
            d.getRealMetrics(realDisplayMetrics);
        }

        int realHeight = realDisplayMetrics.heightPixels;
        int realWidth = realDisplayMetrics.widthPixels;

        DisplayMetrics displayMetrics = new DisplayMetrics();
        d.getMetrics(displayMetrics);

        int displayHeight = displayMetrics.heightPixels;
        int displayWidth = displayMetrics.widthPixels;

        return (realWidth - displayWidth) > 0 || (realHeight - displayHeight) > 0;
    }


    /**
     * 获取导航栏高度
     *
     * @param
     * @return
     */
    public static int getNavigationBarHeight() {
        int result = 0;
        int resourceId = 0;
        int rid = _context.getResources().getIdentifier("config_showNavigationBar", "bool", "android");
        if (rid != 0) {
            resourceId = _context.getResources().getIdentifier("navigation_bar_height", "dimen", "android");
            return _context.getResources().getDimensionPixelSize(resourceId);
        } else
            return 0;
    }

    public static int getStatusBarHeight() {
        return NotchStatusBarUtils.getStatusBarHeight(_context);
    }

}

