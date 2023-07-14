package com.crazymaplestudio.sdk.tools;

import android.app.Application;

import java.lang.reflect.Method;

/**
 * description
 * Created by jin
 * Created date: 2021-06-18
 */
public class ApplicationTool {

    public static Application mApplication;

    public static void init(Application application){
        mApplication = application;
    }

    public static Application getApplication(){
        if (mApplication == null) {
            mApplication = getApplicationInner();
        }
        return mApplication;
    }

    public static Application getApplicationInner() {
        try {
            Class<?> activityThread = Class.forName("android.app.ActivityThread");

            Method currentApplication = activityThread.getDeclaredMethod("currentApplication");
            Method currentActivityThread = activityThread.getDeclaredMethod("currentActivityThread");

            Object current = currentActivityThread.invoke((Object)null);
            Object app = currentApplication.invoke(current);

            return (Application)app;
        } catch (Exception e) {
            e.printStackTrace();
            return null;
        }
    }
}
