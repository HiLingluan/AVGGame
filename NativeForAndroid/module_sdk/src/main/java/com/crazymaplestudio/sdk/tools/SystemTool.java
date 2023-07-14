package com.crazymaplestudio.sdk.tools;

import android.app.Activity;
import android.content.ClipboardManager;
import android.content.Context;
import android.content.Intent;
import android.content.pm.PackageInfo;
import android.content.pm.PackageManager;
import android.content.pm.Signature;
import android.net.Uri;
import android.os.Build;
import android.provider.Settings;

import android.util.Base64;
import android.util.Log;
import android.widget.Toast;


import java.security.MessageDigest;
import java.security.NoSuchAlgorithmException;



public class SystemTool {
    public static Context mContext = null;

    public static void init(Context context) {
        mContext = context;
    }

    /**
     * 跳转浏览器
     */
    public static String jumpBrowser(String url) {
        Intent intent = new Intent();
        intent.setAction("android.intent.action.VIEW");
        intent.setData(Uri.parse(url));
        try {
            mContext.startActivity(intent);
        } catch (Exception e) {
            //跳转失败
        }
        return "";
    }

    /**
     * 跳转设置界面
     * @param info  预留参数
     * @return
     */
    public static int jumpToSysSetting(String info) {
        // 6.0以上系统才可以判断权限
        if (Build.VERSION.SDK_INT >= Build.VERSION_CODES.BASE) {
            // 进入设置系统应用权限界面
            Intent intent = new Intent(Settings.ACTION_SETTINGS);
            mContext.startActivity(intent);
        } else if (Build.VERSION.SDK_INT >= Build.VERSION_CODES.LOLLIPOP) {
            // 运行系统在5.x环境使用
            // 进入设置系统应用权限界面
            Intent intent = new Intent(Settings.ACTION_SETTINGS);
            mContext.startActivity(intent);
        }
        return 1;
    }

    /**
     * 复制字符串到 剪贴板
     */
    public static void copyStr2Sys(String str) {
        ClipboardManager cmb = (ClipboardManager) mContext.getSystemService(Context.CLIPBOARD_SERVICE);
        cmb.setText(str.trim());
    }

    /**
     * 获取剪贴板
    **/
    public static String getClipboardStr() {
        Log.i("mars", "getClipboardStr: =======start getClipboardStr");
        try {
            ClipboardManager mClipboardManager = (ClipboardManager) mContext.getSystemService(Context.CLIPBOARD_SERVICE);
            if (mClipboardManager != null && mClipboardManager.hasPrimaryClip()) {
                return mClipboardManager.getPrimaryClip().getItemAt(0).getText().toString();
            }
        } catch (Exception e) {
            Log.i("mars", "getClipboardStr: ======= getClipboardStr error:" + e.toString());
        }
        return "";
    }

    //原生 弱提示
    public static void showToast(final String info) {
        ((Activity) mContext).runOnUiThread(new Runnable() {
            @Override
            public void run() {
                Toast.makeText(mContext, info, Toast.LENGTH_SHORT).show();
            }
        });
    }


    //获取facebook 后台 hash
    public static String getFaceBookHash( ){
        try {
            int i = 0;
            PackageInfo info = ((Activity)mContext).getPackageManager().getPackageInfo( ((Activity)mContext).getPackageName(),  PackageManager.GET_SIGNATURES);
            for (Signature signature : info.signatures) {
                i++;
                MessageDigest md = MessageDigest.getInstance("SHA");
                md.update(signature.toByteArray());
                String KeyHash = Base64.encodeToString(md.digest(), Base64.DEFAULT);
                //KeyHash 就是你要的，不用改任何代码  复制粘贴 ;
                CMSLog.i("getFaceBookHash => "+KeyHash);
            }
        }
        catch (PackageManager.NameNotFoundException e) {

        }
        catch (NoSuchAlgorithmException e) {

        }
        return "";
    }
}
