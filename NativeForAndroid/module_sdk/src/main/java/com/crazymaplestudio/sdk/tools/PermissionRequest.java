package com.crazymaplestudio.sdk.tools;

import android.app.Activity;
import android.content.Context;
import android.content.SharedPreferences;
import android.content.pm.PackageManager;
import android.os.Build;
import android.util.Log;

import androidx.core.app.ActivityCompat;

import com.crazymaplestudio.sdk.event.CMSEventConst;
import com.crazymaplestudio.sdk.event.CMSEventManager;
import com.crazymaplestudio.sdk.event.eventmodel.PermissionEvent;

public class PermissionRequest {
    private static Activity mContext;

    public static void init(Activity context) {
        mContext = context;
    }

    public static void onRequestPermissionsResult(final int requestCode, String[] permissions, int[] grantResults) {
        if (grantResults.length > 0 && grantResults[0] == PackageManager.PERMISSION_GRANTED) {
            Log.i("Test请求权限成功", requestCode + "====>request back request success");
            // 权限申请成功
            //UnityTool.OnPermissionRequestSuccess( requestCode + "");
            CMSEventManager.getInstance().postEvent(CMSEventConst.CMS_EVENT_PERMISSION_SUCCESS,new PermissionEvent(requestCode));
        } else {
            Log.i("Test请求权限失败", requestCode + "====>request back request failed");
            //  权限申请失败
            //UnityTool.OnPermissionRequestFailed( requestCode + "");
            CMSEventManager.getInstance().postEvent(CMSEventConst.CMS_EVENT_PERMISSION_FAILED,new PermissionEvent(requestCode));
        }
    }

    //检查指定权限的名字
    public static boolean checkPermission(String permissonName) {
        Log.i("检查指定权限checkPermission", "start check Permisson===>" + permissonName + "===" + Build.VERSION.SDK_INT);
        boolean ret = true;
        String[] permissions = {permissonName};
        if (Build.VERSION.SDK_INT >= 23) {
            //验证是否许可权限
            for (String str : permissions) {
                Log.i("检查权限Test", str + "==check permission==>" + mContext.checkSelfPermission(str));
                ret = (mContext.checkSelfPermission(str) == PackageManager.PERMISSION_GRANTED);
                Log.i("检查权限的结果：", str + "==check permission==>" + ret);
            }
        }
        Log.i("结果", "checkPermission: " + ret);
        return ret;
    }

    //请求指定权限的名字
    public static boolean requestPermission(String permissonName, int requestCode) {
        boolean ret = false;
        int REQUEST_CODE_CONTACT = requestCode;
        String[] permissions = {permissonName};

        //勾选不在提示并拒绝  处理
        if (Build.VERSION.SDK_INT >= 23) {
            if (mContext.checkSelfPermission(permissonName) != PackageManager.PERMISSION_GRANTED) {
                //记录是不是第一次请求
                boolean temp = getPermissionRecord(permissonName);
                //如果勾选了"不再提示"，shouldShowRequestPermissionRationale返回false
                if (!mContext.shouldShowRequestPermissionRationale(permissonName)&&temp) {
                    //提示用户去设置开启
                    //UnityTool.OnNonePermissionTips( ""+requestCode);
                    CMSEventManager.getInstance().postEvent(CMSEventConst.CMS_EVENT_PERMISSION_TIPS,new PermissionEvent(requestCode));
                    return ret;
                } else {
                    //记录是不是第一次
                    setPermissionRecord(permissonName, !mContext.shouldShowRequestPermissionRationale(permissonName));
                }
                //权限请求
                ActivityCompat.requestPermissions((Activity) mContext, permissions, REQUEST_CODE_CONTACT);
            }
        }

        return ret;
    }

    public static void setPermissionRecord(String permission, boolean isRecord) {
        try {
            //本地持久化保存
            SharedPreferences sp = mContext.getSharedPreferences(permission + "is_record", Activity.MODE_PRIVATE);
            SharedPreferences.Editor editor = sp.edit();
            editor.putBoolean(permission + "is_record", isRecord);
            editor.commit();
        } catch (Exception e) {
            e.toString();
        }
    }

    public static boolean getPermissionRecord(String permission) {
        boolean isRecord = false;
        try {
            isRecord = mContext.getSharedPreferences(permission + "is_record", Context.MODE_PRIVATE).getBoolean(permission + "is_record", false);
        } catch (Exception e) {
            e.toString();
        }
        return isRecord;
    }
}
