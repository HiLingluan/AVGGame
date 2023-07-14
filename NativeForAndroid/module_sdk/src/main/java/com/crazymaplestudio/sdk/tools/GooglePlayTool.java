package com.crazymaplestudio.sdk.tools;

import android.app.Activity;
import android.content.Intent;
import android.net.Uri;
import android.widget.Toast;

public class GooglePlayTool {
    private static Activity activity;

    public static void init(Activity context) {
        activity = context;
    }

    //打开谷歌
    public static void openGoogle() {
        try {
            Intent intent = new Intent(Intent.ACTION_VIEW);
            //跳转应用市场
            intent.setData(Uri.parse("market://details?id=" + activity.getPackageName()));
            //Google应用市场
            final String GOOGLE_PLAY = "com.android.vending";
            intent.setPackage(GOOGLE_PLAY);
            //有没有Google
            if (intent.resolveActivity(activity.getPackageManager()) != null) {
                activity.startActivity(intent);
            } else {
                //没有用浏览器跳转Google
                intent.setData(Uri.parse("https://play.google.com/store/apps/details?id=" + activity.getPackageName()));
                //有没有浏览器
                if (intent.resolveActivity(activity.getPackageManager()) != null) {
                    activity.startActivity(intent);
                } else {
                    //没有浏览器,提示“缺少打开方式”
                    Toast.makeText(activity, "Lack of open mode", Toast.LENGTH_SHORT).show();
                }
            }
        } catch (Exception e) {
            e.printStackTrace();
        }
    }
}
