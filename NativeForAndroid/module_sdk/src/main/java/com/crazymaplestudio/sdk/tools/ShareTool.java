package com.crazymaplestudio.sdk.tools;

import android.content.Context;
import android.content.Intent;
import android.net.Uri;
//import android.support.annotation.VisibleForTesting;
//import android.support.v4.content.FileProvider;
import android.util.Log;

import androidx.core.content.FileProvider;

import java.io.File;

public class ShareTool {
    private static Context mContext;

    public final static int SHARE_CONTENT_TYPE_URL = 1;
    public final static int SHARE_CONTENT_TYPE_IMAGE = 2;
    public final static int SHARE_CONTENT_TYPE_TXT = 3;

    public static void init(Context context) {
        mContext = context;
    }

    /**
     * 调用系统隐式intent分享图片
     *
     * @param imgPath 路径
     */
    private static void callSystemShareImage(String imgPath) {
        try {
            //创建分享Intent
            Intent intent = new Intent();
            intent.setAction(Intent.ACTION_SEND);
            //分享本地图片
            intent.setType("image/*");
            File file = new File(imgPath);
            Uri uri = FileProvider.getUriForFile(mContext, "com.crazymaplestudio.sdk.fileprovider", file);
            intent.putExtra(Intent.EXTRA_STREAM, uri);
            intent.setFlags(Intent.FLAG_ACTIVITY_NEW_TASK);
            intent.setFlags(Intent.FLAG_GRANT_READ_URI_PERMISSION);
            mContext.startActivity(Intent.createChooser(intent, "share this image to..."));
        } catch (Exception e) {
            Log.i("i", "share error===>" + e.toString());
        }
    }

    /**
     * 调用系统隐式intent分享链接
     *
     * @param content 内容
     */
    private static void callSystemShareTxt(String content) {
        try {
            //创建分享Intent
            Intent intent = new Intent();
            //设置动作为Intent.ACTION_SEND
            intent.setAction(Intent.ACTION_SEND);
            //设置为文本类型
            intent.setType("text/plain");
            intent.putExtra(Intent.EXTRA_SUBJECT, "SpotLight");
            intent.putExtra(Intent.EXTRA_TEXT, content);
            mContext.startActivity(Intent.createChooser(intent, "share this link to..."));
        } catch (Exception e) {
            Log.i("i", "share error===>" + e.toString());
        }
    }

    private static void shareToSystem(int shareType, String content) {
        switch (shareType) {
            case SHARE_CONTENT_TYPE_IMAGE:
                callSystemShareImage(content);
                break;
            case SHARE_CONTENT_TYPE_TXT:
                callSystemShareTxt(content);
                break;
            case SHARE_CONTENT_TYPE_URL:
                callSystemShareTxt(content);
                break;
        }
    }

    /**
     * 分享
     * @param shareType 分享类型
     * @param content   分享的内容
     * @return
     */
    public static int share(int shareType, String content) {
        shareToSystem(shareType, content);
        return 0;
    }
}
