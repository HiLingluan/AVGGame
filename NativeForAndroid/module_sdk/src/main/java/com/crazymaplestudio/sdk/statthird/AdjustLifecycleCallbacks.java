package com.crazymaplestudio.sdk.statthird;

import android.app.Activity;
import android.app.Application;
import android.os.Bundle;

import com.adjust.sdk.Adjust;
import com.crazymaplestudio.sdk.tools.CMSLog;

/**
 * description
 * Created by jin
 * Created date: 2021-05-26
 */
public class AdjustLifecycleCallbacks implements Application.ActivityLifecycleCallbacks {
    @Override
    public void onActivityResumed(Activity activity) {
        try {
            Adjust.onResume();
        } catch (Exception e) {
            CMSLog.i("adjust  onresume error==>"+e.toString());
        }
    }
    @Override
    public void onActivityPaused(Activity activity) {
        try {
            Adjust.onPause();
        } catch (Exception e) {
            CMSLog.i("adjust onpause error==>"+e.toString());
        }
    }
    @Override
    public void onActivityDestroyed(Activity activity) {}
    @Override
    public void onActivityCreated(Activity activity, Bundle savedInstanceState) {}
    @Override
    public void onActivitySaveInstanceState(Activity activity, Bundle outState) {}
    @Override
    public void onActivityStarted(Activity activity) {}
    @Override
    public void onActivityStopped(Activity activity) {}

}
